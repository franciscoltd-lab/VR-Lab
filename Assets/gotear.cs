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
        

        if (other.tag == "liquid" && llave.transform.rotation.y <= -0.085f)
        {
            //int rotations = ((int)_nozzle.localEulerAngles.z + 45) / 90;

            Color tintColor = material.GetColor("_Tint");
            Color topColor = material.GetColor("_TopColor");
            tintColor.r -= -0.0005f * llave.transform.rotation.y;
            topColor.r -= -0.0005f * llave.transform.rotation.y;
            material.SetColor("_Tint", tintColor);
            material.SetColor("_TopColor", topColor);
        }

    }
}
