using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NpcRender : MonoBehaviour
{

    [SerializeField] private NpcController npc;
    [SerializeField] private TextMeshProUGUI dialog; // Texte a modifier
    [SerializeField] private GameObject m_PNJCanvas; // Canvas pour le dialogue
    [SerializeField] private GameObject m_InteractCanvas; // Canvas pour le "Intéragir avec 'F' "
    [SerializeField] private GameObject wallToDisappear; // Mur spécifique à ce PNJ

    private bool m_IsInRange = false;
    private bool isInteracting = false;

    // Référence au script Woodplank
    private Woodplank woodplankNpc;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise les dialogues des NPC
        dialog.text = npc.dialog;

        // Récupérer la référence au script Woodplank attaché
        woodplankNpc = GetComponent<Woodplank>();
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Vérifie si le player est dans la range, n'a pas déjà intéragie
        if (context.performed && m_IsInRange && !isInteracting)
        {
            // Affiche le dialogue
            m_PNJCanvas.SetActive(true);
            isInteracting = true;

            // Si c'est le Npc rélié au script woodplank
            if (woodplankNpc != null)
            {
                // Vérifie si les woodplanks requis ont été collectés
                if (woodplankNpc.RequirementMet())
                {
                    dialog.text = woodplankNpc.GetSecondaryDialog(); // Change le dialogue
                    // Faire disparaître le mur associé à ce PNJ
                    if (wallToDisappear != null)
                    {
                        wallToDisappear.SetActive(false);
                    }
                }
            }
        }
        else if (context.performed && m_IsInRange && isInteracting)
        {
            // Sinon referme la modal & reinitialise isInteracting
            m_PNJCanvas.SetActive(false);
            isInteracting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        // Vérifie si le player rentre dans la range du npc
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("player enter");
            m_IsInRange = true;
            m_InteractCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {

        // Vérifie si le player sort dans la range du npc
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("player exit");
            m_IsInRange = false;
            m_PNJCanvas.SetActive(false);
            m_InteractCanvas.SetActive(false);
        }
    }

}
