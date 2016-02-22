using UnityEngine;
using System.Collections;

public class GearMenu : MonoBehaviour {

	private bool actvate;
	private GameObject gearsystemUI;

	// Use this for initialization
	void Start () {
		actvate=false;
		gearsystemUI=GameObject.Find("GearSystem");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Tab)){
			actvate=true;
		}
		if(Input.GetKeyUp(KeyCode.Tab)){
			actvate=false;
		}


		if(actvate){
			HeroMove.DisableMove();
			gearsystemUI.SetActive(true);


		}

		if(!actvate){
			HeroMove.EnableMove();
			//gearsystemUI.SetActive(false);

		}

	
	}
}
