using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private float moveSpeed = 0.1f;

    void FixedUpdate()
    {
        Move(moveSpeed);
        Delete();
    }

    private void Move(float speed)
    {
        transform.Translate(speed * Vector3.left);
    }

    private void Delete()
    {
        if (transform.position.x < -100)
        {
            Destroy(gameObject);
        }
    }
}
