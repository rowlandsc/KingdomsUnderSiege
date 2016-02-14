using UnityEngine;
using System.Collections;

public class KnightSuper : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;



	private float cooldown;
	private float duration;
	
	private bool canAttack;
	public float timer;
	private bool super_activation;
	private float duration_timer;

	private Vector3 normal_hero_size;
	private Vector3 super_hero_size;

	//FX
	public GameObject super;
	private GameObject super_clone;
	private bool super_clone_runonce;

	// Use this for initialization
	void Start () {

		cooldown=30f;
		duration=10f;

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
	
		if(Input.GetKeyDown(KeyCode.R)&&canAttack&&!super_activation){

			this.gameObject.transform.localScale =super_hero_size;
			super_activation=true;
		}

		if(super_activation){
			//apply hero_increased_effect here
			if(super_clone_runonce){
				super_clone= Instantiate(super, this.transform.position, transform.rotation)as GameObject;
				super_clone.transform.parent=this.gameObject.transform;

				ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
				ending_.transform.parent=this.gameObject.transform;

				super_clone_runonce=false;
			}

			duration_timer+=Time.deltaTime;
			if(duration_timer>=duration){
				super_activation=false;
				canAttack=false;
			}
		}

		if(!super_activation){
			//apply un_hero_increased_effect here


			super_clone_runonce=true;
			Destroy(super_clone);
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
}
