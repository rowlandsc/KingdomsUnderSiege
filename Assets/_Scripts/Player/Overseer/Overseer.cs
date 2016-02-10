using UnityEngine;
using System.Collections;

public class Overseer : Player {

	public OverseerCamera Cam;

	void Start () {
		Cam = Camera.main.gameObject.AddComponent<OverseerCamera>();
		gameObject.AddComponent<TowerPlacer>();
	}
	
	void Update () {
		
	}
}
