using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour {

	public GameObject mage;
	public GameObject knight;
	public GameObject arch;
	public GameObject UI;
	public GameObject GearSystem;

	private GameObject mage_Clone;
	private GameObject knight_Clone;
	private GameObject arch_Clone;
	private GameObject UI_clone;
	private GameObject GearSystem_clone;

	private bool respawn;

	private GameObject birthplace;
	private GameObject cam;

	public float herorespwn_timer;
	private string herorespwn_words;

	private float maxhp_;
	private float maxmp_;
	private float meleedam;
	private float seconddam;
	private float superdam;
	private float aromr_;
	private float hre_;
	private float mre_;
	private float money_;
	private string hero;

	private GameObject[] UIfinder;


	// Use this for initialization
	void Start () {
		respawn=false;
		cam=GameObject.FindGameObjectWithTag("MainCamera");
		birthplace=GameObject.FindGameObjectWithTag("Birthplace");
		herorespwn_words=null;
		herorespwn_timer=10f;
	}
	
	// Update is called once per frame
	void Update () {

		cam=GameObject.FindGameObjectWithTag("MainCamera");
	
		if(respawn){
			
			herorespwn_timer-=Time.deltaTime;

			herorespwn_words = "Respawn in " + ((int) Mathf.Round (herorespwn_timer)).ToString() + " seconds"; 

			if(herorespwn_timer<0){


				UIfinder = GameObject.FindGameObjectsWithTag("HeroUI");
				for(int i=0;i<UIfinder.Length;i++){
					Destroy(UIfinder[i]);
				}

				if(hero=="Mage(Clone)"){
					mage_Clone=Instantiate(mage, birthplace.transform.position, Quaternion.identity)as GameObject;
					mage_Clone.GetComponent<ProfileSystem>().inital(maxhp_,maxmp_,meleedam,seconddam,superdam,aromr_,hre_,mre_,money_);

					cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(mage_Clone);
				}

				if(hero=="Knight(Clone)"){
					knight_Clone=Instantiate(knight, birthplace.transform.position, Quaternion.identity)as GameObject;
					knight_Clone.GetComponent<ProfileSystem>().inital(maxhp_,maxmp_,meleedam,seconddam,superdam,aromr_,hre_,mre_,money_);

					cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(knight_Clone);
				}

				if(hero=="Arch(Clone)"){
					arch_Clone=Instantiate(arch, birthplace.transform.position, Quaternion.identity)as GameObject;
					arch_Clone.GetComponent<ProfileSystem>().inital(maxhp_,maxmp_,meleedam,seconddam,superdam,aromr_,hre_,mre_,money_);

					cam.gameObject.GetComponent<TPSCamera>().ChangePlayer(arch_Clone);
				}

				UI_clone=Instantiate(UI, birthplace.transform.position, Quaternion.identity)as GameObject;
				GearSystem_clone=Instantiate(GearSystem, birthplace.transform.position, Quaternion.identity)as GameObject;

				respawn=false;
				herorespwn_words=null;

			}

		}

	}
	public void HeroDie(string heroname, float maxhp, float maxmp, float meleedamage, float seconddamage, float superdamage, float armor, float hre, float mre, float money){


		hero= heroname;
		maxhp_ = maxhp;
		maxmp_ = maxmp;
		meleedam = meleedamage;
		seconddam = seconddamage;
		superdam = superdamage;
		aromr_ = armor;
		hre_ = hre;
		mre_ = mre;
		money_ = money;

		respawn=true;


	}

	void OnGUI() {
		GUI.Label(new Rect(Screen.width/2-Screen.width/8, Screen.height/2, 1000, 1000), herorespwn_words);
	}
}
