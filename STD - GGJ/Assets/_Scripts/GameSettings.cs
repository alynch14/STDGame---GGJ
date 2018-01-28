using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uCPf;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public static Color P1_DEFAULT = new Color(255, 200, 0);
    public static Color P2_DEFAULT = new Color(0, 75, 255);

    public Color[] playerColors;

    public ColorPicker player1Color;
    public ColorPicker player2Color;

    public Dropdown player1Dropdown;
    public Dropdown player2Dropdown;

    public int player1DiseaseOption = 0;
    public int player2DiseaseOption = 1;

    public string[] diseaseOptions = {
        "Influenza",
        "The Black Plague",
        "Tiberculosis",
        "Cholera",
        "Measles",
        "Ebola",
        "Malaria",
        "Strep Throat",
        "Ringworm",
        "West Nile Virus"
    };


	// Use this for initialization
	void Start () {

        playerColors = new Color[2];
        playerColors[0] = P1_DEFAULT;
        playerColors[1] = P2_DEFAULT;

        player1Color.color = P1_DEFAULT;
        player1Color.UpdateUI();

        player2Color.color = P2_DEFAULT;
        player2Color.UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateDiseaseOptions() {

        if (player1Dropdown.value != player2Dropdown.value)
        {
            player1DiseaseOption = player1Dropdown.value;
            player2DiseaseOption = player2Dropdown.value;
        } else
        {
            player1Dropdown.value = player1DiseaseOption;
            player2Dropdown.value = player2DiseaseOption;
        }

    }

    public void UpdateColors() {

        if (playerColors.Length >= 2)
        {
            playerColors[0] = player1Color.color;
            playerColors[1] = player2Color.color;
        }

    }
}
