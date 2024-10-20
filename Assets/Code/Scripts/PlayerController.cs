using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.1f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    [SerializeField] private GameObject m_PNJCanvas;

    private Vector2 m_MoveVector;
    private float m_TurnSmoothVelocity;
    private CharacterController m_CharacterController;
    private Animator m_animator;
    private bool m_IsInRange = false;
    private bool isInteracting = false;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Check if the player is in range of an NPC
        if (context.performed && m_IsInRange && !isInteracting)
        {
            m_PNJCanvas.SetActive(true);
            isInteracting = true;
        } else if (context.performed && m_IsInRange && isInteracting)
        {
            m_PNJCanvas.SetActive(false);
            isInteracting = false;
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;

        if (direction.magnitude > 0.1f)
        {
            // Get direction angle from direction vector
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            // Rotate the player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player
            Vector3 moveDirection = direction;

            // Apply the movement
            m_CharacterController.Move(moveDirection.normalized * m_Speed * Time.deltaTime);

            // Change animation state to run forward
            m_animator.SetBool("Idle", false);
            m_animator.SetBool("Run Forward", true);
        } else if (direction.magnitude < 0.1f)
        {
            // Change animation state to idle
            m_animator.SetBool("Idle", true);
            m_animator.SetBool("Run Forward", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is in range of an NPC
        if (other.gameObject.tag == "NPC")
        {
            m_IsInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player is out of range of an NPC
        if (other.gameObject.tag == "NPC")
        {
            m_IsInRange = false;
            m_PNJCanvas.SetActive(false);
        }
    }
}
