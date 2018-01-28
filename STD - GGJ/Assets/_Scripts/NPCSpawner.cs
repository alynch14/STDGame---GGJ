using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public GameObject UI;

    public GameObject objectContainer;

    public GameObject npcObject;
    public GameObject doctorObject;

    public int numNPCs = 100;
    public int numDoctors = 2;
    public GameObject powerUpObject;


    public float worldMinX = -30;
    public float worldMaxX = 30;
    public float worldMinY = -30;
    public float worldMaxY = 30;

    public List<AIWalk> npcList = new List<AIWalk>();
    public List<Doctor_AIScript> docsList = new List<Doctor_AIScript>();

    public bool play = false;

    public float POWERUP_TIMER = 15;
    public float timer = 0f;

    private void Start()
    {
        play = false;
    }


    public virtual void Update()
    {
        if (play)
        {
            timer += Time.deltaTime;

            if(timer > POWERUP_TIMER)
            {
                int var = Random.Range(0, 9);
                if (var%3 == 0)
                {
                    GameObject powerUp = Instantiate(powerUpObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.SPEED);
                }
                if(var % 3 == 1)
                {
                    GameObject powerUp = Instantiate(powerUpObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.WIND);
                }
                if(var % 3 == 2)
                {
                    GameObject powerUp = Instantiate(powerUpObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.INVINCIBLE);
                }

                timer = 0;
            }
        }
    }


    public virtual void OnPlay()
    {
        play = true;

        foreach (AIWalk npc in npcList) {
            Destroy(npc.gameObject);
        }
        foreach (Doctor_AIScript doc in docsList)
        {
            Destroy(doc.gameObject);
        }

        npcList = new List<AIWalk>();
        docsList = new List<Doctor_AIScript>();

        SpawnDoctors();
        SpawnNPCs();
    }

    public virtual void OnGameOver() {

        play = false;

        int player1Points = 0;
        int player2Points = 0;
        int doctorPoints  = 0;

        foreach (AIWalk npc in npcList) {

            switch (npc.GetInfectData()) {

                case AIWalk.InfectData.NONE:
                    doctorPoints++;
                    break;

                case AIWalk.InfectData.PLAYER_1:
                    player1Points++;
                    break;

                case AIWalk.InfectData.PLAYER_2:
                    player2Points++;
                    break;

            }

        }

        UI.GetComponent<UIController>().SetupResultsScreen(player1Points, player2Points, doctorPoints);

    }



    public virtual void SpawnNPCs() {

        for (int i = 0; i < numNPCs; i++) {

            GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            AIWalk npcScript = person.GetComponent<AIWalk>();
            npcScript.worldObject = gameObject;

            npcList.Add(npcScript);
        }

    }

    public virtual void SpawnDoctors()
    {
        for (int i = 0; i < numDoctors; i++)
        {
            GameObject person = Instantiate(doctorObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            Doctor_AIScript docScript = person.GetComponent<Doctor_AIScript>();
            docScript.worldObject = gameObject;

            docsList.Add(docScript);
        }
    }


}
