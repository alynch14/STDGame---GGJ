using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor_AIScript : MonoBehaviour {
    public GameObject worldObject;
    NPCSpawner worldSpawner;

    public AIWalk targetNPC;

    public static float MIN_WAIT = 0f;
    public static float MAX_WAIT = 4f;

    public static float WALK_SPEED = 7;

    public Vector3 target;
    public Vector3 direction;
    public Vector3 heading;
    public float distance;

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
    void Update()
    {
        if (targetNPC != null)
        {

            if (distance < 2f)
            {
                targetNPC.Uninfect();
                ChooseNewTarget();
            }

            UpdateTargetLocation();

            gameObject.transform.Translate(direction * Time.deltaTime * 7);

            UpdateMovement();
        }
        else {

            ChooseNewTarget();

        }

    }


    private void ChooseNewTarget() {

        List<AIWalk> infected = worldSpawner.npcList.FindAll(x => x.GetInfectData() != AIWalk.InfectData.NONE);
            
        float testDistance = 1000;
        foreach(AIWalk npc in infected) {

            float d = Vector3.Distance(npc.transform.position, transform.position);
            if ( d < testDistance) {
                targetNPC = npc;
                testDistance = d;
            }

        }

        if (infected.Count == 0) {
            targetNPC = worldSpawner.npcList.ToArray()[Random.Range(0, worldSpawner.npcList.Count - 1)];
        }
    }

    private void UpdateTargetLocation() {
        target = targetNPC.transform.position;
        UpdateMovement();

        RaycastHit hits;

        if (Physics.Raycast(gameObject.transform.position, direction, out hits, distance)) {

            target = hits.point - Vector3.Slerp(direction, hits.normal, 0.5f)*5;
            target.z = 0;
            UpdateMovement();     

        }

        path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, target + Vector3.back, targetNPC.transform.position + Vector3.back });

    }



    private void UpdateMovement()
    {

        heading = target - gameObject.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

    }
}

