using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : EnemyManagement
{
    // Start is called before the first frame update
    void Start()
    {
        fireDelay = 1f / fireRate;
        // Calcula el tiempo del próximo disparo
        nextFireTime = Time.time + fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            collidedWithDestroyer = true;
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
}
