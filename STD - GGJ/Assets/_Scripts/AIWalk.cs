using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalk : MonoBehaviour {

    public GameObject worldObject;
    NPCSpawner worldSpawner;

    public static float MIN_WAIT = 0f;
    public static float MAX_WAIT = 4f;

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
            
            target = new Vector3(Random.Range(worldSpawner.worldMinX, worldSpawner.worldMaxX), Random.Range(worldSpawner.worldMinY, worldSpawner.worldMaxY));

            UpdateMovement();

            RaycastHit hits;

            while (Physics.Raycast(gameObject.transform.position, direction, out hits, distance)) {

                target = new Vector3(Random.Range(worldSpawner.worldMinX, worldSpawner.worldMaxX), Random.Range(worldSpawner.worldMinY, worldSpawner.worldMaxY));

                UpdateMovement();

                Debug.Log("test");

            }

            //path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, target + Vector3.back });

            walking = true;

            wait += Random.Range(MIN_WAIT, MAX_WAIT);
            
        }

	}



    private void UpdateMovement() {

        heading = target - gameObject.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

    }

}
