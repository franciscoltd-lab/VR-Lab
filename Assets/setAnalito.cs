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
    private TextMeshProUGUI[] infoTextAnalito;
    private TextMeshProUGUI[] infoTextTitulante;
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

                        //DEFINIR EL COLOR INICIAL DEL ANALITO
                        Color tintColor = material.GetColor("_Tint");
                        Color topColor = material.GetColor("_TopColor");

                        Color colorInicial;

                        ColorUtility.TryParseHtmlString(analitoInfo.color_inicial, out colorInicial);

                        material.SetColor("_Tint", colorInicial);
                        material.SetColor("_TopColor", colorInicial);


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
}
