using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.1f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;

    private Vector2 m_MoveVector;
    private float m_TurnSmoothVelocity;
    private CharacterController m_CharacterController;
    private Animator m_animator;
    public Transform cam;

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

    void Move()
    {
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;

        if (direction.magnitude > 0.1f)
        {
            // Obtenir l'angle de direction à partir du vecteur de direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            // Faire pivoter le joueur
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Déplacer le joueur dans la direction pointée par la caméra
            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            // Appliquer le mouvement
            m_CharacterController.Move(moveDirection.normalized * m_Speed * Time.deltaTime);

            // Changer l'état de l'animation en courir vers l'avant
            m_animator.SetBool("Idle", false);
            m_animator.SetBool("Run Forward", true);
        }
        else if (direction.magnitude < 0.1f)
        {
            // Changer l'état de l'animation en idle
            m_animator.SetBool("Idle", true);
            m_animator.SetBool("Run Forward", false);
        }
    }

    // Lorsque la touche Échap est pressée, quitter le jeu
    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Escape pressed");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
    }
}
