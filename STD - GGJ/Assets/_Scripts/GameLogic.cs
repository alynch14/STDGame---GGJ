using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    Text timerText;

    float gameTimer;
    bool isPlaying = false;


    private void Update()
    {
        if (isPlaying) {

            gameTimer -= Time.deltaTime;

            //timerText = 

        }
    }

}
