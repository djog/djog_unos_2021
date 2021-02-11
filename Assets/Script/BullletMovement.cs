using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletMovement : MonoBehaviour
{
    public float moveSpeed = 106;
    public Rigidbody2D rigidbody;
  
    void Start()
    {
        rigidbody.velocity = transform.forward * moveSpeed;
       
    }

    void OnCollisionEnter2D(Collision2D collision)
       
    {
        if (collision.gameObject.name != "Player")
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }


    void Update()
    {

    }

}
