using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

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

	public bool CheckLocalPlayer {
		get {
			return isLocalPlayer;
		}
	}


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
				StartCoroutine(SpawnPlayer());
			} 
		}
	}



	IEnumerator SpawnPlayer() {
		while (NetworkSpawnManagers.Instance && !NetworkSpawnManagers.Instance.DoneLoading) {
			yield return null;
		}

		if (Type == PlayerType.OVERSEER) {
			_player = gameObject.AddComponent<Overseer> ();
		}
		else if(Type == PlayerType.HERO) {
			if(Class == PlayerClass.MAGE){
				_player = gameObject.AddComponent<HeroMage>();
				PlayerController player = NetworkManager.singleton.client.connection.playerControllers[0];
				player.gameObject.GetComponent<NetworkPlayerObject>().CmdCreateHeroMagePlayer(_networkIdentity);

			}
			if(Class == PlayerClass.KNIGHT){
				_player = gameObject.AddComponent<HeroKnight>();
				PlayerController player = NetworkManager.singleton.client.connection.playerControllers[0];
				player.gameObject.GetComponent<NetworkPlayerObject>().CmdCreateHeroKnightPlayer(_networkIdentity);

			}
			if(Class == PlayerClass.ARCHER){
				_player = gameObject.AddComponent<HeroArch>();
				PlayerController player = NetworkManager.singleton.client.connection.playerControllers[0];
				player.gameObject.GetComponent<NetworkPlayerObject>().CmdCreateHeroArchPlayer(_networkIdentity);

			}
		}
	}


    [Command]
    public void CmdPlaceTower(string prefabID, Vector3 position) {  
        Debug.Log("Server Command Called " + prefabID + " " + position);
        Tower tower = GameObject.Instantiate(PrefabCache.Instance.PrefabIndex[prefabID]).GetComponent<Tower>();
        tower.transform.position = position;
        //tower.transform.rotation = transform.rotation;
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

	[Command]
	public void CmdCreateHeroMagePlayer(NetworkIdentity player) {
		Debug.Log("Server Command Called create mage player");
		GameObject birthplace = GameObject.Find("MageSummonPoint");
		GameObject mage_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroMage"], birthplace.transform.position, Quaternion.identity)as GameObject;
        mage_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(mage_Clone);

        RpcSetOwner(mage_Clone.GetComponent<NetworkIdentity>(), player);
	}

	[Command]
	public void CmdCreateHeroKnightPlayer(NetworkIdentity player) {
		Debug.Log("Server Command Called create knight player");
		GameObject birthplace = GameObject.Find("KnightSummonPoint");
		GameObject knight_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroKnight"], birthplace.transform.position, Quaternion.identity)as GameObject;
        knight_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(knight_Clone);

        RpcSetOwner(knight_Clone.GetComponent<NetworkIdentity>(), player);
    }

	[Command]
	public void CmdCreateHeroArchPlayer(NetworkIdentity player) {
		Debug.Log("Server Command Called create arch player");
		GameObject birthplace = GameObject.Find("ArchSummonPoint");
		GameObject arch_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroArch"], birthplace.transform.position, Quaternion.identity)as GameObject;
        arch_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(arch_Clone);

        RpcSetOwner(arch_Clone.GetComponent<NetworkIdentity>(), player);
	}


	[ClientRpc]
	public void RpcStartGame() {
        if (SceneManager.GetActiveScene().name == "SetupScreen")
            SceneManager.LoadScene("GameScreen");
        else
            SceneManager.LoadScene("NetworkingTestScene");
	}

    [ClientRpc]
    public void RpcSetOwner(NetworkIdentity obj, NetworkIdentity owner) {
        Debug.Log("Setting network player owner of " + obj.gameObject.name + " to " + owner.gameObject.name);
        obj.gameObject.GetComponent<NetworkPlayerOwner>().Owner = owner.gameObject.GetComponent<NetworkPlayerObject>();
        if (owner.connectionToClient != null)
            Debug.Log("Assigned authority? " + obj.AssignClientAuthority(owner.connectionToClient));
        else
            Debug.Log("Assigned authority? " + obj.AssignClientAuthority(owner.connectionToServer));
    }
}
