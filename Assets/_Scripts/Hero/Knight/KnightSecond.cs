﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KnightSecond : MonoBehaviour {

	private float effect_time;
	private float cooldown;

	private bool canAttack;
	private bool Startcooldown;
	
	public float timer;
	private float timer_for_max_effect;
	public bool knightsecond_Activate;
	private float movespeed;

	private GameObject Maincamera;


	//FX
	public GameObject effect_fire;
	private GameObject effect_fire_clone;
	private bool effect_fire_clone_runonce;

	// Use this for initialization
	void Start () {
		effect_time=2f;
		cooldown=10f;

		canAttack = true;
		Startcooldown = false;
		timer = cooldown;
		timer_for_max_effect=0f;
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
		movespeed = gameObject.GetComponent<HeroMove>().movespeed;
		knightsecond_Activate=false;
		effect_fire_clone_runonce=true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButton(1)&&canAttack)
		{
			knightsecond_Activate=true;

			timer_for_max_effect+=Time.deltaTime;
			if(timer_for_max_effect>=effect_time){
				knightsecond_Activate=false;
				timer_for_max_effect=0;
				canAttack=false;
			}

		}

		if(Input.GetMouseButtonUp(1)&&knightsecond_Activate)
		{
			knightsecond_Activate=false;
			canAttack=false;
		}


		if(knightsecond_Activate){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);
			if(Input.GetKey(KeyCode.W)){transform.Translate(moveDirection * Time.deltaTime* movespeed*1.2f, Space.World);}
			else if(!Input.GetKey(KeyCode.W)){transform.Translate(moveDirection * Time.deltaTime* movespeed*1.5f, Space.World);}

			if(effect_fire_clone_runonce){
				effect_fire_clone= Instantiate(effect_fire, this.transform.position, transform.rotation)as GameObject;
				effect_fire_clone.transform.parent=this.gameObject.transform;
				effect_fire_clone_runonce=false;
			}
		}

		if(!knightsecond_Activate){
			Destroy(effect_fire_clone);
			effect_fire_clone_runonce=true;
		}

		if(!canAttack){
			timer -= Time.deltaTime;
			if(timer<0){
				canAttack = true;	
				timer = cooldown;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(knightsecond_Activate){
			Destroy(col.gameObject);
		}
		//apply confusion effect here
	}
}
