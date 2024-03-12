using Assets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gotear : MonoBehaviour
{
    [SerializeField] Liquid liquidoBureta;
    [SerializeField] Liquid liquidoMatraz;
    public GameObject llave;
    Renderer renderer;
    Material material;
    public Animator gotearAnimator;
    float counter = .000f;

    // Start is called before the first frame update
    public TextMeshProUGUI countermL;
    void Start()
    {
        renderer = liquidoMatraz.GetComponent<Renderer>();
        material = renderer.material;

    }

    // Update is called once per frame
    void Update()
    {

        if (llave.transform.rotation.y <= -0.085f)
        {
            liquidoBureta.fillAmount += -0.0001f * llave.transform.rotation.y;
            gotearAnimator.speed = Math.Abs(-10 * llave.transform.rotation.y);
            gotearAnimator.Play("gotear");
            countermL.text = (float.Parse(countermL.text) + Math.Abs(0.05f * llave.transform.rotation.y)) + "";
        }
        else
        {
            gotearAnimator.Play("gotear", -1, 0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Color stepColorRGB;

        if (other.tag == "liquid" && llave.transform.rotation.y <= -0.085f)
        {
            //int rotations = ((int)_nozzle.localEulerAngles.z + 45) / 90;

            Color tintColor = material.GetColor("_Tint");
            Color topColor = material.GetColor("_TopColor");
            //tintColor.r -= -liquidoMatraz.step * llave.transform.rotation.y;
            //topColor.r -= -0.0005f * llave.transform.rotation.y;

            //stepColorInt = (float)(liquidoMatraz.step * (1 - llave.transform.rotation.y));
            //setpColorHex = stepColorInt.ToString("X6");

            //UnityEngine.ColorUtility.TryParseHtmlString(liquidoMatraz.step, out stepColorRGB);
            //string[] stepColorRGB = liquidoMatraz.step.Split(",");

            //tintColor.r -= float.Parse(stepColorRGB[0]) * (0.1f - llave.transform.rotation.y);
            //tintColor.g -= float.Parse(stepColorRGB[1]) * (0.1f - llave.transform.rotation.y);
            //tintColor.b -= float.Parse(stepColorRGB[2]) * (0.1f - llave.transform.rotation.y);

            //topColor.r -= float.Parse(stepColorRGB[0]) * (0.1f - llave.transform.rotation.y);
            //topColor.g -= float.Parse(stepColorRGB[1]) * (0.1f - llave.transform.rotation.y);
            //topColor.b -= float.Parse(stepColorRGB[2]) * (0.1f - llave.transform.rotation.y);

            Color colorInicialRGB;
            Color colorBuenoRGB;
            Color colorFinalRGB;

            float ratio1 = (float)(counter / 0.0005f) / liquidoMatraz.step;
            float ratio2 = (float)(liquidoMatraz.step - (counter / 0.0005f)) / liquidoMatraz.step;

            UnityEngine.ColorUtility.TryParseHtmlString(liquidoMatraz.colorInicial, out colorInicialRGB);
            UnityEngine.ColorUtility.TryParseHtmlString(liquidoMatraz.colorBueno, out colorBuenoRGB);
            UnityEngine.ColorUtility.TryParseHtmlString(liquidoMatraz.colorFinal, out colorFinalRGB);

            Color nuevoColor = InterpolarColor(colorInicialRGB, colorBuenoRGB, colorFinalRGB, ratio1, ratio2);


            material.SetColor("_Tint", nuevoColor);
            material.SetColor("_TopColor", nuevoColor);

            counter += 0.0005f;
        }


    }

    public static Color InterpolarColor(Color colorInicial, Color colorBueno, Color colorFinal, double ratio1, double ratio2)
    {
        float nuevoRojo = (float)(colorInicial.r * ratio2 + colorFinal.r * ratio1 + colorBueno.r * ratio2);
        float nuevoVerde = (float)(colorInicial.g * ratio2 + colorFinal.g * ratio1 + colorBueno.g * ratio2);
        float nuevoAzul = (float)(colorInicial.b * ratio2 + colorFinal.b * ratio1 + colorBueno.b * ratio2);

        return new Color(nuevoRojo, nuevoVerde, nuevoAzul);
    }
}
