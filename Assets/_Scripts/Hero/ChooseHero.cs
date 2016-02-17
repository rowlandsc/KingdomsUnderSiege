using UnityEngine;
using System.Collections;

public class ChooseHero : MonoBehaviour {

	public GameObject mage;
	public GameObject knight;
	public GameObject arch;

	private GameObject birthplace;
	private GameObject cam;

	private GameObject mage_Clone;
	private GameObject knight_Clone;
	private GameObject arch_Clone;

	// Use this for initialization
	void Start () {
		cam=GameObject.FindGameObjectWithTag("MainCamera");
		birthplace=GameObject.FindGameObjectWithTag("Birthplace");


	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.F1)){

			if(GameObject.FindGameObjectWithTag("Player")){
				GameObject temp = GameObject.FindGameObjectWithTag("Player");
				temp.SetActive(false);
			}

			mage_Clone=Instantiate(mage, birthplace.transform.position, Quaternion.identity)as GameObject;

			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(mage_Clone);

		}


		if(Input.GetKeyDown(KeyCode.F2)){

			if(GameObject.FindGameObjectWithTag("Player")){
				GameObject temp = GameObject.FindGameObjectWithTag("Player");
				temp.SetActive(false);
			}

			knight_Clone=Instantiate(knight, birthplace.transform.position, Quaternion.identity)as GameObject;
			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(knight_Clone);
		}


		if(Input.GetKeyDown(KeyCode.F3)){
			
			if(GameObject.FindGameObjectWithTag("Player")){
				GameObject temp = GameObject.FindGameObjectWithTag("Player");
				temp.SetActive(false);
			}
			
			arch_Clone=Instantiate(arch, birthplace.transform.position, Quaternion.identity)as GameObject;
			
			cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(arch_Clone);
			
		}

	
}
}
