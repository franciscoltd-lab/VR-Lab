using Assets;
using Meta.WitAi.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class setAnalito : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas infoCardAnalito;
    public Canvas infoCardTitulante;
    public Canvas infoCardIndicador;
    private TextMeshProUGUI[] infoTextAnalito;
    private TextMeshProUGUI[] infoTextTitulante;
    private TextMeshProUGUI[] infoTextIndicador;
    private Toggle change;

    public Liquid liquidoMatraz;
    private Renderer renderer;
    private Material material;

    private string url = "http://192.168.1.72:5000/analito/";


    private void Start()
    {
        renderer = liquidoMatraz.GetComponent<Renderer>();
        material = renderer.material;
    }
    public async void getAnalito()
    {
        //int id = int.Parse(this.GetComponent<GameObject>().tag);
        change = GetComponent<Toggle>();
        string[] tag = change.tag.Split("/");

        int id = int.Parse(tag[1]);

        if (change.isOn)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza una solicitud GET al endpoint
                    HttpResponseMessage response = await client.GetAsync(url+id);

                    // Verifica si la solicitud fue exitosa (c�digo de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta en formato JSON
                        string responseBody = await response.Content.ReadAsStringAsync();
                        AnalitoInfo analitoInfo = JsonConvert.DeserializeObject<AnalitoInfo>(responseBody);

                        //Llenar la informaci�n con el analito seleccionado
                        infoTextAnalito = infoCardAnalito.GetComponentsInChildren<TextMeshProUGUI>();
                        infoTextAnalito[1].text = analitoInfo.nombre_analito;
                        infoTextAnalito[4].text = "Estructura: " + analitoInfo.estructura_analito;

                        //Llenar la informaci�n con el analito seleccionado
                        infoTextTitulante = infoCardTitulante.GetComponentsInChildren<TextMeshProUGUI>();
                        infoTextTitulante[1].text = analitoInfo.nombre_titulante;
                        infoTextTitulante[2].text = "Concentraci�n Real: " + analitoInfo.concentracion_titulante + " N";
                        infoTextTitulante[3].text = "Estructura: " + analitoInfo.estructura_titulante;

                        //LLenar la información del indicador utilizado
                        infoTextIndicador = infoCardIndicador.GetComponentsInChildren<TextMeshProUGUI>();
                        infoTextIndicador[1].text = analitoInfo.nombre_indicador + "";
                        //DEFINIR EL COLOR INICIAL DEL ANALITO
                        Color tintColor = material.GetColor("_Tint");
                        Color topColor = material.GetColor("_TopColor");
                      
                        Color colorInicial;

                        ColorUtility.TryParseHtmlString(analitoInfo.color_inicial, out colorInicial);

                        material.SetColor("_Tint", colorInicial);
                        material.SetColor("_TopColor", colorInicial);

                        //Definir el step con el que va a crecer el color por gota 
                        //int color_inicial = int.Parse(analitoInfo.color_inicial, System.Globalization.NumberStyles.HexNumber);
                        //int color_bueno = int.Parse(analitoInfo.color_bueno, System.Globalization.NumberStyles.HexNumber);

                        //string colorInicialString = analitoInfo.color_inicial.Replace("#", "");
                        //string colorBuenoString = analitoInfo.color_bueno.Replace("#", "");

                        //int colorInicialInt = int.Parse(colorInicialString, System.Globalization.NumberStyles.HexNumber);
                        //int colorBuenolInt = int.Parse(colorBuenoString, System.Globalization.NumberStyles.HexNumber);

                        //int stepHex = (int)Math.Floor((colorBuenolInt - colorInicialInt) * 0.0005f);
                        //string setpColorHex = stepHex.ToString("X6");

                        //string rgbColorStep = HexToRgb(setpColorHex);
                        liquidoMatraz.colorInicial = analitoInfo.color_inicial;
                        liquidoMatraz.colorBueno = analitoInfo.color_bueno;
                        liquidoMatraz.colorFinal = analitoInfo.color_final;
                        liquidoMatraz.step = 25 / 0.0005f;   

                    }
                    else
                    {
                        // Si la solicitud no fue exitosa, imprime el c�digo de estado
                        Console.WriteLine($"Error al realizar la solicitud: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                }
            }
        }
    }

    static string HexToRgb(string hex)
    {
        // Eliminar el símbolo '#' si está presente
        hex = hex.Replace("#", "");

        // Convertir a valores RGB
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        // Normalizar los valores RGB
        double rNormalized = r / 255.0;
        double gNormalized = g / 255.0;
        double bNormalized = b / 255.0;

        // Devolver la representación RGB normalizada
        return $"{rNormalized:F2}, {gNormalized:F2}, {bNormalized:F2}";
    }
}
