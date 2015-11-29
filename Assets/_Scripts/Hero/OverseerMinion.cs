using UnityEngine;
using System.Collections;

public class OverseerMinion : MonoBehaviour {
	
	static public float hp=100;


	public GameObject OnFire;
	public GameObject Glow;
	public GameObject Smoke;
	public GameObject explo;

	private bool onfireyes = true;
	private bool glowyes = true;
	private bool smokeyes = true;

	private GameObject clone,clone1,clone2,clone3;

	// Use this for initialization
	void Start () {



	}
	

	// Update is called once per frame
	void Update () {

	
		if(hp<80 && smokeyes){
			clone = (GameObject)Instantiate(Smoke, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			smokeyes=false;
		}

		if(hp<60 && glowyes&&onfireyes){
			clone1 = (GameObject)Instantiate(Glow, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			glowyes=false;
			clone2 = (GameObject)Instantiate(OnFire, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			onfireyes=false;
		}


		if(hp<0){

			clone3 = (GameObject)Instantiate(explo, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy(this.gameObject);

			Destroy(clone.gameObject);
			Destroy(clone1.gameObject);
			Destroy(clone2.gameObject);



		}

	}


}
