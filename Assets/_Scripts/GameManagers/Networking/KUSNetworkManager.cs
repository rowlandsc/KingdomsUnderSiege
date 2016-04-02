using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KUSNetworkManager : NetworkManager {

    public static NetworkPlayerObject HostPlayer {
        get {
            PlayerController player = singleton.client.connection.playerControllers[0];
            return player.gameObject.GetComponent<NetworkPlayerObject>();
        }
    }

    public int Port = 48084;
	public InputField IPInputField;
	public Button HostGameButton;
	public Button JoinGameButton;
	public Button DisconnectButton;

    

    void Start() {
        FindStartButtons();
    }

	public void StartUpHost() {
        StopHost();
		SetPort();
		NetworkManager.singleton.StartHost();
	}

	public void JoinGame() {
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();
	}

	void SetIPAddress() {
		string ip = IPInputField.text;
		NetworkManager.singleton.networkAddress = ip;
	}

	void SetPort() {
		NetworkManager.singleton.networkPort = Port;
	}

	void SetUpMenuSceneButtons() {
		HostGameButton.onClick.RemoveAllListeners();
		HostGameButton.onClick.AddListener(StartUpHost);
		JoinGameButton.onClick.RemoveAllListeners();
		JoinGameButton.onClick.AddListener(JoinGame);
	}

	void SetUpSetUpSceneButtons() {
		DisconnectButton.onClick.RemoveAllListeners();
		DisconnectButton.onClick.AddListener(NetworkManager.singleton.StopHost);
	}

	void OnLevelWasLoaded(int level) {
		if (level == 0) {
            FindStartButtons();
		} else if (level == 1) {
			DisconnectButton = GameObject.Find("DisconnectButton").GetComponent<Button>();
			SetUpSetUpSceneButtons();
		}
	}

    void FindStartButtons() {
        HostGameButton = GameObject.Find("StartHostButton").GetComponent<Button>();
        JoinGameButton = GameObject.Find("JoinGameButton").GetComponent<Button>();
        IPInputField = GameObject.Find("IPInputField").GetComponent<InputField>();
        SetUpMenuSceneButtons();
    }
}
