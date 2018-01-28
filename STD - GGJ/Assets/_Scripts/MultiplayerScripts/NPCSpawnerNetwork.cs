using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerNetwork : NPCSpawner {

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
                    GameObject powerUp = PhotonNetwork.Instantiate("PowerUp", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 2);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.SPEED);
                }
                if (var % 3 == 1)
                {
                    GameObject powerUp = PhotonNetwork.Instantiate("PowerUp", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 2);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.WIND);
                }
                if (var % 3 == 2)
                {
                    GameObject powerUp = PhotonNetwork.Instantiate("PowerUp", new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, 2);
                    powerUp.GetComponent<PowerUp>().SetPowerupType(PowerUp.PowerType.INVINCIBLE);
                }

                timer = 0;
            }
        }
    }


    override public void OnPlay()
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
    }

    override public void OnGameOver()
    {

        play = false;

        int player1Points = 0;
        int player2Points = 0;
        int doctorPoints = 0;

        foreach (AIWalk npc in npcList)
        {

            switch (npc.GetInfectData())
            {

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



    override public void SpawnNPCs()
    {

        for (int i = 0; i < numNPCs; i++)
        {

            GameObject person = Instantiate(npcObject, new Vector3(Random.Range(worldMinX, worldMaxX), Random.Range(worldMinY, worldMaxY)), Quaternion.identity, objectContainer.transform);
            AIWalk npcScript = person.GetComponent<AIWalk>();
            npcScript.worldObject = gameObject;

            npcList.Add(npcScript);
        }

    }

    override public void SpawnDoctors()
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
