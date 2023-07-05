using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public AudioSource audioSource;
    public AudioClip shootSound;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
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
}
