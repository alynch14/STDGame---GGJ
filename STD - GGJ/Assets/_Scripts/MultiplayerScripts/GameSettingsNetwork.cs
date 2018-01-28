using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsNetwork : GameSettings {

	// Use this for initialization
	public override void Start () {

        playerColors = new Color[4];
        playerColors[0] = P1_DEFAULT;

        player1Color.color = P1_DEFAULT;
        player1Color.UpdateUI();

    }

    public void UpdateColors()
    {

        if (playerColors.Length >= 2)
        {
            playerColors[0] = player1Color.color;
        }

    }

}
