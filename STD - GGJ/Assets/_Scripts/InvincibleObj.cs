using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleObj : MonoBehaviour {
    public GameObject worldObject;

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement p1 = other.GetComponent<PlayerMovement>();

        Destroy(GameObject.Find("InvincibleObj"));
        p1.myPowerUp = PlayerMovement.powerUp.INVINCIBILITY;
    }
}
