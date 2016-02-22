using UnityEngine;
using System.Collections;

public class ChooseHero : MonoBehaviour {

	public GameObject mage;
	public GameObject knight;
	public GameObject arch;
	public GameObject UI;
	public GameObject GearSystem;

	private GameObject birthplace;
	private GameObject cam;

	private GameObject mage_Clone;
	private GameObject knight_Clone;
	private GameObject arch_Clone;
	private GameObject UI_clone;
	private GameObject GearSystem_clone;

	private GameObject playerfinder;
	private GameObject[] UIfinder;

	// Use this for initialization
	void Start () {
		cam=GameObject.FindGameObjectWithTag("MainCamera");
		birthplace=GameObject.FindGameObjectWithTag("Birthplace");


	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.F1)){

			if(GameObject.FindGameObjectWithTag("Player")){
				playerfinder= GameObject.FindGameObjectWithTag("Player");
				Destroy(playerfinder);
				UIfinder = GameObject.FindGameObjectsWithTag("HeroUI");
				for(int i=0;i<UIfinder.Length;i++){
					Destroy(UIfinder[i]);
				}

			}

			mage_Clone=Instantiate(mage, birthplace.transform.position, Quaternion.identity)as GameObject;
			UI_clone=Instantiate(UI, birthplace.transform.position, Quaternion.identity)as GameObject;
			GearSystem_clone=Instantiate(GearSystem, birthplace.transform.position, Quaternion.identity)as GameObject;

			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(mage_Clone);

		}


		if(Input.GetKeyDown(KeyCode.F2)){

			if(GameObject.FindGameObjectWithTag("Player")){
				playerfinder= GameObject.FindGameObjectWithTag("Player");
				Destroy(playerfinder);
				UIfinder = GameObject.FindGameObjectsWithTag("HeroUI");
				for(int i=0;i<UIfinder.Length;i++){
					Destroy(UIfinder[i]);
				}
			}



			knight_Clone=Instantiate(knight, birthplace.transform.position, Quaternion.identity)as GameObject;
			UI_clone=Instantiate(UI, birthplace.transform.position, Quaternion.identity)as GameObject;
			GearSystem_clone=Instantiate(GearSystem, birthplace.transform.position, Quaternion.identity)as GameObject;

			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(knight_Clone);
		}


		if(Input.GetKeyDown(KeyCode.F3)){
			
			if(GameObject.FindGameObjectWithTag("Player")){
				playerfinder= GameObject.FindGameObjectWithTag("Player");
				Destroy(playerfinder);
				UIfinder = GameObject.FindGameObjectsWithTag("HeroUI");
				for(int i=0;i<UIfinder.Length;i++){
					Destroy(UIfinder[i]);
				}
			}
			
			arch_Clone=Instantiate(arch, birthplace.transform.position, Quaternion.identity)as GameObject;
			UI_clone=Instantiate(UI, birthplace.transform.position, Quaternion.identity)as GameObject;
			GearSystem_clone=Instantiate(GearSystem, birthplace.transform.position, Quaternion.identity)as GameObject;

			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(arch_Clone);
			
		}

	
}
}
