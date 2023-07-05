using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CustomInput input = null;
    private Rigidbody2D rb;
    private InputAction movementAction;
    private Vector2 moveDirection;

    private void Awake()
    {
        // Obtener la acci�n de movimiento del archivo de entrada de acciones
        movementAction = new InputAction();
        movementAction.Enable();
    }

    private void OnEnable()
    {
        // Suscribirse al evento de actualizaci�n de la acci�n de movimiento
        movementAction.performed += OnMovementPerformed;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento de actualizaci�n de la acci�n de movimiento
        movementAction.performed -= OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // Obtener el valor de direcci�n de movimiento
        moveDirection = context.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
    }

    private void Update()
    {
        // Mover el jugador utilizando la direcci�n de movimiento
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
