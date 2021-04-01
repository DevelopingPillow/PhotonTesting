using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Debug.Log("passed by here");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(0, 2, 0), Quaternion.identity);
    }
}
