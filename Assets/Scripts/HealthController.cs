using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float Health { get; private set; }
    [SerializeField] private float startHealth = 100.0f;
    
    void Start() 
    {
        Health = startHealth;
    }

    public void DealDamage(float amount)
    {
        Health -= amount;
	if (Health <= 0){
		Destroy(gameObject);
    }
}
}