using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalk : MonoBehaviour {

    public enum InfectData {

        PLAYER_1,
        PLAYER_2,
        PLAYER_3,
        PLAYER_4,

        NONE

    }

    public static float MIN_WAIT = 0f;
    public static float MAX_WAIT = 4f;

    public static float WALK_SPEED = 5;

    public Color NO_INFECT;
    public Color INFECT_P1;
    public Color INFECT_P2;

    //world stuff
    public GameObject worldObject;
    NPCSpawner worldSpawner;

    //this npc's features
    public InfectData infected = InfectData.NONE;

    //targeting
    public Vector3 target;
    public Vector3 direction;
    public Vector3 heading;
    public float distance;


    public float wait = 0;
    public bool walking = false;

    public LineRenderer path;

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
	public virtual void Update () {

        if (!walking) {
            wait -= Time.deltaTime;
        }
        else {

            gameObject.transform.Translate(direction * Time.deltaTime * WALK_SPEED);

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



    public virtual void GetNewTarget() {

        target = new Vector3(Random.Range(worldSpawner.worldMinX, worldSpawner.worldMaxX), Random.Range(worldSpawner.worldMinY, worldSpawner.worldMaxY));

        if (infected != InfectData.NONE) {

            target = gameObject.transform.position + ( new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5 );

        }

    }


    public void UpdateMovement() {

        heading = target - gameObject.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

    }

    public InfectData GetInfectData() {
        return infected;
    }

    public void Uninfect() {
        infected = InfectData.NONE;
        UpdateVisual();
    }

    public Vector3 GetDirection() {
        return direction;
    }

    public virtual void UpdateVisual() {

        GameSettings settings = worldObject.GetComponent<GameSettings>();
        INFECT_P1 = settings.playerColors[0] * 0.7f;
        INFECT_P2 = settings.playerColors[1] * 0.7f;

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

    public virtual void OnTriggerEnter(Collider other) {

        PlayerMovement playerTest = other.GetComponent<PlayerMovement>();

        if (playerTest != null) {
            //we are touching a player!
            List<GameObject> allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

            PlayerMovement otherPlayer = allPlayers.Find(x => x.GetComponent<PlayerMovement>() != playerTest).GetComponent<PlayerMovement>();
            
            if(otherPlayer.myPowerUp != PlayerMovement.powerUp.INVINCIBILITY || infected == InfectData.NONE){
                infected = playerTest.playerNumber == 0 ? InfectData.PLAYER_1 : InfectData.PLAYER_2;
                UpdateVisual();
            }
        }

    }

}
