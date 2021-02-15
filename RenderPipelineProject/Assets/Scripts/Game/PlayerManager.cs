using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject topdownCamera;
    [SerializeField] GameObject crossHairPrefab;

    PhotonView PV;
    GameObject controller;
    GameObject playerCamera;
    GameObject crossHair;

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

        playerCamera = Instantiate(topdownCamera, spawnPoint.position, Quaternion.Euler(60, 0, 0));
        //crossHair = Instantiate(crossHairPrefab, spawnPoint.position, Quaternion.Euler(60, 0, 0));

        playerCamera.GetComponent<CameraFollow>().SetTarget(controller.transform);
        controller.GetComponent<PlayerController>().BindPlayerCamera(playerCamera.GetComponentInChildren<Camera>());
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        Destroy(playerCamera);
        CreateController();
    }
}
