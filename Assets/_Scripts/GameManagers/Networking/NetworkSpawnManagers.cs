using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

    public GameObject RoundManager, PrefabCache;

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

    // Use this for initialization
	void OnLevelWasLoaded(int level) {
		if (isServer) {
			if (level == 2) {
				GameObject roundManager = Instantiate (this.RoundManager);
				GameObject prefabCache = Instantiate (this.PrefabCache);
				NetworkServer.Spawn (roundManager);
				NetworkServer.Spawn (prefabCache);
			}
		}
	}
}
