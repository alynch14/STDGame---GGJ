using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

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


    public void Start()
    {
        MainMenu();
        ResultsScreen(false);
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

        player1Slider.value = (float)player1 / (player1 + player2 + doctor);
        player2Slider.value = (float)player2 / (player1 + player2 + doctor);

        string winText = "The Winner is: ";

        if (player1 > player2)
        {
            winText += "Player 1!";
        }
        else if (player2 > player1) {
            winText += "Player 2!";
        } else {
            winText += "The Doctors!";
        }

    }

}
