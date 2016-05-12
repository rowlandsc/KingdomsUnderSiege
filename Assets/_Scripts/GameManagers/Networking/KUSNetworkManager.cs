using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class KUSNetworkManager : NetworkManager {

    static NetworkPlayerObject _overseerPlayer = null;
    static NetworkPlayerObject _archerPlayer = null;
    static NetworkPlayerObject _knightPlayer = null;
    static NetworkPlayerObject _magePlayer = null;
    public static NetworkPlayerObject HostPlayer {
        get {
            PlayerController player = singleton.client.connection.playerControllers[0];
            return player.gameObject.GetComponent<NetworkPlayerObject>();
        }
    }
    public static NetworkPlayerObject LocalPlayer {
        get {
            List<PlayerController> playerControllers = singleton.client.connection.playerControllers;
            for (int i=0; i<playerControllers.Count; i++) {
                if (playerControllers[i].unetView.isLocalPlayer)
                    return playerControllers[i].gameObject.GetComponent<NetworkPlayerObject>();
            }
            return null;
        }
    }
    public static NetworkPlayerObject OverseerPlayer {
        get {
            if (_overseerPlayer) return _overseerPlayer;

            NetworkPlayerObject[] playerControllers = GameObject.FindObjectsOfType<NetworkPlayerObject>();
            for (int i = 0; i < playerControllers.Length; i++) {
                NetworkPlayerObject netobj = playerControllers[i];
                if (netobj.Class == NetworkPlayerObject.PlayerClass.OVERSEER) {
                    _overseerPlayer = netobj;
                    break;
                }
            }
            return _overseerPlayer;
        }
    }
    public static NetworkPlayerObject ArcherPlayer {
        get {
            if (_archerPlayer) return _archerPlayer;

            NetworkPlayerObject[] playerControllers = GameObject.FindObjectsOfType<NetworkPlayerObject>();
            for (int i = 0; i < playerControllers.Length; i++) {
                NetworkPlayerObject netobj = playerControllers[i];
                if (netobj.Class == NetworkPlayerObject.PlayerClass.ARCHER) {
                    _archerPlayer = netobj;
                    break;
                }
            }
            return _archerPlayer;
        }
    }
    public static NetworkPlayerObject KnightPlayer {
        get {
            if (_knightPlayer) return _knightPlayer;

            NetworkPlayerObject[] playerControllers = GameObject.FindObjectsOfType<NetworkPlayerObject>();
            for (int i = 0; i < playerControllers.Length; i++) {
                NetworkPlayerObject netobj = playerControllers[i];
                if (netobj.Class == NetworkPlayerObject.PlayerClass.KNIGHT) {
                    _knightPlayer = netobj;
                    break;
                }
            }
            return _knightPlayer;
        }
    }
    public static NetworkPlayerObject MagePlayer {
        get {
            if (_magePlayer) return _magePlayer;

            NetworkPlayerObject[] playerControllers = GameObject.FindObjectsOfType<NetworkPlayerObject>();
            for (int i = 0; i < playerControllers.Length; i++) {
                NetworkPlayerObject netobj = playerControllers[i];
                if (netobj.Class == NetworkPlayerObject.PlayerClass.MAGE) {
                    _magePlayer = netobj;
                    break;
                }
            }
            return _magePlayer;
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
