using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanEnemy : MonoBehaviour
{
    public Transform Dest = null;
    public float MoveSpeed = 10.0f;
    Animation anim;
    CharacterController pcCtrl;
    bool isDead = false;
    bool isArrived = false;
    
    void Start()
    {
        anim = GetComponentInChildren<Animation>();
        pcCtrl = GetComponent<CharacterController>();
        anim.wrapMode = WrapMode.Loop;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && !isArrived)
            RunToDest();

        transform.rotation.Set(0, transform.rotation.y, transform.rotation.z, 1);
    }

    void RunToDest()
    {
        if(!anim.IsPlaying("run")) 
            anim.Play("run");

        if(Dest == null)
        {
            transform.LookAt(new Vector3(-12.5f, 1.2f, -17.0f));
        }
        else
        {
            transform.LookAt(Dest.position);
        }
        pcCtrl.Move(transform.forward * MoveSpeed * Time.deltaTime + Physics.gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            isDead = true;

            anim.wrapMode = WrapMode.Once;
            anim.Play("die");

            CharacterController collider = GetComponent<CharacterController>();
            collider.enabled = false;

            Destroy(gameObject, 3.0f);
        }

        if(!isArrived && other.name == "Destination")
        {
            isArrived = true;
            anim.wrapMode = WrapMode.Loop;
            anim.Play("attack");

            if (Dest == null)
            {
                transform.LookAt(new Vector3(-12.5f, 1.2f, -17.0f));
            }
            else
                transform.LookAt(Dest.position);
        }
    }
}
