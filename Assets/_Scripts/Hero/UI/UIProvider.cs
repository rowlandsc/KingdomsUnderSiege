using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIProvider : MonoBehaviour {

	private float hp;
	private float mp;
	private float money;
	private float Max_hp;
	private float Max_mp;

	private float melee_cooldown;
	private float second_cooldown;
	private float super_cooldown;

	private float melee_cooldown_timer;
	private float second_cooldown_timer;
	private float super_cooldown_timer;


	private GameObject UIhp;
	private GameObject UIhptext;
	private GameObject UImp;
	private GameObject UImptext;
	private GameObject UIMoney;
	private GameObject UIMelee;
	private GameObject UISecond;
	private GameObject UISuper;

	private GameObject MeleeIcon;
	private GameObject SecondIcon;
	private GameObject SuperIcon;

	public Texture melee_icon;
	public Texture second_icon;
	public Texture super_icon;

	public bool enable=true;

    private bool initialized = false;

    private ProfileSystem profileSystem;
    private NetworkPlayerStats ownerStats;

    private MageMelee mageMelee;
    private MegeSecond mageSecond;
    private MageSuper mageSuper;

    private KnightMelee knightMelee;
    private KnightSecond knightSecond;
    private KnightSuper knightSuper;

    private ArchMelee archerMelee;
    private ArchSecond archerSecond;
    private ArchSuper archerSuper;
	private NetworkPlayerInput _playerInput;

	// Use this for initialization
	void Start () {
		melee_cooldown=0;
		second_cooldown=0;
		super_cooldown=0;
		_playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }

    void Initialize() {
		
		if(_playerInput.CheckLocalPlayer != 1) {

		} else {

	        initialized = true;

	        UIhp = GameObject.Find("HealthPoints");
	        UIhptext = GameObject.Find("HP_Shows");
	        UImp = GameObject.Find("MagicPoints");
	        UImptext = GameObject.Find("MP_Shows");
	        UIMoney = GameObject.Find("Money_Amount");
	        UIMelee = GameObject.Find("Melee_Cooldown");
	        UISecond = GameObject.Find("Second_Cooldown");
	        UISuper = GameObject.Find("Super_Cooldown");

	        MeleeIcon = GameObject.Find("UIMelee");
	        SecondIcon = GameObject.Find("UISecond");
	        SuperIcon = GameObject.Find("UISuper");

	        profileSystem = GetComponent<ProfileSystem>();
	        ownerStats = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();

	        if (this.gameObject.name == "Mage" || this.gameObject.name == "Mage(Clone)") {
	            mageMelee = GetComponent<MageMelee>();
	            mageSecond = GetComponent<MegeSecond>();
	            mageSuper = GetComponent<MageSuper>();
	        }

	        if (this.gameObject.name == "Knight" || this.gameObject.name == "Knight(Clone)") {
	            knightMelee = GetComponent<KnightMelee>();
	            knightSecond = GetComponent<KnightSecond>();
	            knightSuper = GetComponent<KnightSuper>();
	        }

	        if (this.gameObject.name == "Arch" || this.gameObject.name == "Arch(Clone)") {
	            archerMelee = GetComponent<ArchMelee>();
	            archerSecond = GetComponent<ArchSecond>();
	            archerSuper = GetComponent<ArchSuper>();
	        }
		}
    }

	// Update is called once per frame
	void Update () {

        if (!initialized) {
            if (!GameObject.Find("UISystem(Clone)")) return;
            else {
                Initialize();
            }
        }

		if(enable && _playerInput.CheckLocalPlayer == 1){
			
            hp = profileSystem.HealthPoints;
			mp = profileSystem.MagicPoints;
            money = ownerStats.Gold;
			Max_hp = profileSystem.MaxHealthPoints;
			Max_mp = profileSystem.MaxMagicPoints;

			if(this.gameObject.name=="Mage"||this.gameObject.name=="Mage(Clone)")
			{
				melee_cooldown = mageMelee.getcooldown();
				second_cooldown = mageSecond.getcooldown();
				super_cooldown = mageSuper.getcooldown();

				melee_cooldown_timer = mageMelee.gettimer();
				second_cooldown_timer = mageSecond.gettimer();
				super_cooldown_timer = mageSuper.gettimer();
			}

			if(this.gameObject.name=="Knight"||this.gameObject.name=="Knight(Clone)")
			{
				melee_cooldown = knightMelee.getcooldown();
				second_cooldown = knightSecond.getcooldown();
				super_cooldown = knightSuper.getcooldown();

				melee_cooldown_timer = knightMelee.gettimer();
				second_cooldown_timer = knightSecond.gettimer();
				super_cooldown_timer = knightSuper.gettimer();
			}

			if(this.gameObject.name=="Arch"||this.gameObject.name=="Arch(Clone)")
			{
				melee_cooldown = archerMelee.getcooldown();
				second_cooldown = archerSecond.getcooldown();
				super_cooldown = archerSuper.getcooldown();

				melee_cooldown_timer = archerMelee.gettimer();
				second_cooldown_timer = archerSecond.gettimer();
				super_cooldown_timer = archerSuper.gettimer();
			}


			//push the value to UI
			MeleeIcon.GetComponent<RawImage>().texture = melee_icon;
			SecondIcon.GetComponent<RawImage>().texture = second_icon;
			SuperIcon.GetComponent<RawImage>().texture = super_icon;

			UIhp.GetComponent<Slider>().value = hp/(this.gameObject.GetComponent<ProfileSystem>().MaxHealthPoints);
			UImp.GetComponent<Slider>().value = mp/(this.gameObject.GetComponent<ProfileSystem>().MaxMagicPoints);
	
			UIMoney.GetComponent<Text>().text = money.ToString();

			UIhptext.GetComponent<Text>().text = ((int) Mathf.Round (hp)).ToString() + "/" + ((int) Mathf.Round (Max_hp)).ToString();
			UImptext.GetComponent<Text>().text = ((int) Mathf.Round (mp)).ToString() + "/" + ((int) Mathf.Round (Max_mp)).ToString();

			if(melee_cooldown_timer!=melee_cooldown) UIMelee.GetComponent<Text>().text = ((int) Mathf.Round (melee_cooldown_timer)).ToString();
			if(melee_cooldown_timer==melee_cooldown) UIMelee.GetComponent<Text>().text = null;

			if(second_cooldown_timer!=second_cooldown) UISecond.GetComponent<Text>().text = ((int) Mathf.Round (second_cooldown_timer)).ToString();
			if(second_cooldown_timer==second_cooldown) UISecond.GetComponent<Text>().text = null;

			if(super_cooldown_timer!=super_cooldown) UISuper.GetComponent<Text>().text = ((int) Mathf.Round (super_cooldown_timer)).ToString();
			if(super_cooldown_timer==super_cooldown) UISuper.GetComponent<Text>().text = null;

		}
	}


}
