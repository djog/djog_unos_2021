using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float startVelocity = 100;
    public float damage = 100;

    public float destroyAfter = 8.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * startVelocity;
        
        Destroy(gameObject, destroyAfter);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.layer == 6) // 6 is Damageable layer
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.DealDamage(damage);
        }
    }
}