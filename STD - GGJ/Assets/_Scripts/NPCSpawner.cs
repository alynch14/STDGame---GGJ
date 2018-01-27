using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public GameObject objectContainer;

    public GameObject npcObject;
    public GameObject doctorObject;

    public int numNPCs = 100;
    public int numDoctors = 2;
    public GameObject speedObject;
    public GameObject windObject;
    public GameObject invincibleObject;


    public float worldMinX = -30;
    public float worldMaxX = 30;
    public float worldMinY = -30;
    public float worldMaxY = 30;
    public List<AIWalk> npcList = new List<AIWalk>();
    public bool play = true;


	// Use this for initialization
	void Start () {
        SpawnDoctors();
        SpawnNPCs();
	}

    void Update()
    {
        if (play)
        {
            float timer = 0f;
            timer += Time.deltaTime;

            if(timer > 15)
            {
                int var = Random.Range(0, 9);
                if (var%3 == 0)
                {
                    GameObject powerUp = Instantiate(speedObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<SpeedBoost>().worldObject = gameObject;
                }
                if(var % 3 == 1)
                {
                    GameObject powerUp = Instantiate(windObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<WindPowerUp>().worldObject = gameObject;
                }
                if(var % 3 == 2)
                {
                    GameObject powerUp = Instantiate(invincibleObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<InvincibleObj>().worldObject = gameObject;
                }
            }
        }
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
