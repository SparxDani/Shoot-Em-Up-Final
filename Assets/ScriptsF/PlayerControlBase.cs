using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerControlBase : MonoBehaviour
{
    // Variables de movimiento
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Vector2 initialPosition;
    private CustomInput input = null;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private Camera mainCamera;
    private Vector2 minCameraPos;
    private Vector2 maxCameraPos;

    // Variables de disparo
    [Header("Shooting")]
    public float fireDelay = 0.5f;
    private bool isFiring = false;
    private float lastFire = 0.0f;
    public Transform shootOriginCenter;
    public GameObject shootPrefabCenter;
    public Transform shootOriginLeft;
    public GameObject shootPrefabLeft;
    public Transform shootOriginRight;
    public GameObject shootPrefabRight;

    // Variables de escudo
    [Header("Shield")]
    public GameObject shield;
    private bool hasShield = false;

    // Variables de invencibilidad
    [Header("Invincibility")]
    public int hits = 3;
    private bool invincible = false;
    private float invincibleTimer = 0;
    public float invincibleTime = 2;

    // Variables de power-ups
    private bool hasDashPowerUp = false;
    private bool hasTripleShootPowerUp = false;

    // Referencia al SpriteRenderer
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        initialPosition = transform.position;
        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
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
            if (hasTripleShootPowerUp)
                FireTripleShoot();
            else
                Fire();
        }

        if (invincible)
        {
            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
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
        if (hasDashPowerUp && !isDashing && !isDashCooldown)
        {
            StartCoroutine(StartDashCoroutine());
        }
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    private void OnFireCancelled(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletLogic bullet = collision.GetComponent<BulletLogic>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
            }
        }

        EnemyManagement destructable = collision.GetComponent<EnemyManagement>();
        if (destructable != null)
        {
            Hit(destructable.gameObject);
        }
        PowerUpData powerUp = collision.GetComponent<PowerUpData>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
            }
            if (powerUp.dashSkill)
            {
                hasDashPowerUp = true;
            }
            if (powerUp.tripleShoot)
            {
                hasTripleShootPowerUp = true;
            }
            LevelManager.instance.AddScore(powerUp.pointValue);
            Destroy(powerUp.gameObject);
        }
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
        Instantiate(shootPrefabCenter, shootOriginCenter.position, Quaternion.identity);
    }

    private void FireTripleShoot()
    {
        lastFire = Time.time;
        Instantiate(shootPrefabLeft, shootOriginLeft.position, Quaternion.identity);
        Instantiate(shootPrefabCenter, shootOriginCenter.position, Quaternion.identity);
        Instantiate(shootPrefabRight, shootOriginRight.position, Quaternion.identity);
    }

    private void ActivateShield()
    {
        shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        shield.SetActive(false);
    }
    bool HasShield()
    {
        return shield.activeSelf;
    }
    void Hit(GameObject gameObjectHit)
    {
        if (HasShield())
        {
            DeactivateShield();
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetShip();
                }
                else
                {
                    invincible = true;
                }
            }
        }
    }

    private void ResetShip()
    {
        transform.position = initialPosition;
        DeactivateShield();
        StartDashCoroutine();
        hasTripleShootPowerUp = false;
        hits = 3;
        LevelManager.instance.ResetLevel();
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

    private IEnumerator StartDashCoroutine()
    {
        if (!isDashing && !isDashCooldown)
        {
            isDashing = true;
            rb.velocity = moveDirection * dashSpeed;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector2.zero;
            isDashing = false;
            StartCoroutine(DashCooldownCoroutine());
        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }
}
