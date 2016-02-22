using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIProvider : MonoBehaviour {

	private float hp;
	private float mp;
	private float money;

	private float melee_cooldown;
	private float second_cooldown;
	private float super_cooldown;

	private float melee_cooldown_timer;
	private float second_cooldown_timer;
	private float super_cooldown_timer;

	private GameObject UIhp;
	private GameObject UImp;
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

	// Use this for initialization
	void Start () {
		melee_cooldown=0;
		second_cooldown=0;
		super_cooldown=0;
	}

	// Update is called once per frame
	void Update () {


		if(enable){

			UIhp = GameObject.Find("HealthPoints");
			UImp = GameObject.Find("MagicPoints");
			UIMoney = GameObject.Find("Money_Amount");
			UIMelee = GameObject.Find("Melee_Cooldown");
			UISecond = GameObject.Find("Second_Cooldown");
			UISuper = GameObject.Find("Super_Cooldown");

			MeleeIcon = GameObject.Find("UIMelee");
			SecondIcon = GameObject.Find("UISecond");
			SuperIcon = GameObject.Find("UISuper");

			hp = this.gameObject.GetComponent<ProfileSystem>().healthPoints;
			mp = this.gameObject.GetComponent<ProfileSystem>().MagicPoints;
			money = this.gameObject.GetComponent<ProfileSystem>().haveMoney;

			if(this.gameObject.name=="Mage"||this.gameObject.name=="Mage(Clone)")
			{
				melee_cooldown = this.gameObject.GetComponent<MageMelee>().getcooldown();
				second_cooldown = this.gameObject.GetComponent<MegeSecond>().getcooldown();
				super_cooldown = this.gameObject.GetComponent<MageSuper>().getcooldown();

				melee_cooldown_timer = this.gameObject.GetComponent<MageMelee>().gettimer();
				second_cooldown_timer = this.gameObject.GetComponent<MegeSecond>().gettimer();
				super_cooldown_timer = this.gameObject.GetComponent<MageSuper>().gettimer();
			}

			if(this.gameObject.name=="Knight"||this.gameObject.name=="Knight(Clone)")
			{
				melee_cooldown = this.gameObject.GetComponent<KnightMelee>().getcooldown();
				second_cooldown = this.gameObject.GetComponent<KnightSecond>().getcooldown();
				super_cooldown = this.gameObject.GetComponent<KnightSuper>().getcooldown();

				melee_cooldown_timer = this.gameObject.GetComponent<KnightMelee>().gettimer();
				second_cooldown_timer = this.gameObject.GetComponent<KnightSecond>().gettimer();
				super_cooldown_timer = this.gameObject.GetComponent<KnightSuper>().gettimer();
			}

			if(this.gameObject.name=="Arch"||this.gameObject.name=="Arch(Clone)")
			{
				melee_cooldown = this.gameObject.GetComponent<ArchMelee>().getcooldown();
				second_cooldown = this.gameObject.GetComponent<ArchSecond>().getcooldown();
				super_cooldown = this.gameObject.GetComponent<ArchSuper>().getcooldown();

				melee_cooldown_timer = this.gameObject.GetComponent<ArchMelee>().gettimer();
				second_cooldown_timer = this.gameObject.GetComponent<ArchSecond>().gettimer();
				super_cooldown_timer = this.gameObject.GetComponent<ArchSuper>().gettimer();
			}


		//push the value to UI
			MeleeIcon.GetComponent<RawImage>().texture = melee_icon;
			SecondIcon.GetComponent<RawImage>().texture = second_icon;
			SuperIcon.GetComponent<RawImage>().texture = super_icon;



			UIhp.GetComponent<Slider>().value = hp/(this.gameObject.GetComponent<ProfileSystem>().MAX_HealthPoints);
			UImp.GetComponent<Slider>().value = mp/(this.gameObject.GetComponent<ProfileSystem>().MAX_MagicPoints);
	
			UIMoney.GetComponent<Text>().text = money.ToString();

			if(melee_cooldown_timer!=melee_cooldown) UIMelee.GetComponent<Text>().text = ((int) Mathf.Round (melee_cooldown_timer)).ToString();
			if(melee_cooldown_timer==melee_cooldown) UIMelee.GetComponent<Text>().text = null;

			if(second_cooldown_timer!=second_cooldown) UISecond.GetComponent<Text>().text = ((int) Mathf.Round (second_cooldown_timer)).ToString();
			if(second_cooldown_timer==second_cooldown) UISecond.GetComponent<Text>().text = null;

			if(super_cooldown_timer!=super_cooldown) UISuper.GetComponent<Text>().text = ((int) Mathf.Round (super_cooldown_timer)).ToString();
			if(super_cooldown_timer==super_cooldown) UISuper.GetComponent<Text>().text = null;

		}
	}


}
