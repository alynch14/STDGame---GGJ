using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public Material windPowerup;
    public Material speedPowerup;
    public Material invinciblePowerup;
    public AudioClip windSFX;
    public AudioClip speedSFX;
    public AudioClip invincibleSFX;
    public AudioSource a = new AudioSource();


    public enum PowerType {
        WIND,
        SPEED,
        INVINCIBLE,
        NUM_TYPES
    }

    public PowerType myPower = PowerType.NUM_TYPES;

    public void SetPowerupType(PowerType newType) {
        myPower = newType;

        Renderer ren = GetComponent<Renderer>();

        switch (newType) {
            case PowerType.WIND:
                ren.material = windPowerup;
                break;

            case PowerType.SPEED:
                ren.material = speedPowerup;
                break;

            case PowerType.INVINCIBLE:
                ren.material = invinciblePowerup;
                break;

        }
    }


    void OnTriggerEnter(Collider other)
    {

        PlayerMovement p1 = other.GetComponent<PlayerMovement>();

        if (p1 == null)
            return;

        switch (myPower) {
            case PowerType.WIND:

                List<GameObject> allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

                PlayerMovement otherPlayer = allPlayers.Find(x => x.GetComponent<PlayerMovement>() != p1).GetComponent<PlayerMovement>();

                otherPlayer.myPowerUp = PlayerMovement.powerUp.WIND;

                //windSFX.LoadAudioData();
                //a.Play(windSFX.LoadAudioData());

                break;

            case PowerType.SPEED:
                p1.myPowerUp = PlayerMovement.powerUp.SPEED;

                //speedSFX.LoadAudioData();
                //a.Play();

                break;

            case PowerType.INVINCIBLE:
                p1.myPowerUp = PlayerMovement.powerUp.INVINCIBILITY;

                //invincibleSFX.LoadAudioData();
                //a.Play();

                break;
        }

        DestroyMe();
    }


    public virtual void DestroyMe() {
        Destroy(gameObject);
    }
}
