using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject creditsPage;
    public GameObject settingsPage;
    public GameObject gameUI;

    public GameObject resultsPage;

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
}
