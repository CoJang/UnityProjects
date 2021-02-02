using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float rotSpeed = 5.0f;

    void Start()
    {
    }

    void Update()
    {
    }


    private void FixedUpdate()
    {
        RotateMove();
    }

    void RotateMove()
    {
        float rot = Input.GetAxis("Horizontal");
        float move = Input.GetAxis("Vertical");

        rot = rot * rotSpeed;
        move = move * moveSpeed;

        gameObject.transform.Rotate(Vector3.up * rot);
        gameObject.transform.Translate(Vector3.forward * move);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
