using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BullletMovement : MonoBehaviour
{
    public float startVelocity = 100;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * startVelocity;
    }
}