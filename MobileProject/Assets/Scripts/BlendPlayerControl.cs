using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BlendPlayerControl : MonoBehaviour//, iAction
{
    BlendMove move = null;
    BlendAttack attack = null;

    void Start()
    {
        move = GetComponent<BlendMove>();
        attack = GetComponent<BlendAttack>();
    }

    void Update()
    {
        if(Attacking())
        {
            return;
        }
        if (Moving())
        {
            return;
        }
    }

    bool Moving()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                move.Begin(hit.point);
                return true;
            }
        }

        return false;
    }

    bool Attacking()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            Damage damage = hit.transform.GetComponent<Damage>();
            if (damage == null) continue;

            if (Input.GetMouseButtonDown(0))
            {
                attack.Begin(damage);
            }
            return true;
        }
        return false;
    }

    Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
