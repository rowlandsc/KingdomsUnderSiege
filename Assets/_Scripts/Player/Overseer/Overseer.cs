using UnityEngine;
using System.Collections;

public class Overseer : Player {

	public OverseerCamera Cam;

	void Start () {
		Cam = Camera.main.gameObject.AddComponent<OverseerCamera>();

		GameObject.Instantiate(PrefabCache.Instance.PrefabIndex["OverseerTowerPlacer"]);
	}
	
	void Update () {
		
	}
}
