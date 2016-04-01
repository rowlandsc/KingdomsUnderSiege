using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProfileSystem : MonoBehaviour {


    public List<ProfileEffect> CurrentEffects = new List<ProfileEffect>();

    public float baseHealthPoints = 100f;
	public float HealthPoints {
        get {
            return baseHealthPoints;
        }
    }

    public float baseMagicPoints = 100f;
	public float MagicPoints {
        get {
            return baseMagicPoints;
        }
    }

    public float baseMaxHealthPoints = 100f;
	public float MAX_HealthPoints {
        get {
            float maxHealthPoints = baseMaxHealthPoints;
            for (int i=0; i<CurrentEffects.Count; i++) {
                maxHealthPoints += CurrentEffects[i].MaxHealthPointsChange;
            }
            return maxHealthPoints;
        }
    }

    public float baseMaxMagicPoints = 100f;
    public float MAX_MagicPoints {
        get {
            float maxMagicPoints = baseMaxMagicPoints;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                maxMagicPoints += CurrentEffects[i].MaxMagicPointsChange;
            }
            return maxMagicPoints;
        }
    }


	//the defendpoints max is 100
	public float DefendPoints=0f;
	public float Worth=100f;
	public float haveMoney=0f;

	public float MeleeDamageDealt=5f;
	public float SecondDamageDealt=12f;
	public float SuperDamageDealt=40f;

	public float HealthRegen=1f;
	public float MagicRegen=1f;

	private float timer;
	public GameObject damagePOP;
	private GameObject damagePOP_;

	//FX
	public GameObject death;
	private GameObject death_;

	private bool herodie;
	private GameObject Mage_birthplace;
	private GameObject Knight_birthplace;
	private GameObject Arch_birthplace;


	// Use this for initialization
	void Start () {
		timer=0;
		Mage_birthplace=GameObject.Find("MageSummonPoint");
		Knight_birthplace =GameObject.Find("KnightSummonPoint");
		Arch_birthplace =GameObject.Find("ArchSummonPoint");
	}
	
	// Update is called once per frame
	void Update () {


		if(baseHealthPoints > MAX_HealthPoints){
            baseHealthPoints = MAX_HealthPoints;
		}


		if(HealthPoints<=0){

			//death_ = Instantiate(death_, this.transform.position, Quaternion.identity) as GameObject;
			//death_.AddComponent<DestoryselfAfterfewsecond>();
			if(this.gameObject.tag=="Player"){
				herodie = true;
				Destroy(this.gameObject);
			}
			else if(this.gameObject.name=="ChaDragon"){
				this.gameObject.GetComponent<DragonAI>().NowState = 2;
			}
			else{
				Destroy(this.gameObject);
			}
		}

		if(baseHealthPoints < MAX_HealthPoints){
			
			baseHealthPoints += HealthRegen * Time.deltaTime;

		}

		if(baseMaxMagicPoints < MAX_MagicPoints){

            baseMaxMagicPoints += MagicRegen * Time.deltaTime;

		}




		if(herodie){

			if(this.gameObject.name=="Mage(Clone)") Mage_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MAX_HealthPoints,MAX_MagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
			if(this.gameObject.name=="Knight(Clone)") Knight_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MAX_HealthPoints,MAX_MagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
			if(this.gameObject.name=="Arch(Clone)") Arch_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MAX_HealthPoints,MAX_MagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
		}
			
	}




	public void getDamage(float damage){
		baseHealthPoints = HealthPoints - damage*(1- DefendPoints/200);
	}

	public void useMagic(float mp){
		baseMaxMagicPoints = MagicPoints - mp;
	}

	public bool MPenough(float mp){
		if(MagicPoints>=mp){return true;}
		else {return false;}
	}

	public bool KillAndGains(float damage){

		if(damage>=HealthPoints){
			baseHealthPoints = 0f;
			return true;}
		else{
			getDamage(damage);
			return false;
		}
	}

	public void AddHealth(float value){
		baseMaxHealthPoints = baseMaxHealthPoints + value;
	}

	public void AddMagic(float value){
		baseMaxMagicPoints += value;
	}

	public void AddArmor(float value){
		DefendPoints+=value;
	}

	public void AddMeleeDamage(float value){
		MeleeDamageDealt+=value;
	}

	public void AddSecondDamage(float value){
		SecondDamageDealt+=value;
	}

	public void AddSuperDamage(float value){
		SuperDamageDealt+=value;
	}

	public void AddDamage(float value){
		MeleeDamageDealt+=value*0.09f;
		SecondDamageDealt+=value*0.22f;
		SuperDamageDealt+=value*0.69f;
	
	}

	public void AddHealthReco(float value){
		HealthRegen+=value;
	}

	public void AddMagicReco(float value){
		MagicRegen+=value;
	}

	public float returnArmor(){
		return DefendPoints;
	}

	public float returnMeleeDamage(){
		return MeleeDamageDealt;
	}

	public float returnSecondDamage(){
		return SecondDamageDealt;
	}

	public float returnSuperDamage(){
		return SuperDamageDealt;
	}

	public float returnHR(){
		return HealthRegen;
	}

	public float returnMR(){
		return MagicRegen;
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
		
		baseMaxHealthPoints=maxhp;
		baseMaxMagicPoints=maxmp;
        baseHealthPoints = baseMaxHealthPoints;
        baseMagicPoints = baseMaxMagicPoints;

		DefendPoints=armor;
		haveMoney=money;

		MeleeDamageDealt=meleedamage;
		SecondDamageDealt=seconddamage;
		SuperDamageDealt=superdamage;

		HealthRegen=hre;
		MagicRegen=mre;
	}


}
