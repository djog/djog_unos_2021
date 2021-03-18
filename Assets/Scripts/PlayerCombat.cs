using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float spawnOffset = 1.0f;
    public float spawnZ = 10.0f;
    public Transform weaponParent;
    public Weapon weapon;
    private float fireTimer;

    void Update()
    {
        if (!weapon)
            return;

        // Aim the weapon
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ownPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = (mousePos - ownPos).normalized;
        Vector3 spawnPos = ownPos + direction * spawnOffset;
        float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, degrees);

        if (fireTimer > 0.0f)
        {
            fireTimer -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && fireTimer <= 0.0)
        {
            // Spawn a bullet
            fireTimer = weapon.fireDelay;
            var bullet = Instantiate(weapon.bulletPrefab, weapon.bulletSpawn.position, weapon.bulletSpawn.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickup"))
        {
            PickupWeapon(collider.gameObject);
        }
    }

    public void PickupWeapon(GameObject weaponObject)
    {
        // TODO: Multiple inventory slots?
        if (weaponParent.childCount > 0)
        {
            Destroy(weaponParent.GetChild(0).gameObject);
        }

        weapon = weaponObject.GetComponent<Weapon>();
        weapon.transform.SetParent(weaponParent);
        weapon.transform.localPosition = Vector3.zero;
    }
}