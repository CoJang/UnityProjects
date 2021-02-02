using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CoinSpawner : MonoBehaviour
{
    public int range = 100;
    public GameObject coin;
    public bool IsLifeTimeLimited = false;
    public float LifeTime = 5.0f;
    public float SpawnTime = 5.0f;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        while(true)
        {
            agent.destination = new Vector3(Random.Range(0, range), 1, Random.Range(0, range));
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(agent.destination, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                GameObject temp = Instantiate(coin, gameObject.transform);

                temp.transform.localPosition = agent.destination;
                break;
            }
        }
    }

    void Update()
    {

    }

    // 구체를 생성 후 네비매쉬가 충돌하면 가장 가까운 지점을 반환해줌.
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        
        result = Vector3.zero;
        return false;
    }

    public void RepositionCoin(GameObject coin)
    {
        while(true)
        {
            agent.destination = new Vector3(Random.Range(0, range), 1, Random.Range(0, range));
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(agent.destination, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                coin.transform.localPosition = agent.destination;
                break;
            }
        }
    }
}
