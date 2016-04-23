using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Tower : NetworkBehaviour, IKillable, ObjectSelector.ISelectable {

    public float Radius = 2;

    public GameObject GameObject {
        get { return gameObject; }
    }

    private MapCircleDrawer _mapCircleDrawer = null;
    private ProfileSystem _towerStats;

	void Start () {
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();
        _mapCircleDrawer.GameMap = Map.Instance;
        _mapCircleDrawer.CircleRadius = Radius;
        _mapCircleDrawer.UpdateCircle();
        _mapCircleDrawer.SetCircleVisible(false);
        this._towerStats = GetComponent<ProfileSystem>();
    }

    void OnEnable() {
        TowerPlacer.Instance.TowerList.Add(this);
        RegisterAsSelectable();
    }

    void Update () {
	
	}

    void OnDisable() {
        TowerPlacer.Instance.TowerList.Remove(this);
        UnregisterAsSelectable();
    }

    public void OnDeath() {
        
        if (this._towerStats.Killer != NetworkInstanceId.Invalid)
        {
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(this._towerStats.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)this._towerStats.Worth);
                playerStats.AddTowerKill();
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(this._towerStats.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)this._towerStats.Worth);
                playerStats.AddTowerKill();
            }
        }

        Destroy(this.gameObject);
    }

    public void RegisterAsSelectable() {
        ObjectSelector.Selectables.Add(this);
    }
    public void UnregisterAsSelectable() {
        ObjectSelector.Selectables.Remove(this);
    }
    
    public Collider GetSelectionCollider() {
        return GetComponent<Collider>();
    }
}
