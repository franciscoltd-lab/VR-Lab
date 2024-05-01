using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class record : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
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
        string apiUrl = "http://192.168.1.75:5000/api/upload";

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
                    string responseBody = await response.Content.ReadAsStringAsync();

                    text.text = responseBody + "";
                }
                else
                {
                    text.text = response.ToSafeString();
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
}
