using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    // Start is called before the first frame update
    // public OVRHand hands;
    public float grabThreshold = 0.8f;
    public float shakeThreshold = 2.0f;
    public float shakeCooldown = 0.5f;

    // [SerializeField] Liquid liquidoMatraz;
    // Renderer renderer;
    // Material material;
    // public LayerMask grabbableLayer;
    // public GameObject grabbedObject;

    void Start()
    {
        // renderer = liquidoMatraz.GetComponent<Renderer>();
        // material = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        // if(hands.GetFingerPinchStrength(OVRHand.HandFinger.Index) > grabThreshold && grabbedObject == null)
        // {
        //     //material.SetColor("_Tint", Color.red);
        //     Ray ray = new Ray(hands.transform.position, hands.transform.forward);
        //     RaycastHit hit;

        //     if(Physics.Raycast(ray, out hit, Mathf.Infinity, grabbableLayer))
        //     {
        //         grabbedObject = hit.collider.gameObject;
        //         material.SetColor("_Tint", Color.red);
        //     }

        //     //material = grabbedObject.GetComponent<Renderer>().material;

        //     if (hands.GetFingerPinchStrength(OVRHand.HandFinger.Index) > grabThreshold && grabbedObject != null)
        //     {
        //         grabbedObject = null;   
        //     }


        // }


    }
}
