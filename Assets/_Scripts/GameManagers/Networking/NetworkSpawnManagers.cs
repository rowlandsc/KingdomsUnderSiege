using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

	public static NetworkSpawnManagers Instance;

    public GameObject RoundManager, PrefabCache, HeroMinionSpawner, OverseerMinionSpawner, MinionManager;
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

                GameObject heroMinionSpawner1 = (GameObject) Instantiate(this.HeroMinionSpawner, new Vector3(214f, 21f, 282.6f), Quaternion.identity);
                GameObject heroMinionSpawner2 = (GameObject)Instantiate(this.HeroMinionSpawner, new Vector3(283f, 21f, 282.6f), Quaternion.identity);
                NetworkServer.Spawn(heroMinionSpawner1);
                NetworkServer.Spawn(heroMinionSpawner2);

                GameObject overseerMinionSpawner1 = (GameObject)Instantiate(this.OverseerMinionSpawner, new Vector3(217.4f, 22.67f, 151.8f), Quaternion.identity);
                GameObject overseerMinionSpawner2 = (GameObject)Instantiate(this.OverseerMinionSpawner, new Vector3(280f, 22.67f, 151.8f), Quaternion.identity);
                NetworkServer.Spawn(overseerMinionSpawner1);
                NetworkServer.Spawn(overseerMinionSpawner2);

                GameObject minionManager = (GameObject)Instantiate(this.MinionManager);
                NetworkServer.Spawn(minionManager);

				DoneLoading = true;
			}
		}
	}
}
