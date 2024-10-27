using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NpcRender : MonoBehaviour
{

    [SerializeField] private NpcController npc;
    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject m_PNJCanvas;
    [SerializeField] private GameObject wallToDisappear; // Mur spécifique à ce PNJ

    private bool m_IsInRange = false;
    private bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(npc.dialog);
        dialog.text = npc.dialog;
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Check if the player is in range of an NPC
        if (context.performed && m_IsInRange && !isInteracting)
        {
            m_PNJCanvas.SetActive(true);
            isInteracting = true;

            // Faire disparaître le mur associé à ce PNJ
            if (wallToDisappear != null)
            {
                wallToDisappear.SetActive(false);
            }
        } else if (context.performed && m_IsInRange && isInteracting)
        {
            m_PNJCanvas.SetActive(false);
            isInteracting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // Check if the player is in range of an NPC
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player enter");
            m_IsInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        // Check if the player is out of range of an NPC
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player exit");
            m_IsInRange = false;
            m_PNJCanvas.SetActive(false);
        }
    }

}
