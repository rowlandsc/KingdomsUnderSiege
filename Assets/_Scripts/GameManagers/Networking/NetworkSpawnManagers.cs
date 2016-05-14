using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawnManagers : NetworkBehaviour {

	public static NetworkSpawnManagers Instance;

    public GameObject RoundManager, PrefabCache, HeroMinionSpawner, OverseerMinionSpawner, MinionManager, heroDoor;
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

                GameObject heroMinionSpawner1 = (GameObject) Instantiate(this.HeroMinionSpawner, new Vector3(286.5f, 22.67f, 290.5f), Quaternion.identity);
                GameObject heroMinionSpawner2 = (GameObject)Instantiate(this.HeroMinionSpawner, new Vector3(200f, 22.67f, 290.5f), Quaternion.identity);

				GameObject HeroDoors = (GameObject)Instantiate(this.heroDoor, new Vector3(-1.4f, 2.722f, 1.57f), Quaternion.identity);
                NetworkServer.Spawn(heroMinionSpawner1);
                NetworkServer.Spawn(heroMinionSpawner2);

                GameObject overseerMinionSpawner1 = (GameObject)Instantiate(this.OverseerMinionSpawner, new Vector3(220.5f, 22.67f, 151.8f), Quaternion.identity);
                GameObject overseerMinionSpawner2 = (GameObject)Instantiate(this.OverseerMinionSpawner, new Vector3(270.3f, 22.67f, 151.8f), Quaternion.identity);
                NetworkServer.Spawn(overseerMinionSpawner1);
                NetworkServer.Spawn(overseerMinionSpawner2);

                GameObject minionManager = (GameObject)Instantiate(this.MinionManager);
                NetworkServer.Spawn(minionManager);

				DoneLoading = true;
			}
		}
	}
}
