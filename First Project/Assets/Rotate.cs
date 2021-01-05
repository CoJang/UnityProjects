using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        //LookAt1();
    }

    // Update is called once per frame
    void Update()
    {
        Rot3();
    }

    void Rot0()
    {
        transform.eulerAngles = new Vector3(0, 45.0f, 0);
    }
    void Rot1()
    {
        Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        transform.rotation = target;
    }
    void Rot2()
    {
        transform.Rotate(Vector3.up * 60.0f);
    }
    void Rot3()
    {
        transform.rotation *= Quaternion.AngleAxis(10.0f * Time.deltaTime, Vector3.up);
    }
    void Rot4()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 30.0f * Time.deltaTime);
    }

    void Rot5()
    {
        //a,d 좌우 키 해당하는값 
        float y = Input.GetAxis("Horizontal");
        y = y * rotSpeed * Time.deltaTime;
        gameObject.transform.Rotate(new Vector3(0, y, 0));
    }

    public GameObject target = null;
    void LookAt()
    {
        if(target != null)
        {
            // 방향벡터를 만든다. 방향벡터 = [ 대상의 위치 - 내 위치 ]
            Vector3 dirToTarget = target.transform.position - transform.position;
            transform.forward = dirToTarget.normalized;
        }
    }

    void LookAt1()
    {
        if (target != null)
        {
            // 방향벡터를 만든다. 방향벡터 = [ 대상의 위치 - 내 위치 ]
            Vector3 dirToTarget = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.forward);
        }
    }
}
