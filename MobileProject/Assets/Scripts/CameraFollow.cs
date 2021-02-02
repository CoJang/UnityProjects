using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTarget = null;
    private Vector3 originPos = Vector3.zero; 
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        //FollowTarget = transform.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FollowTarget.position + originPos;
    }
}
