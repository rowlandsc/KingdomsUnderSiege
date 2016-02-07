using UnityEngine;
using System.Collections;

public class KnightMelee : MonoBehaviour {

	private float cooldown;
		
	public bool canAttack;
	public float timer;

	
	// Use this for initialization
	void Start () {
		cooldown = 0.5f;

		canAttack = true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;
			
			Animator anim=this.gameObject.GetComponent<Animator>();
			anim.Play("UseSword");
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
