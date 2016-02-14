using UnityEngine;
using System.Collections;

public class KnightMelee : MonoBehaviour {

	public GameObject anim_;
	private GameObject anim_clone;

	public float cooldown;
		
	public bool canAttack;
	public float timer;

	private GameObject Maincamera;
	
	// Use this for initialization
	void Start () {
		cooldown = 0.5f;

		canAttack = true;
		timer = cooldown;

		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;
			
			Animator anim=this.gameObject.GetComponent<Animator>();
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
}
