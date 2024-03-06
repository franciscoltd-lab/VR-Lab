using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hoverInfo : MonoBehaviour
{
    public string infoText = "Información a mostrar";
    public Transform infoPanelPrefab; // Prefab del panel de información
    private Transform infoPanelInstance; // Instancia del panel de información

    private bool isHovering = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            isHovering = true;
            // Mostrar la información cuando el jugador entre en el área del collider
            ShowInfo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            isHovering = false;
            // Ocultar la información cuando el jugador salga del área del collider
            HideInfo();
        }
    }

    private void ShowInfo()
    {
        // Crea una instancia del panel de información si no existe
        if (infoPanelInstance == null)
        {
            infoPanelInstance = Instantiate(infoPanelPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            infoPanelInstance.GetComponentInChildren<TextMesh>().text = infoText;
        }
    }

    private void HideInfo()
    {
        // Destruye la instancia del panel de información si existe
        if (infoPanelInstance != null)
        {
            Destroy(infoPanelInstance.gameObject);
            infoPanelInstance = null;
        }
    }
}
