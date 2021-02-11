using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private BoxCollider2D BulletCollider;
    public GameObject BulletPrefab;
    public float spawnOffset;
    void Start()
    {
        BulletCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            Vector2 spawnPosition = transform.position + direction.normalized * spawnOffset;
            var bullet = Instantiate(BulletPrefab, spawnPosition , Quaternion.LookRotation(direction,Vector3.forward));
          //  var bulletScript = bullet.GetComponent<BullletMovement>();
          //  bulletScript.direction
        }
    }
}