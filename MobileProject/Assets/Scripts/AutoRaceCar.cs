﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoRaceCar : Car
{
    [Range(0, 50)]
    public float RayRange = 5.0f;
    private Ray[] rays = new Ray[3];
    private RaycastHit[] rayHits = new RaycastHit[3];

    private float originSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            rays[i] = new Ray(transform.position, transform.forward);
        }

        originSpeed = moveSpeed;
        rigid = GetComponent<Rigidbody>();
        moveDir = transform.forward;
        isGameFinished = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RayUpdate();

        if(!isGameFinished)
        {
            RayProcess();
        }
    }

    void RayUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            rays[i].origin = transform.position;
        }
        rays[0].direction = transform.forward;
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        rays[1].direction = rot * transform.forward;
        rot = Quaternion.Euler(0, -45, 0);
        rays[2].direction = rot * transform.forward;
    }

    void RayProcess()
    {
        bool isLeftHit = false;
        bool isRightHit = false;
        bool isFrontHit = false;

        if (Physics.Raycast(rays[0], out rayHits[0], RayRange))
        {
            if(rayHits[0].collider.gameObject.tag == "Wall")
                isFrontHit = true;
        }

        if (Physics.Raycast(rays[1], out rayHits[1], RayRange))
        {
            if (rayHits[1].collider.gameObject.tag == "Wall")
                isLeftHit = true;
        }
        if (Physics.Raycast(rays[2], out rayHits[2], RayRange))
        {
            if (rayHits[2].collider.gameObject.tag == "Wall")
                isRightHit = true;
        }

        if (!isLeftHit && !isRightHit)
        {
            ResetWheels();
        }
        else if (isLeftHit && isRightHit)
        {
            if(rayHits[1].distance > rayHits[2].distance && isFrontHit)
            {
                Curve(false);
            }
            else if (rayHits[1].distance < rayHits[2].distance && isFrontHit)
            {
                Curve(true);
            }
        }
        else if(isLeftHit)
        {
            Curve(false);
        }
        else if(isRightHit)
        {
            Curve(true);
        }

        if(isFrontHit || isLeftHit || isRightHit)
        {
            rigid.velocity = rigid.velocity * 0.9f;
        }

        Move(true);
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(rays[0].origin, rays[0].direction * RayRange, Color.red);
        Debug.DrawRay(rays[1].origin, rays[1].direction * RayRange, Color.yellow);
        Debug.DrawRay(rays[2].origin, rays[2].direction * RayRange, Color.green);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            Debug.Log("Finish Line Exit!");
            lapCnt++;

            if(lapCnt == 3)
            {
                isGameFinished = true;
                raceScene.IsGameEnd = true;
            }
        }
    }
}
