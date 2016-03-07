﻿using UnityEngine;
using System.Collections;

public class HeroArch : Hero {


	void Start () {
		StartCoroutine(BuildPrefabs());
	}

	void Update () {

	}

	public IEnumerator BuildPrefabs() {
		GameObject archObject = GameObject.Find("Arch(Clone)");
		while (PrefabCache.Instance == null || !archObject) {
			archObject = GameObject.Find("Arch(Clone)");
			yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        HeroCamPrefab = PrefabCache.Instance.PrefabIndex["HeroCamera"];
		HeroUIPrefab = PrefabCache.Instance.PrefabIndex["HeroUI"];
		HeroGearSystemPrefab = PrefabCache.Instance.PrefabIndex["HeroGearSystem"];

		_heroUI = Instantiate(HeroUIPrefab, Vector3.zero, Quaternion.identity)as GameObject;
		GameObject camera_tobedestory = GameObject.FindGameObjectWithTag("MainCamera");
		Destroy(camera_tobedestory);
		_heroCam = Instantiate(HeroCamPrefab) as GameObject;
		_heroGearSystem=Instantiate(HeroGearSystemPrefab)as GameObject;

		_heroCam.gameObject.GetComponent<TPSCamera>().ChangePlayer(archObject);

        archObject.AddComponent<HeroFaceCamera>();
	}
}
