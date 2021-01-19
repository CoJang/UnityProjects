using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public float MoveSpeed = 0.15f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(-MoveSpeed, 0, 0));

        if(transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }
}
