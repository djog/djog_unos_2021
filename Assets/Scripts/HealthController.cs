using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float Health { get; private set; }
    [SerializeField] private float startHealth = 100.0f;
    
    void Start() 
    {
        Health = startHealth;
    }

    void DealDamage(float amount)
    {
        Health -= amount;
    }
}