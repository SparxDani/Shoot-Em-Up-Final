using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyManagement : MonoBehaviour
{
    public Vector2 moveDirection;      // Dirección en la que se mueve el enemigo
    public float moveSpeed;            // Velocidad de movimiento del enemigo
    public bool autoShoot;             // Indica si el enemigo dispara automáticamente
    public float fireRate;             // Cadencia de disparo del enemigo (disparos por segundo)
    public GameObject bulletPrefab;    // Prefab del proyectil que dispara el enemigo
    public Transform firePoint;        // Punto de origen de los disparos del enemigo
    public GameObject explosion;
    public int scoreValue = 100;

    private float fireDelay;            // Retardo entre disparos
    private float nextFireTime;         // Tiempo en el que se realizará el siguiente disparo
    private bool canBeDestroyed = false;
    protected bool collidedWithDestroyer = false;

    private void Start()
    {
        // Calcula el retardo inicial entre disparos
        fireDelay = 1f / fireRate;
        // Calcula el tiempo del próximo disparo
        nextFireTime = Time.time + fireDelay;
        LevelManager.instance.AddDestructable();
    }

    private void Update()
    {
        // Mueve el enemigo en la dirección especificada
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (collidedWithDestroyer)
        {
            DestroyDestructable();
            collidedWithDestroyer = false;
        }

        // Si está habilitado el disparo automático y ha pasado suficiente tiempo, dispara
        if (autoShoot && Time.time >= nextFireTime)
        {
            Shoot();
            // Actualiza el tiempo del próximo disparo
            nextFireTime = Time.time + fireDelay;
        }
    }

    private void Shoot()
    {
        // Instancia el proyectil en la posición del firePoint y con su rotación
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            collidedWithDestroyer = true;
        }
        if (!canBeDestroyed)
        {
            return;
        }
        BulletLogic bullet = collision.GetComponent<BulletLogic>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                LevelManager.instance.AddScore(scoreValue);
                DestroyDestructable();
                Destroy(bullet.gameObject);
            }
        }
    }


    protected void DestroyDestructable()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        LevelManager.instance.RemoveDestructable();
        Destroy(gameObject);
    }
}
