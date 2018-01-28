using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject gameSettings;

    public GameObject mainMenu;
    public GameObject creditsPage;
    public GameObject settingsPage;
    public GameObject gameUI;

    public GameObject resultsPage;

    //Results page stuff
    public Slider player1Slider;
    public Slider player2Slider;
    public Text winnerText;
    public Text player1Disease;
    public Text player2Disease;
    public Text player1Score;
    public Text player2Score;

    //settings page
    public Dropdown player1Dropdown;
    public Dropdown player2Dropdown;


    public void Start()
    {
        MainMenu();
        ResultsScreen(false);

        // disease options
        GameSettings settings = gameSettings.GetComponent<GameSettings>();
        List<Dropdown.OptionData> diseaseOptions = new List<Dropdown.OptionData>();

        foreach (string s in settings.diseaseOptions) {
            diseaseOptions.Add(new Dropdown.OptionData(s));
        }

        player1Dropdown.options = diseaseOptions;
        player1Dropdown.value = settings.player1DiseaseOption;
        player2Dropdown.options = diseaseOptions;
        player2Dropdown.value = settings.player2DiseaseOption;
    }

    public void PlayGame() {
        mainMenu.SetActive(false);
        creditsPage.SetActive(false);
        settingsPage.SetActive(false);
        gameUI.SetActive(true);
    }

    public void CreditsPage() {
        mainMenu.SetActive(false);
        creditsPage.SetActive(true);
        settingsPage.SetActive(false);
        gameUI.SetActive(false);
    }

    public void MainMenu() {
        mainMenu.SetActive(true);
        creditsPage.SetActive(false);
        settingsPage.SetActive(false);
        gameUI.SetActive(false);
    }

    public void SettingsPage() {
        mainMenu.SetActive(false);
        creditsPage.SetActive(false);
        settingsPage.SetActive(true);
        gameUI.SetActive(false);
    }

    public void ResultsScreen(bool show) {
        resultsPage.SetActive(show);
    }

    public void SetupResultsScreen(int player1, int player2, int doctor) {

        gameUI.SetActive(false);

        GameSettings settings = gameSettings.GetComponent<GameSettings>();

        ColorBlock p1 = player1Slider.colors;
        p1.normalColor = settings.playerColors[0];
        p1.highlightedColor = settings.playerColors[0];
        player1Slider.colors = p1;

        ColorBlock p2 = player2Slider.colors;
        p2.normalColor = settings.playerColors[1];
        p2.highlightedColor = settings.playerColors[1];
        player2Slider.colors = p2;

        player1Slider.value = (float)player1 / (player1 + player2 + doctor);
        player2Slider.value = (float)player2 / (player1 + player2 + doctor);

        player1Disease.text = settings.diseaseOptions[settings.player1DiseaseOption] + " transmitted " + player1 + " diseases.";
        player2Disease.text = settings.diseaseOptions[settings.player2DiseaseOption] + " transmitted " + player2 + " diseases.";

        string winText = "The Winner is: ";
        Color winColor = new Color(0, 0, 0);

        if (player1 > player2)
        {
            winText += settings.diseaseOptions[settings.player1DiseaseOption];
            winColor = settings.playerColors[0];
        }
        else if (player2 > player1) {
            winText += settings.diseaseOptions[settings.player2DiseaseOption];
            winColor = settings.playerColors[1];
        } else {
            winText = "You Tied!";
        }

        winnerText.text = winText;
        winnerText.color = winColor;

        player1Score.text = player1.ToString();
        player2Score.text = player2.ToString();

    }

}
