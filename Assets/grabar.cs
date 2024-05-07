using System;
using System.IO;
using System.Net;
using System.Net.Http;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grabar : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public TextMeshProUGUI chatText;
    public GameObject goSourceAudio;

    AudioClip clip;
    byte[] audioData;
    

    public void recording()
    {
        this.text.text = "Grabando!";


        if (Microphone.devices.Length == 0) return;

        string microphone = Microphone.devices[0];
        clip = Microphone.Start(microphone, true, 15, 44100);

    }

    [System.Obsolete]
    public async void stopRecording()
    {
        this.text.text = "Dejando de grabar!";
        Microphone.End(null);
        string apiUrl = "https://vmn2xzct-5000.usw3.devtunnels.ms/api/upload";

        // Convertir el AudioClip a un arreglo de bytes
        byte[] audioData = ConvertAudioClipToByteArray(clip);

        // Enviar el formulario a la API
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Convertir el arreglo de bytes a un objeto ByteArrayContent
                ByteArrayContent byteContent = new ByteArrayContent(audioData);

                // Realizar una solicitud POST al endpoint con el contenido de bytes
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, byteContent);

                // Verificar si la solicitud fue exitosa (código de estado 200-299)
                if (response.IsSuccessStatusCode)
                {

                    text.text = "Regreso del endpoint...";
                    string chat = await response.Content.ReadAsStringAsync();
                    //byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    text.text = "Proceso la respuesta...";
                    chatText.text = chat;

                    //AudioClip audioClip = BytesToAudioClip(bytes, "audio");
                    //text.text = "Proceso bytes en audioclip...";
                    //AudioSource audioSource = goSourceAudio.GetComponent<AudioSource>();
                    //text.text = "Obtuvo el audioSource...";

                    //Save(audioClip);
                    //text.text = "Guardó el audioClip...";


                    //audioSource.PlayOneShot(audioClip);
                    //text.text = "Reproduciendo...";
                   
                }
                else
                {
                    text.text = "Error";
                }
            }
            catch (HttpRequestException e)
            {
                text.text = "Error: " + e.Message ;
            }
        }

    }

    byte[] ConvertAudioClipToByteArray(AudioClip clip)
    {
        // Convertir el AudioClip a un arreglo de bytes
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        byte[] byteArray = new byte[samples.Length * 2];
        int rescaleFactor = 32767; // Para convertir los valores de los samples a valores de 16-bit PCM
        for (int i = 0; i < samples.Length; i++)
        {
            short intSample = (short)(samples[i] * rescaleFactor);
            byteArray[i * 2] = (byte)(intSample & 0xFF);
            byteArray[i * 2 + 1] = (byte)((intSample >> 8) & 0xFF);
        }

        return byteArray;
    }

    public AudioClip BytesToAudioClip(byte[] audioData, string clipName, AudioType audioType = AudioType.WAV)
    {
        // Crear un AudioClip vacío
        AudioClip audioClip = AudioClip.Create(clipName, audioData.Length / 2, 1, 44100, false);

        // Cargar los datos de audio en el AudioClip
        audioClip.SetData(BytesToFloatArray(audioData), 1);

        // Retornar el AudioClip creado
        return audioClip;
    }
    public void Save(AudioClip audioClip)
    {
        string errorMessage = "";

        byte[] wavData = ConvertAudioClipToByteArray(audioClip);

        try
        {
            if (wavData != null)
            {
                string filePath = Path.Combine(Application.persistentDataPath, "audio.wav");
                File.WriteAllBytes(filePath, wavData);
                text.text = "AudioClip saved to: " + filePath;
            }
            else
            {
                text.text = "Failed to save AudioClip: " + errorMessage;
            }
        }catch (Exception ex)
        {
            text.text = "Error: " + ex.Message;
        }
       
    }

    public float[] BytesToFloatArray(byte[] audioData)
    {
        // Verificar si el arreglo de bytes es nulo o está vacío
        if (audioData == null || audioData.Length == 0)
        {
            Debug.LogError("Arreglo de bytes inválido.");
            return null;
        }

        // Obtener el número de muestras (16 bits por muestra)
        int sampleCount = audioData.Length / 2;

        // Crear un arreglo de valores de punto flotante
        float[] floatArray = new float[sampleCount];

        // Convertir cada par de bytes a un valor de punto flotante
        for (int i = 0; i < sampleCount; i++)
        {
            short sample = BitConverter.ToInt16(audioData, i * 2);
            float sampleFloat = sample / 32768f; // Normalizar a valores entre -1 y 1
            floatArray[i] = sampleFloat;
        }

        return floatArray;
    }
}
