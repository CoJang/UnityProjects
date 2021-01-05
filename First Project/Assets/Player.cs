using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range (0.1f, 10)]
    private float moveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void Move()
    {
        float moveDelta = moveSpeed * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.z += moveDelta;
        transform.position = pos;
    }

    private void Move1()
    {
        float moveDelta = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * moveDelta);
    }

    void Move(Vector3 dir)
    {
        float moveDelta = moveSpeed * Time.deltaTime;
        transform.Translate(dir * moveDelta);
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Move(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.back);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right);
        }
    }
}
