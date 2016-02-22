using UnityEngine;
using System.Collections;

public class KnightSuper : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	public float mp_use=40f;

	public float cooldown=30f;
	public float duration=10f;
	
	private bool canAttack;
	public float timer;
	private bool super_activation;
	private float duration_timer;

	private Vector3 normal_hero_size;
	private Vector3 super_hero_size;

	//FX
	public GameObject super;
	private bool super_clone_runonce;

	// Use this for initialization
	void Start () {


		canAttack=true;
		timer=cooldown;
		duration_timer=0f;
		super_activation=false;
		normal_hero_size=this.gameObject.transform.localScale;
		super_hero_size= normal_hero_size+new Vector3(12f, 12f, 12f);
		super_clone_runonce=true;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R)&&canAttack&&!super_activation&&this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use)){

			this.gameObject.transform.localScale =super_hero_size;
			super_activation=true;
			this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);
		}

		if(super_activation){
			//apply hero_increased_effect here
			if(super_clone_runonce){

				ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
				ending_.transform.parent=this.gameObject.transform;

				this.gameObject.GetComponent<ProfileSystem>().healthPoints+=200;
				this.gameObject.GetComponent<ProfileSystem>().MAX_HealthPoints+=200;
				this.gameObject.GetComponent<ProfileSystem>().DefendePoints+=50;
				this.gameObject.GetComponent<ProfileSystem>().meleeDamageDealt*=3;
				this.gameObject.GetComponent<ProfileSystem>().secondDamageDealt*=3;

				super_clone_runonce=false;
			}

			duration_timer+=Time.deltaTime;
			if(duration_timer>=duration){
				super_activation=false;
				canAttack=false;
			}
		}

		if(!super_activation){
			
			this.gameObject.GetComponent<ProfileSystem>().MAX_HealthPoints=100f;
			this.gameObject.GetComponent<ProfileSystem>().DefendePoints=0;
			this.gameObject.GetComponent<ProfileSystem>().meleeDamageDealt*=1;
			this.gameObject.GetComponent<ProfileSystem>().secondDamageDealt*=1;

			super_clone_runonce=true;
			Destroy(ending_);
			this.gameObject.transform.localScale =normal_hero_size;
		}


		if(!canAttack){
			timer-=Time.deltaTime;
			if(timer<=0){
				canAttack=true;
				timer=cooldown;
			}
		}

	}

	public float gettimer(){
		return timer;
	}

	public float getcooldown(){
		return cooldown;
	}
}
