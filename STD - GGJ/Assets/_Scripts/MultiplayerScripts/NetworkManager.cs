using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    private const string VERSION = "v0.0.1";

    public string networkStatus = "";

    //byte playerGroup = 10;


	// Use this for initialization
	void Start () {

        PhotonNetwork.ConnectUsingSettings(VERSION);

    }


    public void findMatch() {

        Debug.Log("Find Match");

        PhotonNetwork.JoinRandomRoom();

    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("Join Room Failed");

        Debug.Log("Creating Room...");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(DateTime.Now.ToString("h:mm:ss tt") + UnityEngine.Random.Range(0, 100000), roomOptions, TypedLobby.Default);

    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("Random Join Failed");
        OnPhotonJoinRoomFailed();
    }

    void OnPhotonCreateRoomFailed() {
        Debug.Log("Create Room Failed");
        OnPhotonJoinRoomFailed();
    }

    void OnJoinedRoom() {
        Debug.Log("Joined Room!");

        GameObject mePlayer = (GameObject)PhotonNetwork.Instantiate("Player Networked", new Vector3(0, 0), Quaternion.identity, 0);
        mePlayer.GetComponent<PlayerMovement>().gameSettings = gameObject;
    }




}
