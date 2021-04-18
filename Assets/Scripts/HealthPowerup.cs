using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public float amount; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
       var colideObject = collider.gameObject; 
       if (colideObject.tag == "Player")
       {
         var x = colideObject.GetComponent<HealthController>().Health;
       }
    }
}
