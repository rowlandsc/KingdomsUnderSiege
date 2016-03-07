using UnityEngine;
using System.Collections.Generic;

public class NetPlayerDisplay : MonoBehaviour {

    public GameObject NetPlayer1;
    public GameObject NetPlayer2;
    public GameObject NetPlayer3;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        NetworkPlayerObject[] players = GameObject.FindObjectsOfType<NetworkPlayerObject>();

        bool found1 = false;
        bool found2 = false;
        bool found3 = false;
        foreach (NetworkPlayerObject player in players) {
            if (!player.isLocalPlayer) {
                GameObject netPlayer = null;
                if (!found1) {
                    found1 = true;
                    netPlayer = NetPlayer1;
                }
                else if (!found2) {
                    found2 = true;
                    netPlayer = NetPlayer2;
                }
                else if (!found3) {
                    found3 = true;
                    netPlayer = NetPlayer3;
                }
                else {
                    break;
                }

                if (!netPlayer.activeSelf) netPlayer.SetActive(true);
                UnityEngine.UI.Text text = netPlayer.transform.FindChild("Class").GetComponent<UnityEngine.UI.Text>();
                switch (player.Class) {
                    case NetworkPlayerObject.PlayerClass.OVERSEER:
                        text.text = "Class: Overseer";
                        break;
                    case NetworkPlayerObject.PlayerClass.ARCHER:
                        text.text = "Class: Archer";
                        break;
                    case NetworkPlayerObject.PlayerClass.KNIGHT:
                        text.text = "Class: Knight";
                        break;
                    case NetworkPlayerObject.PlayerClass.MAGE:
                        text.text = "Class: Mage";
                        break;
                }
            }
        }

        if (!found1 && NetPlayer1.activeSelf) NetPlayer1.SetActive(false);
        if (!found2 && NetPlayer2.activeSelf) NetPlayer2.SetActive(false);
        if (!found3 && NetPlayer3.activeSelf) NetPlayer3.SetActive(false);
    }
}
