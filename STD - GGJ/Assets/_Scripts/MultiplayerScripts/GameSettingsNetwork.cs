using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uCPf;

public class GameSettingsNetwork : GameSettings {


    public Color myColor;
    public int myNumber = 0;

	// Use this for initialization
	public override void Start () {

        player1Color = GameObject.Find("ColorPickerP1").GetComponent<ColorPicker>();
        player1Dropdown = GameObject.Find("DropdownP1").GetComponent<Dropdown>();

        playerColors = new Color[5];
        //playerColors[0] = P1_DEFAULT;

        player1Color.color = P1_DEFAULT;
        player1Color.UpdateUI();

    }

    override public void UpdateColors()
    {

        myColor = player1Color.color;

    }

    public void UpdateNetworkColor() {
       // Debug.Log("my player number is: " + myNumber + " and my color is: " + myColor.ToString());
        GetComponent<PhotonView>().RPC("NetworkedColors", PhotonTargets.AllBufferedViaServer, myNumber, myColor.r, myColor.g, myColor.b);
    }


    // defining a method that can be called by other clients:
    [PunRPC]
    public void NetworkedColors(int id, float r, float g, float b)
    {
        Debug.Log("Setting color for player: " + id + " :: " + r + "," + g + "," + b);
        playerColors[id] = new Color(r, g, b);
        //Debug.Log(string.Format("RPC: 'OnAwakeRPC' Parameter: {0} PhotonView: {1}", myParameter, this.photonView));
    }

}
