using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyManagement : MonoBehaviour
{
    public Vector2 moveDirection;      
    public float moveSpeed;            
    public bool autoShoot;             
    public float fireRate;            
    public GameObject bulletPrefab;  
    public Transform firePoint;        
    public GameObject explosion;
    public int scoreValue = 100;

    protected float fireDelay;            
    protected float nextFireTime; 
    protected bool canBeDestroyed = false;
    protected bool collidedWithDestroyer = false;

    private void Start()
    {
        fireDelay = 1f / fireRate;
        nextFireTime = Time.time + fireDelay;
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (collidedWithDestroyer)
        {
            DestroyDestructable();
            collidedWithDestroyer = false;
        }

        if (autoShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireDelay;
        }
    }

    protected void Shoot()
    {
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
        Destroy(gameObject);
    }
}
