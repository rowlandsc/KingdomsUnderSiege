using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

    public GameObject RoundManager, TowerPlacer, PrefabCache;

    // Use this for initialization
    public override void OnStartServer()
    {
        GameObject roundManager = Instantiate(this.RoundManager);
        GameObject towerPlacer = Instantiate(this.TowerPlacer);
        GameObject prefabCache = Instantiate(this.PrefabCache);
        NetworkServer.Spawn(roundManager);
        NetworkServer.Spawn(towerPlacer);
        NetworkServer.Spawn(prefabCache);
	}
}
