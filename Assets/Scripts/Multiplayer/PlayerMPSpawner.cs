using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMPSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    //public Transform spawnPoint;
   public Transform[] spawnPoints; //--USE IF MORE THAN ONE SPAWN POINT IN OTHER GAMES--

    private void Start()
    {
         int randomNumber =  Random.Range(0, spawnPoints.Length);    //--UTILIZE IN OTHER GAMES--
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];

        //This passes to all active players the resources that we want to instantiate and that are located locally in the Resources folder (Photon needs to have such files in this folder to work)
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);


    }
}
