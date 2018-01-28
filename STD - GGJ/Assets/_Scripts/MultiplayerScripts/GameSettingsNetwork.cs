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

#pragma warning disable CS0114 // 'GameSettingsNetwork.UpdateColors()' hides inherited member 'GameSettings.UpdateColors()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
    public void UpdateColors()
#pragma warning restore CS0114 // 'GameSettingsNetwork.UpdateColors()' hides inherited member 'GameSettings.UpdateColors()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
    {

        if (playerColors.Length >= 2)
        {
            playerColors[0] = player1Color.color;
        }

    }

}
