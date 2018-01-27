using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum powerUp
    {
        NONE,
        SPEED,
        WIND,
        INVINCIBILITY
    }

    // Movement ids
    private const int MOVE_UP = 0;
    private const int MOVE_RIGHT = 1;
    private const int MOVE_DOWN = 2;
    private const int MOVE_LEFT = 3;

    private static KeyCode[] player1Movement = new KeyCode[] {
        KeyCode.UpArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow
    };

    private static KeyCode[] player2Movement = new KeyCode[] {
        KeyCode.W,
        KeyCode.D,
        KeyCode.S,
        KeyCode.A
    };

    //speeds
    public float MOVEMENT_SPEED = 2;
    public float MOVEMENT_FRICTION = 3;
    public float MAX_SPEED = 1;

    //effects
    public float PLAYER_RADIUS = 5;
    public float PLAYER_WIND_RADIUS = 2;
    public float PLAYER_PARTICLE_RADIUS = 3;
    public float PLAYER_PARTICLE_WIND_RADIUS = 1.5f;
    public int PLAYER_EMIT_WIND = 50;
    public int PLAYER_EMIT = 1000;

    public int playerNumber = 0;
    private KeyCode[] thisMovement;

    public float xVel = 0;
    public float yVel = 0;

    public powerUp myPowerUp = powerUp.NONE;
    public float timer;


    // Use this for initialization
    void Start()
    {

        if (thisMovement == null)
        {

            if (playerNumber == 0)
            {
                thisMovement = player1Movement;
            }
            else
            {
                thisMovement = player2Movement;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        float tempSpeed = MOVEMENT_SPEED;
        float tempMax = MAX_SPEED;
        if(myPowerUp != powerUp.NONE)
        {
            timer += Time.deltaTime;
        }

        if(timer > 5)
        {
            timer = 0;
            myPowerUp = powerUp.NONE;
        }

        if (myPowerUp == powerUp.SPEED)
        {
            tempSpeed += 2;
            tempMax += 2;
        }

        if(myPowerUp == powerUp.WIND)
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

        int verticalMovement = 0;
        int horizontalMovement = 0;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SpeedBoost")
        {
            Destroy(GameObject.Find("SpeedBoost"));
            myPowerUp = powerUp.SPEED;
        }

        else if (other.gameObject.name == "WindPowerUp")
        {
            Destroy(GameObject.Find("WindPowerUp"));
            myPowerUp = powerUp.WIND;
        }

        else if(other.gameObject.name == "Invincibility")
        {
            Destroy(GameObject.Find("Invincibility"));
            myPowerUp = powerUp.INVINCIBILITY;
        }

        
        
    }

}