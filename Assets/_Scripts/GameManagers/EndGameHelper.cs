using UnityEngine;
using System.Collections;

public class EndGameHelper : MonoBehaviour {

	public static int heroWin;
	public int heroWinTest;
	private bool firsttime = true;

	// Use this for initialization
	void Start () {
		heroWin = 0;
	}

	// Update is called once per frame
	void Update () {

		heroWinTest = heroWin;

		if (firsttime == true) {

			StartCoroutine(WaitForSpawnerLoad());
			firsttime = false;

		} else {

			if (GameObject.Find("OverseerMinionSpawn(Clone)") == null) {
				heroWin = 1;
				Application.LoadLevel(3);
			}
				
			if (GameObject.Find("HeroMinionSpawn(Clone)") == null) {
				heroWin = 2;
				Application.LoadLevel(3);
			}
		}

	}

	IEnumerator WaitForSpawnerLoad() { 
	
		yield return new WaitForSeconds(5);
	
	}
}
