using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNPC_AIScript : MonoBehaviour {
    public Vector3 velocity;
    public float xVel;
    public int MAX_SPEED = 2;
    public int MOVEMENT_SPEED = 10;
    public float MOVEMENT_FRICTION = 0.8f;
    public float yVel;
    public int var;
    public float uVar;
    public float horizontalMovement;
    public float verticalMovement;


    // Use this for initialization
    void Start () {
        if(var == 0)
        {
            xVel = 4;
            yVel = 0;
        }

        else if (var == 1)
        {
            xVel = 0;
            yVel = 4;
        }

        velocity = new Vector3(xVel, yVel);
	}
	
	// Update is called once per frame
	void Update () {
        
        horizontalMovement = Random.Range(-1, 1);
        verticalMovement = Random.Range(-1, 1);

        xVel += horizontalMovement * MOVEMENT_SPEED * Time.deltaTime;
        yVel += verticalMovement * MOVEMENT_SPEED * Time.deltaTime;

        if (horizontalMovement == 0)
        {
            xVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(xVel);
        }
        if (verticalMovement == 0)
        {
            yVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(yVel);
        }

        if (Mathf.Abs(xVel) > MAX_SPEED)
        {
            xVel = MAX_SPEED * Mathf.Sign(xVel);
        }
        else if (Mathf.Abs(xVel) < 0.1f)
        {
            xVel = 0;
        }

        if (Mathf.Abs(yVel) > MAX_SPEED)
        {
            yVel = MAX_SPEED * Mathf.Sign(yVel);
        }
        else if (Mathf.Abs(yVel) < 0.1f)
        {
            yVel = 0;
        }


        Vector3 move = (Vector3.up * yVel) + (Vector3.right * xVel);
        //move.Normalize();

        gameObject.transform.Translate(move);
    }
}
