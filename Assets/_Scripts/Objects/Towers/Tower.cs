using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class Tower : NetworkBehaviour, IKillable, ObjectSelector.ISelectable, IUpgradable {

    public float Radius = 2;

    public GameObject GameObject {                
        get { return gameObject; }
    }

    private MapCircleDrawer _mapCircleDrawer = null;
    private ProfileSystem _towerStats;
    private bool _localPlayerIsOverseer = false;

	void Start () {
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();
        _mapCircleDrawer.GameMap = Map.Instance;
        _mapCircleDrawer.CircleRadius = Radius;
        _mapCircleDrawer.CirclePosition = transform.position;
        _mapCircleDrawer.UpdateCircle();
        _mapCircleDrawer.SetCircleVisible(false);
        this._towerStats = GetComponent<ProfileSystem>();
    }

    void OnEnable() {
        if (KUSNetworkManager.LocalPlayer.Class == NetworkPlayerObject.PlayerClass.OVERSEER) {
            TowerPlacer.Instance.TowerList.Add(this);
            _localPlayerIsOverseer = true;
            RegisterAsSelectable();
        }
    }

    void Update () {
	
	}

    void OnDisable() {
        if (_localPlayerIsOverseer) {
            TowerPlacer.Instance.TowerList.Remove(this);
            UnregisterAsSelectable();
        }
    }

    public void OnDeath() {
        
        if (this._towerStats.Killer != NetworkInstanceId.Invalid)
        {
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(this._towerStats.Killer);
                if (player) {
                    NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                    playerStats.AddGold((int)this._towerStats.Worth);
                    playerStats.AddTowerKill();
                }
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(this._towerStats.Killer);
                if (player) {
                    NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                    playerStats.AddGold((int)this._towerStats.Worth);
                    playerStats.AddTowerKill();
                }
            }
        }

        Destroy(this.gameObject);
    }

    public void RegisterAsSelectable() {
        if (_localPlayerIsOverseer) ObjectSelector.Selectables.Add(this);
    }
    public void UnregisterAsSelectable() {
        if (_localPlayerIsOverseer) ObjectSelector.Selectables.Remove(this);
    }
    
    public Collider GetSelectionCollider() {
        return GetComponent<Collider>();
    }

    public abstract void LevelUp(int level);
}
