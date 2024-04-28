using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class calcular : MonoBehaviour
{
    private Toggle toggleAceptar;
    public TextMeshProUGUI calculoData;
    public TextMeshProUGUI volumenTitulante;
    public TextMeshProUGUI volumenAnalito;
    public TextMeshProUGUI concentracionTitulante;
    public TextMeshProUGUI concentracionAnalito;
    public TextMeshProUGUI resultado;

    private void Start()
    {
        toggleAceptar = GetComponent<Toggle>();
        toggleAceptar.isOn = false;
    }

    public void Calculate()
    {

        double volumenTitulanteF = getNumber(volumenTitulante.text);

        if (toggleAceptar.isOn && volumenTitulanteF != 0)
        {
            double volumenAnalitoF = getNumber(volumenAnalito.text);

            double concentracionTitulanteF = getNumber(concentracionTitulante.text);

            //double concentracionAnalitoF = getNumber(concentracionAnalito.text); 

            double resultadoF = 0;

            calculoData.text = $"m = ({concentracionTitulanteF} * {volumenTitulanteF}) / {volumenAnalitoF}";
            resultadoF = (concentracionTitulanteF * volumenTitulanteF) / volumenAnalitoF;
            resultado.text = $"m = {resultadoF}";
            concentracionAnalito.text = resultadoF + "";
            toggleAceptar.isOn = false;
        }
        else
        {
            toggleAceptar.isOn = false;
        }


    }

    private double getNumber(string text)
    {
        Regex regex = new Regex(@"\d+(\.\d+)?");

        // Buscamos todas las coincidencias en el string
        MatchCollection matches = regex.Matches(text);

        // Iteramos sobre las coincidencias encontradas

        // Convertimos el valor encontrado a double
        double number = 0;
        double.TryParse(matches[0].Value, out number);

        return number;

    }
}
