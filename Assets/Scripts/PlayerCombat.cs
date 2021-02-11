using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject BulletPrefab;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 derection = mousePos - transform.position;
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            var bullet = Instantiate(BulletPrefab, transform.position , Quaternion.LookRotation(derection,Vector3.forward));
          //  var bulletScript = bullet.GetComponent<BullletMovement>();
            //  bulletScript.direction
        }
    }
}