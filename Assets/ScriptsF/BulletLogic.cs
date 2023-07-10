using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public bool isEnemy = false;

    private Rigidbody2D rb;
    [SerializeField] public Vector2 direction = Vector2.up; // Dirección por defecto hacia arriba

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normalizar el vector para mantener la magnitud constante
    }

    // Sobrecarga del método SetDirection para aceptar valores individuales de x e y
    public void SetDirection(float x, float y)
    {
        SetDirection(new Vector2(x, y));
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        Destroy(gameObject, lifetime);
        PlayShootSound();
    }

    private void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
