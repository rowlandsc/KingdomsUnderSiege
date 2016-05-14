using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverseerUI : MonoBehaviour {

    public Text GoldText;

    private NetworkPlayerStats playerStats;

	void Start () {
        playerStats = KUSNetworkManager.LocalPlayer.GetComponent<NetworkPlayerStats>();
	}
	
	void Update () {
        GoldText.text = playerStats.Gold.ToString();
	}
}
