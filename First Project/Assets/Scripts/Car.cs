using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 20)]
    protected float moveSpeed = 3.0f;

    [SerializeField]
    private GameObject[] Wheels = new GameObject[4];
    [SerializeField]
    protected float Sensitivity = 1.0f;

    private float wheelAngle = 0;
    private float wheelMaxAngle = 50.0f;
    private Vector3 moveDir = Vector3.forward;

    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Curve(false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Curve(true);
        }
    }

    protected void Move(bool isForward)
    {
        int drive = -1;
        if (isForward) drive = 1;

        Quaternion rot = Quaternion.Euler(0, (wheelAngle * drive) * Time.deltaTime, 0);
        moveDir = rot * moveDir;
        transform.position += (moveDir * moveSpeed * Time.deltaTime) * drive;
        transform.rotation *= rot;

        foreach (GameObject wheels in Wheels)
        {
            wheels.transform.Rotate(Vector3.up, moveSpeed, Space.Self);
        }
    }

    protected void Curve(bool isRight)
    {
        int dir = -1;
        if (isRight) dir = 1;
        if (Mathf.Abs(wheelAngle) < wheelMaxAngle)
        {
            wheelAngle += Sensitivity * dir;
            Mathf.Clamp(wheelAngle, -wheelMaxAngle, wheelMaxAngle);
            Wheels[0].transform.Rotate(Vector3.up, Sensitivity * dir, Space.World);
            Wheels[1].transform.Rotate(Vector3.up, Sensitivity * dir, Space.World);
        }
    }

    protected void ResetWheels()
    {
        if(wheelAngle > 0)
        {
            wheelAngle -= Sensitivity * 3.0f;
            if (wheelAngle < 0) wheelAngle = 0;
            Wheels[0].transform.Rotate(Vector3.up, -Sensitivity * 3.0f, Space.World);
            Wheels[1].transform.Rotate(Vector3.up, -Sensitivity * 3.0f, Space.World);
        }
        else if (wheelAngle < 0)
        {
            wheelAngle += Sensitivity * 3.0f;
            if (wheelAngle > 0) wheelAngle = 0;
            Wheels[0].transform.Rotate(Vector3.up, Sensitivity * 3.0f, Space.World);
            Wheels[1].transform.Rotate(Vector3.up, Sensitivity * 3.0f, Space.World);
        }
    }
}
