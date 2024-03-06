using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    // Update is called once per frame
    void Update()
    {
        eulerAngX = transform.localEulerAngles.x;

        if(eulerAngX > 355)
        {

        }       
    }
}
