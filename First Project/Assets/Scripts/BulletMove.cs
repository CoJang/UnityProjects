using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float MoveSpeed = 0.1f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(MoveSpeed, 0, 0));

        if(transform.position.x > 25)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy0")
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy0")
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
