using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

    public GameObject RoundManager, TowerPlacer, PrefabCache;

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

    // Use this for initialization
	void OnLevelWasLoaded(int level) {
		if (isServer) {
			if (level == 2) {
		        GameObject roundManager = Instantiate(this.RoundManager);
		        GameObject towerPlacer = Instantiate(this.TowerPlacer);
		        GameObject prefabCache = Instantiate(this.PrefabCache);
		        NetworkServer.Spawn(roundManager);
		        NetworkServer.Spawn(towerPlacer);
		        NetworkServer.Spawn(prefabCache);
			}
		}
}
