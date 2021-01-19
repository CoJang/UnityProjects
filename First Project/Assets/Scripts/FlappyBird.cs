using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlappyBird : MonoBehaviour
{
    public float JumpPower = 5.0f;

    private Rigidbody rigid;
    private Quaternion originRotation;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigid.AddForce(new Vector3(0, JumpPower, 0));
            //transform.rotation = Quaternion.Euler(new Vector3(0, 90, 30));
        }
    }
}
