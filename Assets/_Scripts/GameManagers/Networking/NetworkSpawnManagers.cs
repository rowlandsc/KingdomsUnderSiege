using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

	public static NetworkSpawnManagers Instance;

    public GameObject RoundManager, PrefabCache, HeroMinionSpawner;
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

                GameObject heroMinionSpawner = (GameObject) Instantiate(this.HeroMinionSpawner, new Vector3(249.76f, 19.996f, 157.2f), Quaternion.identity);

				DoneLoading = true;
			}
		}
	}
}
