using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject replacement;
    
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().PickupWeapon(objectPrefab);
            if (replacement)
            {
                Instantiate(replacement, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
