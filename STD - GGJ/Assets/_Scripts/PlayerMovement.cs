using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Movement ids
    private const int MOVE_UP    = 0;
    private const int MOVE_RIGHT = 1;
    private const int MOVE_DOWN  = 2;
    private const int MOVE_LEFT  = 3;

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


    public float MOVEMENT_SPEED = 2;
    public float MOVEMENT_FRICTION = 3;
    public float MAX_SPEED = 1;

    public int playerNumber = 0;
    private KeyCode[] thisMovement;

    public float xVel = 0;
    public float yVel = 0;

    // Use this for initialization
    void Start () {

        if (thisMovement == null) {

            if (playerNumber == 0)
            {
                thisMovement = player1Movement;
            }
            else {
                thisMovement = player2Movement;
            }

        }

	}
	
	// Update is called once per frame
	void Update () {

        int verticalMovement = 0;
        int horizontalMovement = 0;

        if (Input.GetKey(thisMovement[MOVE_UP])) {
            verticalMovement += 1;
        }

        if (Input.GetKey(thisMovement[MOVE_DOWN])) {
            verticalMovement -= 1;
        }

        if (Input.GetKey(thisMovement[MOVE_RIGHT])) {
            horizontalMovement += 1;
        }

        if (Input.GetKey(thisMovement[MOVE_LEFT])) {
            horizontalMovement -= 1;
        }

        xVel += horizontalMovement * MOVEMENT_SPEED * Time.deltaTime;
        yVel += verticalMovement * MOVEMENT_SPEED * Time.deltaTime;

        if (horizontalMovement == 0) {
            xVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(xVel);
        }
        if (verticalMovement == 0) {
            yVel -= Time.deltaTime * MOVEMENT_FRICTION * Mathf.Sign(yVel);
        }

        if (Mathf.Abs(xVel) > MAX_SPEED) {
            xVel = MAX_SPEED * Mathf.Sign(xVel);
        }

        if (Mathf.Abs(xVel) < 0.2f && horizontalMovement == 0) {
            xVel = 0;
        }

        if (Mathf.Abs(yVel) > MAX_SPEED) {
            yVel = MAX_SPEED * Mathf.Sign(yVel);
        }

        if (Mathf.Abs(yVel) < 0.2f && verticalMovement == 0) {
            yVel = 0;
        }

        Vector3 move = (Vector3.up * yVel) + (Vector3.right * xVel);
        //move.Normalize();

        gameObject.transform.Translate(move);

    }
}
