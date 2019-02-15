using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photoninit : MonoBehaviour
{
    public bool ready = false;
    private void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("woophotonex");
    }
    public virtual void OnConnectedToMaster()
    {
        Debug.Log("connect master");
        PhotonNetwork.JoinRandomRoom();
    }
    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("no room");
        PhotonNetwork.CreateRoom("room");
    }
    public virtual void OnCreatedRoom()
    {
        Debug.Log("create");
    }
    public virtual void OnJoinedRoom()
    {
        Debug.Log("join room");
        StartCoroutine(Createplayer());
    }

    IEnumerator Createplayer()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), Quaternion.identity, 0);
        ready = true;
        yield return null;
    }
}