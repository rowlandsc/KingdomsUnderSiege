using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
        GameObject towerPlacerGO = Instantiate(PrefabCache.Instance.PrefabIndex["OverseerTowerPlacer"]);
        TowerPlacer towerPlacer = towerPlacerGO.GetComponent<TowerPlacer>();
        towerPlacer.Cam = cam.GetComponent<OverseerCamera>();
        towerPlacer.OverseerPlayer = GetComponent<NetworkIdentity>();
        GameObject overseerUI = Instantiate(PrefabCache.Instance.PrefabIndex["OverseerUI"]);
        overseerUI.transform.FindChild("TowerMenu").GetComponent<OverseerTowerMenuUI>().TowerPlacer = towerPlacer;
	}
}
