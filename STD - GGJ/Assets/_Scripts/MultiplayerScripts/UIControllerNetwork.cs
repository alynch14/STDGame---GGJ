using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerNetwork : UIController {

    public GameObject mainCam;

    public GameObject resultParent;
    public GameObject playerResultPrefab;

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

    public void networkResults() {


        GetComponent<PhotonView>().RPC("ShowResultsScreen", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    public void ShowResultsScreen() {
        resultsPage.SetActive(true);
    }

    override public void SettingsPage()
    {
        settingsPage.SetActive(true);
        gameUI.SetActive(false);
    }

    public void networkResultsData(int doctor, int[] playerScores, int total) {
        GetComponent<PhotonView>().RPC("SetupResultsScreen", PhotonTargets.AllViaServer, doctor, playerScores, total);
    }

    [PunRPC]
    public void SetupResultsScreen(int doctor, int[] playerScores, int total)
    {

        gameUI.SetActive(false);

        GameSettings settings = gameSettings.GetComponent<GameSettings>();


        string winText = "The Winner is: ";
        Color winColor = new Color(0, 0, 0);

        int winScore = -1;
        int winnerId = 0;
        for (int i = 0; i < playerScores.Length; i++) {

            GameObject pResult = Instantiate(playerResultPrefab);
            pResult.transform.parent = resultParent.transform;

            pResult.GetComponentInChildren<Text>().text = playerScores[i].ToString();

            float percent = (float)playerScores[i] / total;

            RectTransform p = pResult.GetComponent<RectTransform>();
            p.sizeDelta = new Vector2( p.sizeDelta.x * percent, p.sizeDelta.y );
            p.GetComponentInChildren<Text>().alignment = (i % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.LowerCenter);

            pResult.GetComponent<Image>().color = settings.playerColors[i];

            if (playerScores[i] > winScore) {
                winnerId = i;
                winScore = playerScores[i];
            }
            

        }

        winText += settings.diseaseOptions[settings.player1DiseaseOption] + "(Player " + (winnerId+1) + ")";
        winColor = settings.playerColors[winnerId];


        winnerText.text = winText;
        winnerText.color = winColor;

    }
}
