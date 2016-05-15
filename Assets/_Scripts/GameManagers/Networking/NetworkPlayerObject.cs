﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

		if (Class == PlayerClass.OVERSEER) {
			_player = gameObject.AddComponent<Overseer> ();
		}
		else if(Class == PlayerClass.MAGE){
			_player = gameObject.AddComponent<HeroMage>();
			KUSNetworkManager.HostPlayer.CmdCreateHeroMagePlayer(_networkIdentity);

		}
		if(Class == PlayerClass.KNIGHT){
			_player = gameObject.AddComponent<HeroKnight>();
			KUSNetworkManager.HostPlayer.CmdCreateHeroKnightPlayer(_networkIdentity);

		}
		if(Class == PlayerClass.ARCHER){
			_player = gameObject.AddComponent<HeroArch>();
			KUSNetworkManager.HostPlayer.CmdCreateHeroArchPlayer(_networkIdentity);
		}
	}


    [Command]
    public void CmdPlaceTower(NetworkIdentity player, string prefabID, Vector3 position, Quaternion rotation) { 
        Tower tower = GameObject.Instantiate(PrefabCache.Instance.PrefabIndex[prefabID]).GetComponent<Tower>();
        tower.transform.position = position;
        tower.transform.rotation = rotation;
        player.gameObject.GetComponent<NetworkPlayerStats>().PurchaseItem(PrefabCache.Instance.PrefabIndex[prefabID].GetComponent<ProfileSystem>().Worth);
        //tower.transform.rotation = transform.rotation;
        NetworkServer.Spawn(tower.gameObject);

        RpcSetOwner(tower.GetComponent<NetworkIdentity>(), player);
        player.gameObject.GetComponent<NetworkPlayerStats>().AddTowerPlaced();
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
		GameObject birthplace = GameObject.Find("MageSummonPoint");
		GameObject mage_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroMage"], birthplace.transform.position, Quaternion.identity)as GameObject;
        mage_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(mage_Clone);
        if (player.connectionToClient != null)
            mage_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToClient);
        else
            mage_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToServer);

        RpcSetOwner(mage_Clone.GetComponent<NetworkIdentity>(), player);
	}

	[Command]
	public void CmdCreateHeroKnightPlayer(NetworkIdentity player) {
		GameObject birthplace = GameObject.Find("KnightSummonPoint");
		GameObject knight_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroKnight"], birthplace.transform.position, Quaternion.identity)as GameObject;
        knight_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(knight_Clone);
        if (player.connectionToClient != null)
            knight_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToClient);
        else
            knight_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToServer);

        RpcSetOwner(knight_Clone.GetComponent<NetworkIdentity>(), player);
    }

	[Command]
	public void CmdCreateHeroArchPlayer(NetworkIdentity player) {
		GameObject birthplace = GameObject.Find("ArchSummonPoint");
		GameObject arch_Clone = Instantiate(PrefabCache.Instance.PrefabIndex["HeroArch"], birthplace.transform.position, Quaternion.identity)as GameObject;
        arch_Clone.gameObject.GetComponent<NetworkPlayerOwner>().Owner = player.gameObject.GetComponent<NetworkPlayerObject>();

        NetworkServer.Spawn(arch_Clone);
        if (player.connectionToClient != null)
            arch_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToClient);
        else
           arch_Clone.GetComponent<NetworkIdentity>().AssignClientAuthority(player.connectionToServer);

        RpcSetOwner(arch_Clone.GetComponent<NetworkIdentity>(), player);
	}

    [Command]
    public void CmdAddProfileEffect(NetworkIdentity obj, ProfileEffect effect) {
        if(obj) obj.GetComponent<ProfileSystem>().AddEffect(effect);
    }

    [Command]
    public void CmdArcherMelee(NetworkIdentity archer, Vector3 position, Quaternion rotation, Vector3 velocity) {
        GameObject arrow_clone = Instantiate(PrefabCache.Instance.PrefabIndex["ArcherMeleeObj"], position, rotation) as GameObject;
        arrow_clone.GetComponent<ArchMeleeHit>().Initialize(archer);
        arrow_clone.GetComponent<ArchMeleeHit>().velocity = velocity;
        NetworkServer.Spawn(arrow_clone);
    }

    [Command]
    public void CmdArcherSecond(NetworkIdentity archer, Vector3 position, Quaternion rotation) {
        GameObject proj_clone = Instantiate(PrefabCache.Instance.PrefabIndex["ArcherSecondObj"], position, rotation) as GameObject;
        proj_clone.GetComponent<ArchSecondHit>().Initialize(archer);
        NetworkServer.Spawn(proj_clone);
    }

    [Command]
    public void CmdArcherSuperStart(NetworkIdentity archer, Vector3 position, Quaternion rotation) {
        GameObject fx_clone = Instantiate(PrefabCache.Instance.PrefabIndex["ArcherSuperFXObj"], position, rotation) as GameObject;
        fx_clone.transform.SetParent(archer.transform);
        fx_clone.name = "ArcherSuperFx";
        NetworkServer.Spawn(fx_clone);
    }

    [Command]
    public void CmdArcherSuperDestroy() {
        GameObject superfx = GameObject.Find("ArcherSuperFx");
        if (superfx != null) Destroy(superfx);
    }

    [Command]
    public void CmdArcherSuper(NetworkIdentity archer, Vector3 position, Quaternion rotation, Vector3 velocity) {
        GameObject proj_clone = Instantiate(PrefabCache.Instance.PrefabIndex["ArcherSuperObj"], position, rotation) as GameObject;
        proj_clone.GetComponent<ArchSuperHit>().Initialize(archer);
        proj_clone.GetComponent<ArchSuperHit>().velocity = velocity;
        NetworkServer.Spawn(proj_clone);
    }

    [Command]
    public void CmdKnightMelee(NetworkIdentity knight, Vector3 position, Quaternion rotation) {
        GameObject knightMelee = Instantiate(PrefabCache.Instance.PrefabIndex["KnightMeleeObj"], position, rotation) as GameObject;
        knightMelee.GetComponent<KnightMeleeHit>().Initialize(knight);
        knightMelee.transform.parent = knight.transform;
        knightMelee.AddComponent<DestoryAfter3second>();
        NetworkServer.Spawn(knightMelee);
    }

    [Command]
    public void CmdKnightSecond(NetworkIdentity knight, Vector3 position, Quaternion rotation) {
        GameObject knightSecond = Instantiate(PrefabCache.Instance.PrefabIndex["KnightSecondObj"], position, rotation) as GameObject;
        knightSecond.GetComponent<KnightSecondHit>().Initialize(knight);
        knightSecond.transform.parent = knight.transform;
        knightSecond.name = "KnightSecondFX";
        NetworkServer.Spawn(knightSecond);
    }

    [Command]
    public void CmdKnightSecondDestroy() {
        GameObject secondfx = GameObject.Find("KnightSecondFX");
        if (secondfx != null) Destroy(secondfx);
    }

    [Command]
    public void CmdKnightSuper(NetworkIdentity knight, Vector3 position, Quaternion rotation) {
        GameObject knightSuper = Instantiate(PrefabCache.Instance.PrefabIndex["KnightSuperObj"], position, rotation) as GameObject;
        knightSuper.transform.parent = knight.transform;
        knightSuper.name = "KnightSuperFX";
        NetworkServer.Spawn(knightSuper);
    }

    [Command]
    public void CmdKnightSuperDestroy() {
        GameObject superfx = GameObject.Find("KnightSuperFX");
        if (superfx != null) Destroy(superfx);
    }


    [Command]
    public void CmdMageMelee(NetworkIdentity mage, Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        GameObject iceball_clone = Instantiate(PrefabCache.Instance.PrefabIndex["MageMeleeObj"], position, rotation) as GameObject;
        iceball_clone.GetComponent<Meleehit>().Initialize(mage);
        iceball_clone.GetComponent<Meleehit>().velocity = velocity;
        NetworkServer.Spawn(iceball_clone);
    }

    [Command]
    public void CmdMageSecond(NetworkIdentity mage, Vector3 position, Quaternion rotation, RaycastHit hitpoint)
    {
        GameObject icebullet_clone1 = Instantiate(PrefabCache.Instance.PrefabIndex["MageSecondObj"], position, rotation) as GameObject;
        icebullet_clone1.GetComponent<SecondHit>().Initialize(mage);
        icebullet_clone1.transform.DOMove(hitpoint.point, 0.4f, false);
        NetworkServer.Spawn(icebullet_clone1);
    }

    [Command]
    public void CmdMageCharging(NetworkIdentity mage, string chargingName, Vector3 position, Quaternion rotation)
    {
        GameObject chargingAnim_ = Instantiate(PrefabCache.Instance.PrefabIndex[chargingName], position, rotation) as GameObject;
        chargingAnim_.transform.parent = mage.gameObject.transform;
        NetworkServer.Spawn(chargingAnim_);
    }

    [Command]
    public void CmdMageSecondFxDestroy()
    {
        GameObject superfx1 = GameObject.Find("MageCharging1(Clone)");
        GameObject superfx2 = GameObject.Find("MageCharging2(Clone)");
        GameObject superfx3 = GameObject.Find("MageCharging3(Clone)");
        if (superfx1 != null) Destroy(superfx1);
        if (superfx2 != null) Destroy(superfx2);
        if (superfx3 != null) Destroy(superfx3);
    }

    [Command]
    public void CmdMageSuper(NetworkIdentity mage, Vector3 position, Quaternion rotation, Vector3 moveTo, float time)
    {
        GameObject superBallanim_ = Instantiate(
            PrefabCache.Instance.PrefabIndex["MageSuper"],
            position,
            rotation) as GameObject;

        superBallanim_.GetComponent<SuperHit>().Initialize(mage);
        superBallanim_.transform.DOLocalMove(moveTo, time, false);
        NetworkServer.Spawn(superBallanim_);
    }

    [Command]
    public void CmdMageSuperAnim(NetworkIdentity mage, Vector3 position, Quaternion rotation)
    {
        GameObject super1_ = Instantiate(PrefabCache.Instance.PrefabIndex["MageSuper1"], position, rotation) as GameObject;
        super1_.GetComponent<SuperHit>().Initialize(mage);
        NetworkServer.Spawn(super1_);
    }

    [Command]
    public void CmdTowerAttack(NetworkIdentity tower, string towerAttackType, Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        GameObject attack_ = Instantiate(
           PrefabCache.Instance.PrefabIndex[towerAttackType],
           position, 
           rotation) as GameObject;

        attack_.GetComponent<TowerAttackHit>().Tower = tower.gameObject;
        attack_.GetComponent<TowerAttackHit>().velocity = velocity;

        NetworkServer.Spawn(attack_);

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
        obj.gameObject.GetComponent<NetworkPlayerOwner>().Owner = owner.gameObject.GetComponent<NetworkPlayerObject>();
    }
}
