using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public GameObject objectContainer;

    public GameObject npcObject;
    public int numNPCs = 100;

    public float worldMinX = -30;
    public float worldMaxX = 30;
    public float worldMinY = -30;
    public float worldMaxY = 30;


	// Use this for initialization
	void Start () {
        SpawnNPCs();
	}



    public void SpawnNPCs() {

        for (int i = 0; i < numNPCs; i++) {

            GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            person.GetComponent<AIWalk>().worldObject = gameObject;

        }

    }

}
