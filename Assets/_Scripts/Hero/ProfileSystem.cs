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

	// Use this for initialization
	void Start () {
		timer=0;
	}
	
	// Update is called once per frame
	void Update () {

		if(healthPoints>MAX_HealthPoints){
			healthPoints=MAX_HealthPoints;
		}


		if(healthPoints<=0){

			//death_ = Instantiate(death_, this.transform.position, Quaternion.identity) as GameObject;
			//death_.AddComponent<DestoryselfAfterfewsecond>();
			Destroy(this.gameObject);
		}
	}

	public void getDamage(float damage){
		healthPoints = healthPoints - damage*(DefendePoints/200);
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
			healthPoints-=damage;
			return false;
		}
	}

	public void AddHealth(float value){
		MAX_HealthPoints+=value;
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
}
