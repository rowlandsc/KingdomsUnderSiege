using UnityEngine;
using System.Collections;

public class HeroMage : Hero {


	void Start () {
		StartCoroutine(BuildPrefabs());
	}

	void Update () {

	}

	public IEnumerator BuildPrefabs() {
		GameObject mageObject = GameObject.Find("Mage(Clone)");
		while (PrefabCache.Instance == null || !mageObject) {
			mageObject = GameObject.Find("Mage(Clone)");
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

		_heroCam.gameObject.GetComponent<TPSCamera>().ChangePlayer(mageObject);

        mageObject.AddComponent<HeroFaceCamera>();
    }
}
