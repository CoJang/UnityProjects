using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class NavEnemy : MonoBehaviour
{
    public GameObject TargetObject;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = TargetObject.transform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            other.gameObject.GetComponent<Coin>().OnCollecterPickUp();
        }
    }
}
