using UnityEngine;
using System.Collections;

public class Overseer : Player {

	public OverseerCamera Cam;

	void Start () {
        //Cam = Cam.gameObject.AddComponent<OverseerCamera>();
        GameObject.Destroy(Camera.main.gameObject);

		StartCoroutine(BuildPrefabs());
	}
	
	void Update () {
		
	}

	public IEnumerator BuildPrefabs() {
		while (PrefabCache.Instance == null) {
			yield return null;
		}

        GameObject cam = Instantiate(PrefabCache.Instance.PrefabIndex["OverseerCamera"]);
        cam.GetComponent<NetworkPlayerOwner>().Owner = GetComponent<NetworkPlayerObject>();
        GameObject towerPlacer = Instantiate(PrefabCache.Instance.PrefabIndex["OverseerTowerPlacer"]);
        towerPlacer.GetComponent<TowerPlacer>().Cam = cam.GetComponent<OverseerCamera>();
	}
}
