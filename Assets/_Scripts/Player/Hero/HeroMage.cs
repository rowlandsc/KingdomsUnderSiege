using UnityEngine;
using System.Collections;

public class HeroMage : Hero {


	void Start () {
		
		StartCoroutine(BuildPrefabs());
	}

	void Update () {

	}

	public IEnumerator BuildPrefabs() {
		while (PrefabCache.Instance == null) {
			yield return null;
		}

		_heroUI = Instantiate(HeroUIPrefab, Vector3.zero, Quaternion.identity)as GameObject;
		_heroCam = Instantiate(HeroCamPrefab) as GameObject;
		//GearSystem_clone=Instantiate(GearSystem, birthplace.transform.position, Quaternion.identity)as GameObject;

		_heroCam.gameObject.GetComponent<TPSCamera>().ChangePlayer(GameObject.Find("Mage(Clone)"));
	}
}
