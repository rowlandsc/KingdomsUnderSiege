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

	private GameObject Mage_birthplace;
	private GameObject Knight_birthplace;
	private GameObject Arch_birthplace;
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
		Mage_birthplace=GameObject.Find("MageSummonPoint");
		Knight_birthplace =GameObject.Find("KnightSummonPoint");
		Arch_birthplace =GameObject.Find("ArchSummonPoint");

		herorespwn_words=null;
		herorespwn_timer=10f;
	}
	
	// Update is called once per frame
	void Update () {
		

	}




}
