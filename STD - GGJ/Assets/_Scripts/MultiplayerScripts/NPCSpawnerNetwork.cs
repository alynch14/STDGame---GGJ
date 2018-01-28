using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerNetwork : NPCSpawner {


    public int DOCS_ONLY_MODE = 60;

    override public void Update()
    {
        if (play && PhotonNetwork.isMasterClient)
        {
             timer += Time.deltaTime;

            if (timer > POWERUP_TIMER)
            {
                int var = Random.Range(0, 9);
                if (var % 3 == 0)
                {
                    GameObject powerUp = (GameObject)PhotonNetwork.Instantiate("PowerUpNetworked", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 0);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.SPEED);
                }
                if (var % 3 == 1)
                {
                    GameObject powerUp = (GameObject)PhotonNetwork.Instantiate("PowerUpNetworked", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 0);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.WIND);
                }
                if (var % 3 == 2)
                {
                    GameObject powerUp = (GameObject)PhotonNetwork.Instantiate("PowerUpNetworked", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 0);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.INVINCIBLE);
                }

                timer = 0;
            }
        }
    }


    override public void OnPlay()
    {
        if (PhotonNetwork.isMasterClient)
        {
            play = true;

            foreach (AIWalk npc in npcList)
            {
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
            Debug.Log("playing");
        }
    }

    override public void OnGameOver()
    {

        play = false;

        int totalPoints = 0;
        int[] playerPoints = new int[4];
        int doctorPoints = 0;

        foreach (AIWalk npc in npcList)
        {

            switch (npc.GetInfectData())
            {

                case AIWalk.InfectData.NONE:
                    doctorPoints++;
                    break;

                case AIWalk.InfectData.PLAYER_1:
                    playerPoints[0]++;
                    break;

                case AIWalk.InfectData.PLAYER_2:
                    playerPoints[1]++;
                    break;

                case AIWalk.InfectData.PLAYER_3:
                    playerPoints[2]++;
                    break;

                case AIWalk.InfectData.PLAYER_4:
                    playerPoints[3]++;
                    break;

            }

            totalPoints++;

        }

        UI.GetComponent<UIControllerNetwork>().networkResultsData(doctorPoints, playerPoints, totalPoints);
        //GetComponent<PhotonView>().RPC("SetupResultsScreen", PhotonTargets.AllBufferedViaServer, doctorPoints, playerPoints, totalPoints);

    }



    override public void SpawnNPCs()
    {
        
        for (int i = 0; i < numNPCs; i++)
        {

            GameObject person = PhotonNetwork.Instantiate("NPCNetworked", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 0);
            person.transform.parent = objectContainer.transform;
            AIWalk npcScript = person.GetComponent<AIWalk>();
            npcScript.worldObject = gameObject;

            npcList.Add(npcScript);
        }

    }

    override public void SpawnDoctors()
    {

        for (int i = 0; i < (PhotonNetwork.playerList.Length == 1 ? DOCS_ONLY_MODE : numDoctors); i++)
        {
            GameObject person = PhotonNetwork.Instantiate("DoctorNetworked", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 0);
            person.transform.parent = objectContainer.transform;
            Doctor_AIScript docScript = person.GetComponent<Doctor_AIScript>();
            docScript.worldObject = gameObject;

            docsList.Add(docScript);
        }
    }

}
