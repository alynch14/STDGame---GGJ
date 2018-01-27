using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public Text timerText;

    float gameTimer;
    bool isPlaying = false;


    private void Update()
    {
        if (isPlaying) {

            gameTimer -= Time.deltaTime;

            // Thanks Yasin063 https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
            timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00");

        }
    }

    public void OnPlay()
    {
        isPlaying = true;
        gameTimer = 2 * 60;
    }

    public void OnGameEnd() {
        isPlaying = false;
    }
}
