using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerObject : NetworkBehaviour {
    
    public enum PlayerType
	{
		OVERSEER,
        HERO,
    }

    public enum PlayerClass
	{
		OVERSEER,
        KNIGHT,
        ARCHER,
        MAGE,
    }

	[SyncVar]
    public PlayerType Type;
	[SyncVar]
    public PlayerClass Class;

	private Player _player = null;

	private UnityEngine.UI.Dropdown _typeDropdown;
	private UnityEngine.UI.Dropdown _classDropdown;
	private UnityEngine.UI.Button _startButton;

	private NetworkIdentity _networkIdentity;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		if (isLocalPlayer) {
			_typeDropdown = GameObject.Find("TypeDropdown").GetComponent<UnityEngine.UI.Dropdown>();
			_classDropdown = GameObject.Find("ClassDropdown").GetComponent<UnityEngine.UI.Dropdown>();

			_typeDropdown.onValueChanged.AddListener(OnTypeChange);
			_classDropdown.onValueChanged.AddListener(OnClassChange);
			
			_startButton = GameObject.Find("StartButton").GetComponent<UnityEngine.UI.Button>();
			if (isServer) {
				_startButton.onClick.AddListener(OnHitStartButton);
			}
			else {
				_startButton.gameObject.SetActive(false);
			}
		}

		_networkIdentity = GetComponent<NetworkIdentity>();
	}
	
	void Update () {
	
	}

	void OnTypeChange(int val) {
		CmdPlayerChangeType(_networkIdentity, (PlayerType) val);
	}

	void OnClassChange(int val) {
		CmdPlayerChangeClass(_networkIdentity, (PlayerClass) val);
	}

	void OnHitStartButton() {
		Debug.Log("Hit start button like a mofo");
		RpcStartGame();
	}

	void OnLevelWasLoaded(int level) {
		if (isLocalPlayer) {
			if (level == 2) {
				if (Type == PlayerType.OVERSEER) {
					_player = gameObject.AddComponent<Overseer> ();
				}
			} 
		}
	}
    [Command]
    public void CmdPlaceTower(string prefabID, Vector3 position) {  
        Debug.Log("Server Command Called " + prefabID + " " + position);
        Tower tower = GameObject.Instantiate(PrefabCache.Instance.PrefabIndex[prefabID]).GetComponent<Tower>();
        tower.transform.position = position;
        //tower.transform.rotation = transform.rotation;
        tower.GetComponent<MapCircleDrawer>().GameMap = Map.Instance;
        tower.GetComponent<MapCircleDrawer>().CircleRadius = tower.Radius;
        NetworkServer.Spawn(tower.gameObject);
    }

	[Command]
	public void CmdPlayerChangeType(NetworkIdentity player, NetworkPlayerObject.PlayerType newType) {
		player.GetComponent<NetworkPlayerObject>().Type = newType;
	}

	[Command]
	public void CmdPlayerChangeClass(NetworkIdentity player, NetworkPlayerObject.PlayerClass newClass) {
		player.GetComponent<NetworkPlayerObject>().Class = newClass;
	}

	[ClientRpc]
	public void RpcStartGame() {
		Application.LoadLevel(2);
	}
}
