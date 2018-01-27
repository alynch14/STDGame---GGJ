using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {
    public GameObject worldObject;

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement p1 = other.GetComponent<PlayerMovement>();

        p1.myPowerUp = PlayerMovement.powerUp.SPEED;

        Destroy(gameObject);
    }
}
