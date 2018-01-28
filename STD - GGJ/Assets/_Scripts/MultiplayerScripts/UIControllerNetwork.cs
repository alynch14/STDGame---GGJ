using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerNetwork : UIController {

    public GameObject mainCam;

    override public void Start()
    {
        SettingsPage();
        ResultsScreen(false);

        // disease options
        GameSettings settings = gameSettings.GetComponent<GameSettings>();
        List<Dropdown.OptionData> diseaseOptions = new List<Dropdown.OptionData>();

        foreach (string s in settings.diseaseOptions)
        {
            diseaseOptions.Add(new Dropdown.OptionData(s));
        }

        player1Dropdown.options = diseaseOptions;
        player1Dropdown.value = settings.player1DiseaseOption;
    }

    override public void PlayGame()
    {
        settingsPage.SetActive(false);
        gameUI.SetActive(true);
        mainCam.SetActive(false);
    }

    override public void CreditsPage()
    {

    }

    override public void MainMenu()
    {

    }

    override public void SettingsPage()
    {
        settingsPage.SetActive(true);
        gameUI.SetActive(false);
    }
}
