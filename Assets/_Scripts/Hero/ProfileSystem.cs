using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProfileSystem : NetworkBehaviour {

    [SerializeField]
    public ProfileEffectList CurrentEffects = new ProfileEffectList();

    [SyncVar]
    public bool IsDead = false;
    [SyncVar]
    NetworkInstanceId Killer = NetworkInstanceId.Invalid;

    [SyncVar]
    public float baseHealthPoints = 100f;
    public float HealthPoints {
        get {
            return baseHealthPoints;
        }
    }

    [SyncVar]
    public float baseMagicPoints = 100f;
    public float MagicPoints {
        get {
            return baseMagicPoints;
        }
    }

    [SyncVar]
    public float baseMaxHealthPoints = 100f;
    public float MaxHealthPoints {
        get {
            float maxHealthPoints = baseMaxHealthPoints;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                maxHealthPoints *= CurrentEffects[i].MaxHealthPointsMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                maxHealthPoints += CurrentEffects[i].MaxHealthPointsAdd;
            }
            return maxHealthPoints;
        }
    }

    [SyncVar]
    public float baseMaxMagicPoints = 100f;
    public float MaxMagicPoints {
        get {
            float maxMagicPoints = baseMaxMagicPoints;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                maxMagicPoints *= CurrentEffects[i].MaxMagicPointsMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                maxMagicPoints += CurrentEffects[i].MaxMagicPointsAdd;
            }
            return maxMagicPoints;
        }
    }


    //the defendpoints max is 100
    [SyncVar]
    public float DefendPoints = 0f;
    [SyncVar]
    public float Worth = 100f;
    [SyncVar]
    public float haveMoney = 0f;

    [SyncVar]
    public float baseMeleeDamageDealt = 5f;
    public float MeleeDamageDealt {
        get {
            float meleeDamage = baseMeleeDamageDealt;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                meleeDamage *= CurrentEffects[i].MeleeDamageDealtMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                meleeDamage += CurrentEffects[i].MeleeDamageDealtAdd;
            }
            return meleeDamage;
        }
    }
    [SyncVar]
    public float baseSecondDamageDealt = 12f; 
    public float SecondDamageDealt {
        get {
            float secondDamage = baseSecondDamageDealt;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                secondDamage *= CurrentEffects[i].SecondDamageDealtMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                secondDamage += CurrentEffects[i].SecondDamageDealtAdd;
            }
            return secondDamage;
        }
    }
    [SyncVar]
    public float baseSuperDamageDealt=40f;
    public float SuperDamageDealt {
        get {
            float superDamage = baseSuperDamageDealt;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                superDamage *= CurrentEffects[i].SuperDamageDealtMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                superDamage += CurrentEffects[i].SuperDamageDealtAdd;
            }
            return superDamage;
        }
    }

    [SyncVar]
    public float baseHealthRegen = 1f;
    public float HealthRegen {
        get {
            float healthRegen = baseHealthRegen;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                healthRegen *= CurrentEffects[i].HealthRegenMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                healthRegen += CurrentEffects[i].HealthRegenAdd;
            }
            return healthRegen;
        }
    }
    [SyncVar]
    public float baseMagicRegen=1f;
    public float MagicRegen {
        get {
            float magicRegen = baseMagicRegen;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                magicRegen *= CurrentEffects[i].MagicRegenMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                magicRegen += CurrentEffects[i].MagicRegenAdd;
            }
            return magicRegen;
        }
    }

    [SyncVar]
    public float baseMoveSpeed=1.5f;
    public float MoveSpeed {
        get {
            float moveSpeed = baseMoveSpeed;
            for (int i = 0; i < CurrentEffects.Count; i++) {
                moveSpeed *= CurrentEffects[i].MoveSpeedMult;
            }
            for (int i = 0; i < CurrentEffects.Count; i++) {
                moveSpeed += CurrentEffects[i].MoveSpeedAdd;
            }
            return moveSpeed;
        }
    }

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
        if (!KUSNetworkManager.HostPlayer.isLocalPlayer) return;
        
        UpdateEffects();

        if(baseHealthPoints > MaxHealthPoints){
            baseHealthPoints = MaxHealthPoints;
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

		if(baseHealthPoints < MaxHealthPoints){
			baseHealthPoints += HealthRegen * Time.deltaTime;
		}

		if(baseMaxMagicPoints < MaxMagicPoints){
            baseMaxMagicPoints += MagicRegen * Time.deltaTime;
		}

		if(herodie){
			if(this.gameObject.name=="Mage(Clone)") Mage_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MaxHealthPoints,MaxMagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
			if(this.gameObject.name=="Knight(Clone)") Knight_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MaxHealthPoints,MaxMagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
			if(this.gameObject.name=="Arch(Clone)") Arch_birthplace.GetComponent<RespawnManager>().HeroDie(this.gameObject.name,MaxHealthPoints,MaxMagicPoints,MeleeDamageDealt,SecondDamageDealt,SuperDamageDealt,DefendPoints,HealthRegen,MagicRegen,haveMoney);
		}
			
	}

    public void AddEffect(ProfileEffect effect) {
        if (effect.StartingDuration == ProfileEffect.INSTANT) {
            baseHealthPoints += effect.HealthPointsAdd;
            baseMagicPoints += effect.MagicPointsAdd;

            baseMaxHealthPoints *= effect.MaxHealthPointsMult;
            baseMaxHealthPoints += effect.MaxHealthPointsAdd;
            baseMaxMagicPoints *= effect.MaxMagicPointsMult;
            baseMaxMagicPoints += effect.MaxMagicPointsAdd;

            baseMeleeDamageDealt *= effect.MeleeDamageDealtMult;
            baseMeleeDamageDealt += effect.MeleeDamageDealtAdd;
            baseSecondDamageDealt *= effect.SecondDamageDealtMult;
            baseSecondDamageDealt += effect.SecondDamageDealtAdd;
            baseSuperDamageDealt *= effect.SuperDamageDealtMult;
            baseSuperDamageDealt += effect.SuperDamageDealtAdd;

            baseHealthRegen *= effect.HealthRegenMult;
            baseHealthRegen += effect.HealthRegenAdd;
            baseMagicRegen *= effect.MagicRegenMult;
            baseMagicRegen += effect.MagicRegenAdd;

            if (baseHealthPoints < 0 && !IsDead) {
                IsDead = true;
                Killer = effect.InflicterID;
            }
        }
        else {
            CurrentEffects.Add(effect);
        }
    }

    void UpdateEffects() {
        for (int i=CurrentEffects.Count - 1; i>=0; i--) {
            ProfileEffect countdownEffect = new ProfileEffect(CurrentEffects[i]);
            countdownEffect.RemainingDuration -= Time.deltaTime;
            CurrentEffects[i] = countdownEffect;
            if (CurrentEffects[i].RemainingDuration <= 0) {
                CurrentEffects.RemoveAt(i);
            }
            else {
                // Update health/magic damage over time
                baseHealthPoints += CurrentEffects[i].HealthPointsAdd * Time.deltaTime;
                baseMagicPoints += CurrentEffects[i].MagicPointsAdd * Time.deltaTime;

                if (baseHealthPoints < 0 && !IsDead) {
                    Killer = CurrentEffects[i].InflicterID;
                    IsDead = true;
                    break;
                }
            }
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
		baseMeleeDamageDealt+=value;
	}

	public void AddSecondDamage(float value){
        baseSecondDamageDealt += value;
	}

	public void AddSuperDamage(float value){
        baseSuperDamageDealt += value;
	}

	public void AddDamage(float value){
        baseMeleeDamageDealt += value*0.09f;
        baseSecondDamageDealt += value*0.22f;
        baseSuperDamageDealt += value*0.69f;	
	}

	public void AddHealthReco(float value){
        baseHealthRegen += value;
	}

	public void AddMagicReco(float value){
        baseMagicRegen += value;
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

        baseMeleeDamageDealt = meleedamage;
        baseSecondDamageDealt = seconddamage;
        baseSuperDamageDealt = superdamage;

        baseHealthRegen = hre;
        baseMagicRegen = mre;
	}


}
