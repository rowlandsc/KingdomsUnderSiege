using UnityEngine;
using System.Collections;

public class KnightMelee : MonoBehaviour {

	public GameObject anim_;
	private GameObject anim_clone;

	public float cooldown=0.5f;
		
	public bool canAttack;
	public float timer;

	private GameObject Maincamera;
	private GameObject sword;
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		

		canAttack = true;
		timer = cooldown;
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;
		

			sword = GameObject.Find("HeroKnightSwordV01");
			anim = sword.gameObject.GetComponentInChildren<Animator>();
			anim.Play("UseSword");

			anim_clone = Instantiate(anim_, this.transform.position,Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0)) as GameObject;
			anim_clone.transform.parent=this.gameObject.transform;
			anim_clone.AddComponent<DestoryAfter3second>();

		}
		
		if(!canAttack){
			timer -= Time.deltaTime;
			if(timer<0){
				canAttack = true;	
				timer = cooldown;
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
