using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEx : MonoBehaviour
{
    [Range(0, 50)]
    public float RayRange = 5.0f;

    private RaycastHit rayHit;
    private RaycastHit[] rayHits;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 frontPosition = new Vector3(transform.position.x, 
                                            transform.position.y, 
                                            transform.position.z + 5);
        ray = new Ray(frontPosition, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        RayUpdate();
        //Ray0();
        Ray2();
    }

    void RayUpdate()
    {
        //Vector3 frontPosition = new Vector3(transform.position.x,
        //                                    transform.position.y,
        //                                    transform.position.z);
        ray.origin = transform.position;
        ray.direction = transform.forward;
    }

    void Ray0()
    {
        if(Physics.Raycast(ray, out rayHit, RayRange))
        {
            Debug.Log(rayHit.collider.gameObject.name);
        }
    }

    void Ray1()
    {
        rayHits = Physics.RaycastAll(ray, RayRange);

        for(int index = 0; index < rayHits.Length; index++)
        {
            Debug.Log(rayHits[index].collider.gameObject.name);
        }
    }

    void Ray2()
    {
        rayHits = Physics.RaycastAll(ray, RayRange);

        for (int index = 0; index < rayHits.Length; index++)
        {
            if(rayHits[index].collider.gameObject.tag == "Enemy0")
            {
                Debug.Log(rayHits[index].collider.gameObject.name + "Tag");
            }

            if (rayHits[index].collider.gameObject.layer == LayerMask.NameToLayer("Test2"))
            {
                Debug.Log(rayHits[index].collider.gameObject.name + "Layer");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin, ray.direction * RayRange, Color.red);
    }
}
