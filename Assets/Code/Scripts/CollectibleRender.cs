using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectibleRender : MonoBehaviour
{
    [SerializeField] private GameObject m_CollectibleCanvas; // Canvas pour le dialogue
    [SerializeField] private GameObject m_InteractCanvas; // Canvas pour le "Intéragir avec 'F' "
    [SerializeField] private GameObject wallToDisappear; // Mur spécifique à ce PNJ

    private bool m_IsInRange = false;
    private bool isInteracting = false;


    // Référence uniquement définie pour les collectibles de type "Woodplank"
    private Woodplank woodplankNpc;

    void Start()
    {
        // Assigne `woodplankNpc` seulement si le collectible a le tag "Woodplank"
        if (gameObject.tag == "Woodplank")
        {
            woodplankNpc = FindObjectOfType<Woodplank>();
        }
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Vérifie si un player rentre dans la zone & que le MeshRenderer + Collider sont activé sur le collectible
        if (context.performed && m_IsInRange && !isInteracting && GetComponent<MeshRenderer>().enabled && GetComponent<Collider>().enabled)
        {

            // Affiche le dialogue
            m_CollectibleCanvas.SetActive(true);
            isInteracting = true;

            // Faire disparaître le mur invisible associé à ce collectible
            if (wallToDisappear != null)
            {
                wallToDisappear.SetActive(false);
            }

            // Faire disparaître le collectible visuellement pour permettre de refermer la modal
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            // Faire disparaître le "Intéragir avec 'F'" car c'est un collectible
            m_InteractCanvas.SetActive(false);

            // Vérifie si le collectible est de type "woodplank" avant d'incrémenter le compteur
            if (gameObject.tag == "Woodplank")
            {
                woodplankNpc.AddWoodplank();
            }

        }
        else if (context.performed && m_IsInRange && isInteracting)
        {
            // Sinon referme la modal & reinitialise isInteracting
            m_CollectibleCanvas.SetActive(false);
            isInteracting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si un player rentre dans la zone & que le MeshRenderer + Collider sont activé sur le collectible
        if (other.gameObject.tag == "Player" && GetComponent<MeshRenderer>().enabled && GetComponent<Collider>().enabled)
        {
            // Debug.Log("player enter");
            m_IsInRange = true;
            m_InteractCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Vérifie si le player quitte la zone
        if (other.gameObject.tag == "Player")
        {
            m_IsInRange = false;
            m_CollectibleCanvas.SetActive(false);
            m_InteractCanvas.SetActive(false);
        }
    }
}
