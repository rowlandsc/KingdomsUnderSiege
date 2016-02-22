using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GearMenu : MonoBehaviour {

	private bool actvate;
	private GameObject[] finder;
	private GameObject gearsystemUI;

	private GameObject UpHP;
	private GameObject UpMP;
	private GameObject UpDamage;
	private GameObject UpArmor;

	// Use this for initialization
	void Start () {
		
		actvate=false;
	}
	
	// Update is called once per frame
	void Update () {

	
		finder=GameObject.FindGameObjectsWithTag("HeroUI");
		for(int i=0;i<finder.Length;i++){
			if(finder[i].name=="GearSystem(Clone)"){
				gearsystemUI=finder[i];
			}
		}




		if(Input.GetKeyDown(KeyCode.Tab)){
			actvate=true;
		}
		if(Input.GetKeyUp(KeyCode.Tab)){
			actvate=false;
		}


		if(actvate){
			HeroMove.DisableMove();
			gearsystemUI.SetActive(true);
			Cursor.visible = true;
		}

		if(!actvate){
			HeroMove.EnableMove();
			gearsystemUI.SetActive(false);
			Cursor.visible = false;
		}

		//add function to bottons
		UpHP = GameObject.Find("UpHealth");
		UpMP = GameObject.Find("UpMagic");
		UpDamage = GameObject.Find("UpDamage");
		UpArmor = GameObject.Find("UpArmor");

		//UpHP.GetComponent<Button>().onClick();
		//UpMP.GetComponent<Button>().onClick();
		//UpDamage.GetComponent<Button>().onClick();
		//UpArmor.GetComponent<Button>().onClick();
	}


}
