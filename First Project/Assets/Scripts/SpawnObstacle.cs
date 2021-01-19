using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject obstacleObj = null;
    public bool IsRandomPoint = false;
    public bool IsLifeTimeLimited = false;
    public bool IsXAxisOnly = false;
    public float range = 3.0f;
    public float LifeTime = 5.0f;

    [SerializeField]
    [Range(0, 30)]
    private float SpawnTime = 1.5f;

    IEnumerator Start()
    {
        while (true)
        {
            GameObject obj = null;
            if (obstacleObj != null)
            {
                if(IsXAxisOnly)
                    transform.position = new Vector3(Random.Range(-range, range),
                                                     transform.position.y,
                                                     transform.position.z);
                else if(!IsRandomPoint)
                    transform.position = new Vector3(transform.position.x,
                                                     Random.Range(-range, range),
                                                      transform.position.z);
                else if(IsRandomPoint)
                    transform.position = new Vector3(Random.Range(-range, range),
                                                     Random.Range(-range, range),
                                                      transform.position.z);

                obj = Instantiate(obstacleObj, transform.position, transform.rotation);
            }

            if(IsLifeTimeLimited)
                Destroy(obj, LifeTime);

            yield return new WaitForSeconds(SpawnTime);
        }
    }
}
