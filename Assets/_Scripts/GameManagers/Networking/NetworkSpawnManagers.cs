using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

	public static NetworkSpawnManagers Instance;

    public GameObject RoundManager, PrefabCache;
	public bool DoneLoading = false;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(this);
		}
	}

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

    // Use this for initialization
	void OnLevelWasLoaded(int level) {
		if (isServer) {
			if (level == 2) {
				GameObject roundManager = Instantiate (this.RoundManager);
				NetworkServer.Spawn (roundManager);

				GameObject prefabCache = Instantiate (this.PrefabCache);
				NetworkServer.Spawn (prefabCache);

				DoneLoading = true;
			}
		}
	}
}
