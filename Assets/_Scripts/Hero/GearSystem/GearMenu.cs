using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GearMenu : MonoBehaviour {

	public float hpupgrade=50f;
	public float mpupgrade=50f;

	public float meleeupgrade=5f;
	public float secondupgrade=10f;
	public float superupgrade=20f;

	public float armorupgrade=20f;
	public float healthregenupgrade=3f;
	public float magicregenupgrade=3f;

	public float price_for_upgrade=500f;

	private bool actvate;
	private GameObject[] finder;
	private GameObject gearsystemUI;

	private GameObject UpHP;
	private GameObject UpMP;
	private GameObject UpMelee;
	private GameObject UpSecond;
	private GameObject UpSuper;
	private GameObject UpArmor;
	private GameObject UpHPReco;
	private GameObject UpMPReco;


	private GameObject Armor_text;
	private GameObject Damage_text;
	private GameObject HR_text;
	private GameObject MR_text;



	public GameObject effect;
	private GameObject effect_clone;


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

		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

		if(actvate){

			camera.GetComponent<DrawShotStar>().disableFS();
			
			HeroMove.DisableMove();
			gearsystemUI.SetActive(true);
			Cursor.visible = true;

			//add function to bottons
			UpHP = GameObject.Find("UPHealth");
			UpMP = GameObject.Find("UPMagic");
			UpMelee = GameObject.Find("UPMeleeDamage");
			UpSecond = GameObject.Find("UPSecondDamage");
			UpSuper = GameObject.Find("UPSuperDamage");
			UpArmor = GameObject.Find("UPArmor");
			UpHPReco  = GameObject.Find("UPHRcon");
			UpMPReco = GameObject.Find("UPMRcon");

			if(Input.GetMouseButtonDown(0)){

			UpHP.GetComponent<Button>().onClick.AddListener(delegate {  upgradeHP(hpupgrade,price_for_upgrade); });
			UpMP.GetComponent<Button>().onClick.AddListener(delegate { upgradeMP(mpupgrade,price_for_upgrade); });
			UpMelee.GetComponent<Button>().onClick.AddListener(delegate { upgradeMeleeDamage(meleeupgrade,price_for_upgrade); });
			UpSecond.GetComponent<Button>().onClick.AddListener(delegate {upgradeSecondDamage(secondupgrade,price_for_upgrade); });
			UpSuper.GetComponent<Button>().onClick.AddListener(delegate { upgradeSuperDamage(superupgrade,price_for_upgrade); });
			UpArmor.GetComponent<Button>().onClick.AddListener(delegate { upgradeArmor(armorupgrade,price_for_upgrade); });
			UpHPReco.GetComponent<Button>().onClick.AddListener(delegate { upgradeHealthRegen(healthregenupgrade,price_for_upgrade); });
			UpMPReco.GetComponent<Button>().onClick.AddListener(delegate { upgradeMagicRegen(magicregenupgrade,price_for_upgrade); });

			}

			if(Input.GetMouseButtonUp(0)){
				UpHP.GetComponent<Button>().onClick.RemoveAllListeners();
				UpMP.GetComponent<Button>().onClick.RemoveAllListeners();
				UpMelee.GetComponent<Button>().onClick.RemoveAllListeners();
				UpSecond.GetComponent<Button>().onClick.RemoveAllListeners();
				UpSuper.GetComponent<Button>().onClick.RemoveAllListeners();
				UpArmor.GetComponent<Button>().onClick.RemoveAllListeners();
				UpHPReco.GetComponent<Button>().onClick.RemoveAllListeners();
				UpMPReco.GetComponent<Button>().onClick.RemoveAllListeners();
			}

			//get value for text
			Armor_text = GameObject.Find("Armor_txt");
			Damage_text = GameObject.Find("Damage_txt");
			HR_text = GameObject.Find("HR_txt");
			MR_text = GameObject.Find("MR_txt");

			Armor_text.GetComponent<Text>().text = this.gameObject.GetComponent<ProfileSystem>().returnArmor().ToString();
			Damage_text.GetComponent<Text>().text = this.gameObject.GetComponent<ProfileSystem>().returnMeleeDamage().ToString() + "/" +  this.gameObject.GetComponent<ProfileSystem>().returnSecondDamage().ToString()
				+ "/" + this.gameObject.GetComponent<ProfileSystem>().returnSuperDamage().ToString();
			HR_text.GetComponent<Text>().text = this.gameObject.GetComponent<ProfileSystem>().returnHR().ToString();
			MR_text.GetComponent<Text>().text = this.gameObject.GetComponent<ProfileSystem>().returnMR().ToString();




		}

		if(!actvate){
			camera.GetComponent<DrawShotStar>().enableFS();

			HeroMove.EnableMove();
			gearsystemUI.SetActive(false);
			Cursor.visible = false;
		}




	}


		
	public void upgradeHP(float value, float price){



		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddHealth(value);}
	}

	public void upgradeMP(float value, float price){
		

		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{

			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddMagic(value);}

	}

	public void upgradeMeleeDamage(float value, float price){
		

		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddMeleeDamage(value);}

	}

	public void upgradeSecondDamage(float value, float price){
		
		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddSecondDamage(value);}

	}

	public void upgradeSuperDamage(float value, float price){
		

		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddSuperDamage(value);}

	}

	public void upgradeArmor(float value, float price){
		

		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddArmor(value);}

	}

	public void upgradeHealthRegen(float value, float price){
		
		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddHealthReco(value);}

	}

	public void upgradeMagicRegen(float value, float price){
		

		if(this.gameObject.GetComponent<ProfileSystem>().useMoney(price))
		{
			effect_clone = Instantiate(effect, this.transform.position, Quaternion.identity) as GameObject;
			effect_clone.AddComponent<DestoryselfAfterfewsecond>();
			effect_clone.transform.parent = this.gameObject.transform;

			this.gameObject.GetComponent<ProfileSystem>().AddMagicReco(value);}

	}

}
