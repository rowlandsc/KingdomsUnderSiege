using UnityEngine;
using System.Collections;

public class ProfileSystem : MonoBehaviour {

	public float healthPoints=100f;
	public float MagicPoints=100f;
	public float MAX_HealthPoints=100;
	public float MAX_MagicPoints=100;


	//the defendpoints max is 100
	public float DefendePoints=0f;
	public float Worth=100f;
	public float haveMoney=0f;

	public float meleeDamageDealt=5f;
	public float secondDamageDealt=12f;
	public float superDamageDealt=40f;

	public float Health_Regen=1f;
	public float Magic_Regen=1f;

	private float timer;
	public GameObject damagePOP;
	private GameObject damagePOP_;

	//FX
	public GameObject death;
	private GameObject death_;

	private bool herodie;
	private GameObject birthplace;


	// Use this for initialization
	void Start () {
		timer=0;
		birthplace=GameObject.FindGameObjectWithTag("Birthplace");
	}
	
	// Update is called once per frame
	void Update () {

		if(healthPoints>MAX_HealthPoints){
			healthPoints=MAX_HealthPoints;
		}


		if(healthPoints<=0){

			//death_ = Instantiate(death_, this.transform.position, Quaternion.identity) as GameObject;
			//death_.AddComponent<DestoryselfAfterfewsecond>();
			if(this.gameObject.tag=="Player"){
				herodie = true;
				Destroy(this.gameObject);
			}else{
				Destroy(this.gameObject);
			}
		}

		if(healthPoints<MAX_HealthPoints){
			
			healthPoints+=Health_Regen*0.01f;

		}

		if(MagicPoints<MAX_MagicPoints){
			
			MagicPoints+=Magic_Regen*0.01f;

		}




		if(herodie){
			
			birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MAX_HealthPoints,MAX_MagicPoints,meleeDamageDealt,secondDamageDealt,superDamageDealt,DefendePoints,Health_Regen,Magic_Regen,haveMoney);
		}


	}




	public void getDamage(float damage){
		healthPoints = healthPoints - damage*(1- DefendePoints/200);
	}

	public void useMagic(float mp){
		MagicPoints = MagicPoints - mp;
	}

	public bool MPenough(float mp){
		if(MagicPoints>=mp){return true;}
		else {return false;}
	}

	public bool KillAndGains(float damage){
		
		if(damage>=healthPoints){
			healthPoints=0f;
			return true;}
		else{
			getDamage(damage);
			return false;
		}
	}

	public void AddHealth(float value){
		MAX_HealthPoints=MAX_HealthPoints+value;
	}

	public void AddMagic(float value){
		MAX_MagicPoints+=value;
	}

	public void AddArmor(float value){
		DefendePoints+=value;
	}

	public void AddMeleeDamage(float value){
		meleeDamageDealt+=value;
	}

	public void AddSecondDamage(float value){
		secondDamageDealt+=value;
	}

	public void AddSuperDamage(float value){
		superDamageDealt+=value;
	}

	public void AddDamage(float value){
		meleeDamageDealt+=value*0.09f;
		secondDamageDealt+=value*0.22f;
		superDamageDealt+=value*0.69f;
	
	}

	public void AddHealthReco(float value){
		Health_Regen+=value;
	}

	public void AddMagicReco(float value){
		Magic_Regen+=value;
	}

	public float returnArmor(){
		return DefendePoints;
	}

	public float returnMeleeDamage(){
		return meleeDamageDealt;
	}

	public float returnSecondDamage(){
		return secondDamageDealt;
	}

	public float returnSuperDamage(){
		return superDamageDealt;
	}

	public float returnHR(){
		return Health_Regen;
	}

	public float returnMR(){
		return Magic_Regen;
	}

	public bool useMoney(float value){
		if(haveMoney>=value){
			haveMoney-=value;
			return true;
		}else{
			return false;
		}
	}

	public void inital(float maxhp, float maxmp, float meleedamage, float seconddamage, float superdamage, float armor, float hre, float mre, float money){
		
		MAX_HealthPoints=maxhp;
		MAX_MagicPoints=maxmp;

		DefendePoints=armor;
		haveMoney=money;

		meleeDamageDealt=meleedamage;
		secondDamageDealt=seconddamage;
		superDamageDealt=superdamage;

		Health_Regen=hre;
		Magic_Regen=mre;
	}


}
