using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject TargetObject;
    public float RotSpeed = 480.0f;

    void Start()
    {
        TargetObject = GameObject.Find("coin3D Variant(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetObject == null) 
            TargetObject = GameObject.Find("coin3D Variant(Clone)");

        transform.LookAt(TargetObject.transform.position);
    }
}
