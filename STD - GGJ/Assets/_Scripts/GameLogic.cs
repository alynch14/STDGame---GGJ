using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public GameObject UI;
    public GameObject spawner;

    public GameObject player1;
    public GameObject player2;

    public Vector3 player1Start = new Vector3(45, -20, 0);
    public Vector3 player2Start = new Vector3(-45, 20, 0);

    public Text timerText;

    public float GAME_TIME = 120;

    public float gameTimer;
    public bool isPlaying = false;


    public virtual void Update()
    {
        if (isPlaying) {

            gameTimer -= Time.deltaTime;

            // Thanks Yasin063 https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
            timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00");

            if (gameTimer <= 0) {

                OnGameEnd();

            }

        }
    }

    public virtual void OnPlay()
    {
        isPlaying = true;
        gameTimer = GAME_TIME;

        player1.transform.position = player1Start;
        player2.transform.position = player2Start;
    }

    public virtual void OnGameEnd() {
        isPlaying = false;

        UIController uic = UI.GetComponent<UIController>();
        uic.ResultsScreen(true);

        NPCSpawner ns = spawner.GetComponent<NPCSpawner>();
        ns.OnGameOver();

    }
}
