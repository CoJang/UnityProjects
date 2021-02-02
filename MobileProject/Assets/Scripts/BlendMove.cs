using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlendMove : MonoBehaviour, iAction
{
    float Speed = 0.0f;
    NavMeshAgent agent = null;
    Animator animator = null;
    Vector3 target;
    ActionManager actionManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        actionManager = GetComponent<ActionManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = agent.velocity;
        Vector3 local = transform.InverseTransformDirection(velocity);
        Speed = local.z;
        animator.SetFloat("Speed", Speed);
    }

    public void MoveTo(Vector3 dest)
    {
        agent.isStopped = false;
        agent.destination = dest;
    }

    public void Begin(object obj)
    {
        if(obj is Vector3) // 언박싱
        {
            actionManager.StartAction(this);
            Vector3 dest = (Vector3)obj;
            MoveTo(dest);
        }
    }

    public void End()
    {
        agent.isStopped = true;
    }
}
