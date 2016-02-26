using UnityEngine;
using System.Collections;

public class HeroKnight : Hero {


	void Start () {
		StartCoroutine(BuildPrefabs());
	}

	void Update () {

	}

	public IEnumerator BuildPrefabs() {
		GameObject knightObject = GameObject.Find("Knight(Clone)");
		while (PrefabCache.Instance == null || !knightObject) {
			knightObject = GameObject.Find("Knight(Clone)");
			yield return null;
		}
		HeroCamPrefab = PrefabCache.Instance.PrefabIndex["HeroCamera"];
		HeroUIPrefab = PrefabCache.Instance.PrefabIndex["HeroUI"];
		HeroGearSystemPrefab = PrefabCache.Instance.PrefabIndex["HeroGearSystem"];

		_heroUI = Instantiate(HeroUIPrefab, Vector3.zero, Quaternion.identity)as GameObject;
		GameObject camera_tobedestory = GameObject.FindGameObjectWithTag("MainCamera");
		Destroy(camera_tobedestory);
		_heroCam = Instantiate(HeroCamPrefab) as GameObject;
		_heroGearSystem=Instantiate(HeroGearSystemPrefab)as GameObject;

		_heroCam.gameObject.GetComponent<TPSCamera>().ChangePlayer(knightObject);
	}
}

