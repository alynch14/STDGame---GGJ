using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public GameObject objectContainer;

    public GameObject npcObject;
    public GameObject doctorObject;
    public int numNPCs = 100;
    public int numDoctors = 2;

    public float worldMinX = -30;
    public float worldMaxX = 30;
    public float worldMinY = -30;
    public float worldMaxY = 30;
    public List<AIWalk> npcList = new List<AIWalk>();


	// Use this for initialization
	void Start () {
        SpawnDoctors();
        SpawnNPCs();
	}


    
    public void SpawnNPCs() {

        for (int i = 0; i < numNPCs; i++) {

            GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            AIWalk npcScript = person.GetComponent<AIWalk>();
            npcScript.worldObject = gameObject;

            npcList.Add(npcScript);
        }

    }

    public void SpawnDoctors()
    {
        for (int i = 0; i < numDoctors; i++)
        {
            GameObject person = Instantiate(doctorObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            person.GetComponent<Doctor_AIScript>().worldObject = gameObject;
        }
    }


}
