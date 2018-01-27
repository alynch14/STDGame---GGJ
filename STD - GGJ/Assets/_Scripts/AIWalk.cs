using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalk : MonoBehaviour {

    public enum InfectData {

        PLAYER_1,
        PLAYER_2,
        NONE

    }

    public static float MIN_WAIT = 0f;
    public static float MAX_WAIT = 4f;

    public Color NO_INFECT;
    public Color INFECT_P1;
    public Color INFECT_P2;

    //world stuff
    public GameObject worldObject;
    NPCSpawner worldSpawner;

    //this npc's features
    InfectData infected = InfectData.NONE;

    //targeting
    Vector3 target;
    Vector3 direction;
    Vector3 heading;
    float distance;


    float wait = 0;
    bool walking = false;

    LineRenderer path;

    // Use this for initialization
    void Start () {

        if (worldObject == null) {
            worldObject = GameObject.Find("_Scripts");
        }

        if (worldSpawner == null) {
            worldSpawner = worldObject.GetComponent<NPCSpawner>();
        }

        path = gameObject.AddComponent<LineRenderer>();

        UpdateVisual();

    }
	
	// Update is called once per frame
	void Update () {

        if (!walking) {
            wait -= Time.deltaTime;
        }
        else {

            gameObject.transform.Translate(direction * Time.deltaTime * 5);

            UpdateMovement();

            if (distance < 1) {
                walking = false;
            }

        }

        if (wait <= 0 && !walking) {

            GetNewTarget();
            UpdateMovement();

            RaycastHit hits;

            while (Physics.Raycast(gameObject.transform.position, direction, out hits, distance)) {

                GetNewTarget();
                UpdateMovement();

            }

            //path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, target + Vector3.back });

            walking = true;

            wait += Random.Range(MIN_WAIT, MAX_WAIT);
            
        }

	}



    private void GetNewTarget() {

        target = new Vector3(Random.Range(worldSpawner.worldMinX, worldSpawner.worldMaxX), Random.Range(worldSpawner.worldMinY, worldSpawner.worldMaxY));

        if (infected != InfectData.NONE) {

            target = gameObject.transform.position + ( new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5 );

        }

    }


    private void UpdateMovement() {

        heading = target - gameObject.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

    }
    public Vector3 getDirection()
    {
        return direction;
    }

    public void UpdateVisual() {

        MeshRenderer renderer = gameObject.GetComponentInChildren<MeshRenderer>();

        switch (infected) {
            case InfectData.NONE:
                renderer.material.color = NO_INFECT;
                break;

            case InfectData.PLAYER_1:
                renderer.material.color = INFECT_P1;
                break;

            case InfectData.PLAYER_2:
                renderer.material.color = INFECT_P2;
                break;
        }

    }

    private void OnTriggerEnter(Collider other) {

        PlayerMovement playerTest = other.GetComponent<PlayerMovement>();

        if (playerTest != null) {
            //we are touching a player!
            List<GameObject> allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            allPlayers.Remove(playerTest.gameObject);
            
            if(allPlayers.ToArray()[0].GetComponent<PlayerMovement>().myPowerUp != PlayerMovement.powerUp.INVINCIBILITY){
                infected = playerTest.playerNumber == 0 ? InfectData.PLAYER_1 : InfectData.PLAYER_2;
                UpdateVisual();
            }
        }

    }

}
