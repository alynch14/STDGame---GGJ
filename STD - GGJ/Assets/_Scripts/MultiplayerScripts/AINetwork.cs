using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINetwork : AIWalk {


    public Color[] infectColors;


    // Update is called once per frame
    override public void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {

            if (!walking)
            {
                wait -= Time.deltaTime;
            }
            else
            {

                gameObject.transform.Translate(direction * Time.deltaTime * WALK_SPEED);

                UpdateMovement();

                if (distance < 1)
                {
                    walking = false;
                }

            }

            if (wait <= 0 && !walking)
            {

                GetNewTarget();
                UpdateMovement();

                RaycastHit hits;

                while (Physics.Raycast(gameObject.transform.position, direction, out hits, distance))
                {

                    GetNewTarget();
                    UpdateMovement();

                }

                //path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, target + Vector3.back });

                walking = true;

                wait += Random.Range(MIN_WAIT, MAX_WAIT);

            }

        }

    }

    override public void UpdateVisual()
    {

        if (worldObject == null)
        {
            worldObject = GameObject.Find("_Scripts");
        }

        GameSettings settings = worldObject.GetComponent<GameSettings>();
        infectColors = settings.playerColors;

        MeshRenderer renderer = gameObject.GetComponentInChildren<MeshRenderer>();

        switch (infected)
        {
            case InfectData.NONE:
                renderer.material.color = NO_INFECT;
                break;

            case InfectData.PLAYER_1:
                renderer.material.color = infectColors[0] * 0.7f;
                break;

            case InfectData.PLAYER_2:
                renderer.material.color = infectColors[1] * 0.7f;
                break;

            case InfectData.PLAYER_3:
                renderer.material.color = infectColors[2] * 0.7f;
                break;

            case InfectData.PLAYER_4:
                renderer.material.color = infectColors[3] * 0.7f;
                break;
        }

    }

    override public void OnTriggerEnter(Collider other)
    {

        if (PhotonNetwork.isMasterClient)
        {
            PlayerMovement playerTest = other.GetComponent<PlayerMovement>();

            if (playerTest != null)
            {

                //we are touching a player!
                List<GameObject> allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

                List<GameObject> otherPlayers = allPlayers.FindAll(x => x.GetComponent<PlayerMovement>() != playerTest);
                foreach (GameObject go in otherPlayers)
                {
                    PlayerMovement p = go.GetComponent<PlayerMovement>();

                    if (p.myPowerUp != PlayerMovement.powerUp.INVINCIBILITY || infected == InfectData.NONE)
                    {
                        setInfect(playerTest.playerNumber);
                    }

                }

                if (otherPlayers.Count == 0) {
                    setInfect(playerTest.playerNumber);
                }

            }

        }

    }

    public void setInfect(int playerNum) {
        switch (playerNum)
        {
            case 0:
                infected = InfectData.PLAYER_1;
                break;
            case 1:
                infected = InfectData.PLAYER_2;
                break;
            case 2:
                infected = InfectData.PLAYER_3;
                break;
            case 3:
                infected = InfectData.PLAYER_4;
                break;
        }
        UpdateVisual();
    }

    // for multiplayer
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();

        if (stream.isWriting)
        {

            int storePowerUp = (int)infected;
            stream.Serialize(ref storePowerUp);
            UpdateVisual();
        }
        else
        {

            int updatePowerUp = 0;
            stream.Serialize(ref updatePowerUp);
            infected = (InfectData)updatePowerUp;
            UpdateVisual();

        }
    }

}
