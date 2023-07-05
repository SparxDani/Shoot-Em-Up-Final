using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlBase : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float fireDelay = 0.5f;

    private CustomInput input = null;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private Camera mainCamera;
    private Vector2 minCameraPos;
    private Vector2 maxCameraPos;

    public Transform shootOrigin;
    public GameObject shootPrefab;
    private bool isFiring = false;
    private float lastFire = 0.0f;

    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = moveDirection * moveSpeed;
            ClampPlayerPosition();
        }
    }

    private void Update()
    {
        if (isDashing && !isDashCooldown)
            StartCoroutine(DashCooldown());

        if (isFiring && CanFire())
        {
            Fire();
        }
    }

    private void OnEnable()
    {
        input.Enable();
        input.Ingame.Movement.performed += OnMovementPerformed;
        input.Ingame.Movement.canceled += OnMovementCancelled;
        input.Ingame.Dash.performed += OnDashPerformed;
        input.Ingame.Fire.performed += OnFirePerformed;
        input.Ingame.Fire.canceled += OnFireCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Ingame.Movement.performed -= OnMovementPerformed;
        input.Ingame.Movement.canceled -= OnMovementCancelled;
        input.Ingame.Dash.performed -= OnDashPerformed;
        input.Ingame.Fire.performed -= OnFirePerformed;
        input.Ingame.Fire.canceled -= OnFireCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (!isDashing && !isDashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = moveDirection * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    private IEnumerator DashCooldown()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }

    private void CalculateCameraBounds()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        minCameraPos.x = mainCamera.transform.position.x - cameraWidth;
        minCameraPos.y = mainCamera.transform.position.y - cameraHeight;
        maxCameraPos.x = mainCamera.transform.position.x + cameraWidth;
        maxCameraPos.y = mainCamera.transform.position.y + cameraHeight;
    }

    private void ClampPlayerPosition()
    {
        float clampedX = Mathf.Clamp(rb.position.x, minCameraPos.x, maxCameraPos.x);
        float clampedY = Mathf.Clamp(rb.position.y, minCameraPos.y, maxCameraPos.y);
        rb.position = new Vector2(clampedX, clampedY);
    }

    private bool CanFire()
    {
        return Time.time >= lastFire + fireDelay;
    }

    private void Fire()
    {
        lastFire = Time.time;
        Instantiate(shootPrefab, shootOrigin.position, Quaternion.identity);
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    private void OnFireCancelled(InputAction.CallbackContext context)
    {
        isFiring = false;
    }
}
