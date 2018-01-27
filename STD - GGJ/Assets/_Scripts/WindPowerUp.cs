using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPowerUp : MonoBehaviour {
    public GameObject worldObject;

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement p1 = other.GetComponent<PlayerMovement>();

        List<GameObject> allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        PlayerMovement otherPlayer = allPlayers.Find(x => x.GetComponent<PlayerMovement>() != p1).GetComponent<PlayerMovement>();

        otherPlayer.myPowerUp = PlayerMovement.powerUp.WIND;

        Destroy(gameObject);
    }
}
