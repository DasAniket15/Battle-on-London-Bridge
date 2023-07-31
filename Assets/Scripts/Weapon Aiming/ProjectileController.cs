using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Projectile properties
    public float speed = 20f;
    public float lifetime = 2f;

    // Static variables shared across all projectiles
    public static int damage;
    private static int bulletHitThreshold;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        // Move the projectile in the forward direction
        rb.velocity = transform.right * speed;
    }

    // Set the damage of the projectile (used before instantiation)
    public void SetDamage(int damage)
    {
        ProjectileController.damage = damage;
    }

    // Get the static bullet hit counter
    public int GetCounter()
    {
        return ProjectileController.bulletHitThreshold;
    }

    // Set the static bullet hit counter (used before instantiation)
    public void SetCounter(int bulletHitThreshold)
    {
        ProjectileController.bulletHitThreshold = bulletHitThreshold;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            // Get the BossHealth component from the boss
            BossHealth bossHealth = other.GetComponent<BossHealth>();

            if (bossHealth != null)
            {
                // Increment the bullet hit threshold
                bulletHitThreshold++;

                // Damage the boss with the projectile damage
                bossHealth.TakeDamage(damage);
            }

            // Destroy the projectile on collision with the boss
            Destroy(gameObject);
        }
    }
}
