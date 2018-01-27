using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public GameObject objectContainer;

    public GameObject npcObject;
    public int numNPCs = 100;
    public GameObject doctorObject;
    public const int DOCTOR_MAX = 2;

    public float worldMinX = -30;
    public float worldMaxX = 30;
    public float worldMinY = -30;
    public float worldMaxY = 30;
    public GameObject[] npcList = new GameObject[100];


	// Use this for initialization
	void Start () {
        SpawnNPCs();
        SpawnDoctors();
	}


    
    public void SpawnNPCs() {

        for (int i = 0; i < numNPCs; i++) {

            GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            person.GetComponent<AIWalk>().worldObject = gameObject;
            npcList[i] = person;
        }

    }

    public void SpawnDoctors()
    {
        GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
        person.GetComponent<Doctor_AIScript>().worldObject = gameObject;
    }


}
