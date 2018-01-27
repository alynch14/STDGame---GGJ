using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPowerUp : MonoBehaviour {
    public GameObject worldObject;

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement p1 = other.GetComponent<PlayerMovement>();

        Destroy(GameObject.Find("WindPowerUp"));
        p1.myPowerUp = PlayerMovement.powerUp.WIND;
    }
}
