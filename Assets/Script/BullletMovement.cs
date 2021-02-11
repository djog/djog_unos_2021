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


    void Update()
    {

    }
    void FixedUpdate()
    {
       
    }
}
