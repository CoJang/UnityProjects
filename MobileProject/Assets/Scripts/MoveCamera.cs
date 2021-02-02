using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCar;

    private Camera mainCamera;

    private void OnPreCull()
    {
        
    }

    private void OnPreRender()
    {
        
    }

    private void OnPostRender()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
    }

    void CameraControl()
    {
        // Drag Move
        if(Input.GetMouseButton(0))
        {
            transform.Translate(Input.GetAxisRaw("Mouse X") / 5.0f,
                                Input.GetAxisRaw("Mouse Y") / 5.0f,
                                0);
        }

        //// Drag Rotate
        //if (Input.GetMouseButton(1))
        //{
        //    transform.Rotate(0,
        //                    Input.GetAxisRaw("Mouse X") * 5.0f,
        //                    0);
        //}

        // Drag Rotate Around
        if (Input.GetMouseButton(1))
        {
            Vector3 point = PlayerCar.transform.position;
            Vector3 dir = Vector3.up;
            transform.RotateAround(point, dir, Input.GetAxisRaw("Mouse X") * 5.0f);
        }

        // Mouse wheel Zoom
        mainCamera.fieldOfView += (20.0f * Input.GetAxis("Mouse ScrollWheel"));
        if (mainCamera.fieldOfView < 10)
            mainCamera.fieldOfView = 10;

    }
}
