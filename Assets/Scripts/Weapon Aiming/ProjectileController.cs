using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 20f;
    public static int damage;
    public float lifetime = 2f;
    private static int bulletHitThreshold;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }


    public void SetDamage(int damage)
    {
        ProjectileController.damage = damage;
    }


    public int GetCounter()
    {
        return ProjectileController.bulletHitThreshold;
    }


    public void SetCounter(int bulletHitTreshold)
    {
       ProjectileController.bulletHitThreshold = bulletHitTreshold;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();

            if (bossHealth != null)
            {
                bulletHitThreshold++;
                
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
