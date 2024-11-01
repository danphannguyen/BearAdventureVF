using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectibleRender : MonoBehaviour
{
    [SerializeField] private GameObject m_CollectibleCanvas;
    [SerializeField] private GameObject m_InteractCanvas;
    [SerializeField] private GameObject wallToDisappear; // Mur spécifique à ce PNJ

    private bool m_IsInRange = false;
    private bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Check if the player is in range of an NPC
        if (context.performed && m_IsInRange && !isInteracting)
        {

            // Affiche le dialogue
            m_CollectibleCanvas.SetActive(true);
            isInteracting = true;

            // Faire disparaître le mur invisible associé à ce collectible
            if (wallToDisappear != null)
            {
                wallToDisappear.SetActive(false);
            }

            // Faire disparaître le collectible
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            // Faire disparaître le "Intéragir avec 'F'" car c'est un collectible
            m_InteractCanvas.SetActive(false);

        } else if (context.performed && m_IsInRange && isInteracting)
        {
            m_CollectibleCanvas.SetActive(false);
            isInteracting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is in range of an NPC
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("player enter");
            m_IsInRange = true;
            m_InteractCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player is out of range of an NPC
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("player exit");
            m_IsInRange = false;
            m_CollectibleCanvas.SetActive(false);
            m_InteractCanvas.SetActive(false);
        }
    }
}
