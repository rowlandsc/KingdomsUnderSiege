using UnityEngine;
using System.Collections;

public class Overseer : Player {

	public OverseerCamera Cam;

	void Start () {
		Cam = Camera.main.gameObject.AddComponent<OverseerCamera>();

		StartCoroutine(BuildPrefabs());
	}
	
	void Update () {
		
	}

	public IEnumerator BuildPrefabs() {
		while (PrefabCache.Instance == null) {
			yield return null;
		}

		GameObject.Instantiate(PrefabCache.Instance.PrefabIndex["OverseerTowerPlacer"]);
	}
}
