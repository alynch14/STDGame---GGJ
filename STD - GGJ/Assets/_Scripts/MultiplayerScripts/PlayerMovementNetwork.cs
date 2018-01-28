using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNetwork : PlayerMovement {


    // Use this for initialization
    override public void Start()
    {

        if (gameSettings == null)
        {
            gameSettings = GameObject.Find("_Scripts");
        }

        thisMovement = player2Movement;

        GetComponentInChildren<Camera>().gameObject.SetActive(GetComponent<PhotonView>().ownerId == PhotonNetwork.player.ID);


    }

    // for multiplayer
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();

        if (stream.isWriting)
        {
            int storePowerUp = (int)myPowerUp;
            stream.Serialize(ref storePowerUp);

            int pNum = playerNumber;
            stream.Serialize(ref pNum);
        }
        else
        {
            int updatePowerUp = 0;
            stream.Serialize(ref updatePowerUp);
            myPowerUp = (powerUp)updatePowerUp;

            int pNum = 5;
            stream.Serialize(ref pNum);
            playerNumber = pNum;
            //gameSettings.GetComponent<GameSettingsNetwork>().UpdateNetworkColor(pNum);
            

        }
    }


    override public void Update() {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();
        ren.material.color = ren.trailMaterial.color = gameSettings.GetComponent<GameSettings>().playerColors[playerNumber];


        if (GetComponent<PhotonView>().ownerId == PhotonNetwork.player.ID)
        {
            //THIS IS THE OWNER


            //update speeds
            float tempSpeed = MOVEMENT_SPEED;
            float tempMax = MAX_SPEED;
            if (myPowerUp != powerUp.NONE)
            {
                timer += Time.deltaTime;
            }

            if (timer > 5)
            {
                timer = 0;
                myPowerUp = powerUp.NONE;
            }

            if (myPowerUp == powerUp.SPEED)
            {
                tempSpeed += 1;
                tempMax += 1;
            }



            //MOVEMENT
            int verticalMovement = 0;
            int horizontalMovement = 0;

#if UNITY_STANDALONE
            if (Input.GetKey(thisMovement[MOVE_UP]))
            {
                verticalMovement += 1;
            }

            if (Input.GetKey(thisMovement[MOVE_DOWN]))
            {
                verticalMovement -= 1;
            }

            if (Input.GetKey(thisMovement[MOVE_RIGHT]))
            {
                horizontalMovement += 1;
            }

            if (Input.GetKey(thisMovement[MOVE_LEFT]))
            {
                horizontalMovement -= 1;
            }
#endif

            //Controls for Android devices
#if UNITY_ANDROID
        Touch touch = Input.GetTouch(0);

        if (!touch.Equals(null))
        {
            horizontalMovement = (int) (touch.deltaPosition.x);
            verticalMovement = (int)(touch.deltaPosition.y );
        }
#endif

            xVel += horizontalMovement * tempSpeed * Time.deltaTime;
            yVel += verticalMovement * tempSpeed * Time.deltaTime;

            if (horizontalMovement == 0)
            {
                xVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(xVel);
            }
            if (verticalMovement == 0)
            {
                yVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(yVel);
            }

            if (Mathf.Abs(xVel) > tempMax)
            {
                xVel = tempMax * Mathf.Sign(xVel);
            }

            if (Mathf.Abs(xVel) < 0.2f && horizontalMovement == 0)
            {
                xVel = 0;
            }

            if (Mathf.Abs(yVel) > tempMax)
            {
                yVel = tempMax * Mathf.Sign(yVel);
            }

            if (Mathf.Abs(yVel) < 0.2f && verticalMovement == 0)
            {
                yVel = 0;
            }



            Vector3 move = (Vector3.up * yVel) + (Vector3.right * xVel);
            //move.Normalize();



            gameObject.transform.Translate(move);

        }
        else {

            //THIS IS ANOTHER PLAYER


        }

        //BOTH

        if (myPowerUp == powerUp.WIND)
        {
            GetComponent<SphereCollider>().radius = PLAYER_WIND_RADIUS;
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();

            ParticleSystem.ShapeModule shape = ps.shape;
            shape.radius = PLAYER_PARTICLE_WIND_RADIUS;

            ParticleSystem.EmissionModule emit = ps.emission;
            emit.rateOverTime = PLAYER_EMIT_WIND;
        }
        else
        {
            GetComponent<SphereCollider>().radius = PLAYER_RADIUS;
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();

            ParticleSystem.ShapeModule shape = ps.shape;
            shape.radius = PLAYER_PARTICLE_WIND_RADIUS;

            ParticleSystem.EmissionModule emit = ps.emission;
            emit.rateOverTime = PLAYER_EMIT;
        }

    }



}
