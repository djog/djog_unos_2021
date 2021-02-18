using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float spawnOffset = 1.0f;
    public float spawnZ = 10.0f;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 ownPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = (mousePos - ownPos).normalized;
            Vector3 spawnPos = ownPos + direction * spawnOffset;
            float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bullet = Instantiate(BulletPrefab, spawnPos, Quaternion.Euler(0.0f, 0.0f, degrees));
        }
    }
}