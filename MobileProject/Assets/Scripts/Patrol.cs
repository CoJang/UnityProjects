using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public int GetNextIndex(int index)
    {
        return (index + 1) % transform.childCount;
    }

    public Vector3 GetWayPoint(int index)
    {
        return transform.GetChild(index).position;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            int next = GetNextIndex(i);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(GetWayPoint(i), 0.1f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(next));
        }
    }
}
