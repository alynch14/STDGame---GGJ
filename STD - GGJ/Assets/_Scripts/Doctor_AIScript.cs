using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor_AIScript : MonoBehaviour {
    public GameObject worldObject;
    NPCSpawner worldSpawner;
    public GameObject targetNPC;

    public static float MIN_WAIT = 0f;
    public static float MAX_WAIT = 4f;

    Vector3 target;
    Vector3 direction;
    Vector3 heading;
    Vector3 targetNPCWaypoint;
    float distance;
    Stack<Vector3> waypoints;

    float wait = 0;
    bool walking = false;

    LineRenderer path;

    // Use this for initialization
    void Start () {
        if (worldObject == null)
        {
            worldObject = GameObject.Find("_Scripts");
        }

        if (worldSpawner == null)
        {
            worldSpawner = worldObject.GetComponent<NPCSpawner>();
        }

        path = gameObject.AddComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        

        gameObject.transform.Translate(direction * Time.deltaTime * 5);

        UpdateMovement();

        if (distance < 1 || target == null)
        {
            walking = false;
            int x = Random.Range(0, worldSpawner.npcList.Length-1);

            targetNPC = worldSpawner.npcList[x];
        }

        

        target = targetNPC.transform.position;

        UpdateMovement();

        int counter = 0;
        RaycastHit hits;

        while (Physics.Raycast(gameObject.transform.position, direction, out hits, distance))
        {
            if (counter == 0)
            {
                target = hits.transform.position;
            }

            else
            {
                target += Vector3.Lerp(direction, hits.normal, 0.5f);

            }
            UpdateMovement();
            counter++;
        }

        path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, target + Vector3.back });



        

    }



    private void UpdateMovement()
    {

        heading = target - gameObject.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

    }
}

