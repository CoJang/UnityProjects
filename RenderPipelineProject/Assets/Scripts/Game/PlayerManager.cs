using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject topdownCamera;
    PhotonView PV;
    GameObject controller;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();

        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NoCamPlayerController"),
                        spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });

        GameObject newCam = Instantiate(topdownCamera, spawnPoint.position, Quaternion.Euler(60, 0, 0));

        newCam.GetComponent<CameraFollow>().SetTarget(controller.transform);
        controller.GetComponent<PlayerController>().BindPlayerCamera(newCam.GetComponentInChildren<Camera>());
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
