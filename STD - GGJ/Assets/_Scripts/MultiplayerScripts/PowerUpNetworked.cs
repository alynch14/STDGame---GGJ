using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpNetworked : PowerUp {

    // for multiplayer
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();

        if (stream.isWriting)
        {

            int storePowerUp = (int)myPower;
            stream.Serialize(ref storePowerUp);
        }
        else
        {

            int updatePowerUp = 0;
            stream.Serialize(ref updatePowerUp);
            SetPowerupType((PowerType)updatePowerUp);

        }
    }


    public override void DestroyMe()
    {
        PhotonNetwork.Destroy(gameObject);
    }

}
