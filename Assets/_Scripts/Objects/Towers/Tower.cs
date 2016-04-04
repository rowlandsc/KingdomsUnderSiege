using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Tower : NetworkBehaviour, IKillable {

    public float Radius = 2;

    private MapCircleDrawer _mapCircleDrawer = null;
    private ProfileSystem _towerStats;

	void Start () {
        TowerPlacer.Instance.TowerList.Add(this);
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();
        _mapCircleDrawer.GameMap = Map.Instance;
        _mapCircleDrawer.CircleRadius = Radius;
        _mapCircleDrawer.UpdateCircle();
        _mapCircleDrawer.SetCircleVisible(false);
        this._towerStats = GetComponent<ProfileSystem>();
    }
	
	void Update () {
	
	}

    void OnDestroy() {
        TowerPlacer.Instance.TowerList.Remove(this);
    }

    public void OnDeath() {
        
        if (this._towerStats.Killer != NetworkInstanceId.Invalid)
        {
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(this._towerStats.Killer);
                player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>().AddGold((int)this._towerStats.Worth);
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(this._towerStats.Killer);
                player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>().AddGold((int)this._towerStats.Worth);
            }
        }

        Destroy(this.gameObject);
    }
}
