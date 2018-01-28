using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicNetwork : GameLogic {

    public float WAIT_TIME = 30;

    public bool isWaiting = false;

    public float networkTimer = 0;

    override public void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            base.Update();

            if (isWaiting)
            {

                gameTimer -= Time.deltaTime;

                // Thanks Yasin063 https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
                timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00");

                if (gameTimer < 0)
                {
                    isWaiting = false;
                    OnPlay();
                }
            }
        }
        else {

            gameTimer = Mathf.Lerp(gameTimer, networkTimer, 0.5f);

            // Thanks Yasin063 https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
            timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00");

        }
    }

    public void OnLobbyWait() {
        isPlaying = false;
        isWaiting = true;

        gameTimer = WAIT_TIME;
    }

    override public void OnPlay()
    {

        GetComponent<NPCSpawnerNetwork>().OnPlay();

        isPlaying = true;
        gameTimer = GAME_TIME;
    }

    override public void OnGameEnd()
    {
        isPlaying = false;

        UIController uic = UI.GetComponent<UIController>();
        uic.ResultsScreen(true);

        NPCSpawner ns = spawner.GetComponent<NPCSpawner>();
        ns.OnGameOver();

    }



    // for multiplayer
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();

        if (stream.isWriting)
        {
            float t = gameTimer;
            stream.Serialize(ref t);
        }
        else
        {

            float t = 0;
            stream.Serialize(ref t);
            networkTimer = t;
        }
    }


}
