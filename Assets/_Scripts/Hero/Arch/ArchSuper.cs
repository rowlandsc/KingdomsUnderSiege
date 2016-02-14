﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchSuper : MonoBehaviour {
	
	private float cooldown;
	private bool canAttack;
	private float skytime;
	public bool SuperActivate;
	private int shot_times_left;

	public float timer;
	
	public GameObject superbullet;
	private GameObject superbullet_clone;

	public GameObject superFx;
	private GameObject superFx_clone;
	private bool effect_apply;

	// Use this for initialization
	void Start () {
		cooldown = 10f;
		skytime=10f;
		shot_times_left=3;
		canAttack=true;
		SuperActivate=false;
		effect_apply=true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R)&&canAttack){
		
			canAttack = false;
			SuperActivate=true;

			this.gameObject.transform.DOMove(this.gameObject.transform.position+new Vector3(0f,2.5f,0f),0.5f,false);
	
		}

		if(SuperActivate){
			HeroMove.DisableMove();

			if(effect_apply){



				superFx_clone = Instantiate(superFx, this.gameObject.transform.position+new Vector3(0f,2.5f,0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject;
				effect_apply=false;


			}

			this.gameObject.GetComponent<Rigidbody>().useGravity=false;


			if(Input.GetMouseButtonDown(0)&&shot_times_left>0){
				shot_times_left-=1;

				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit))  {  

					if(hit.transform.gameObject.tag!="Player"){
						superbullet_clone = Instantiate(superbullet, this.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
						superbullet_clone.transform.DOMove(hit.point,2f,false);
					}
				} 
			}

		
			skytime-=Time.deltaTime;
			if(skytime<=0||shot_times_left<=0){
				this.gameObject.GetComponent<Rigidbody>().useGravity=true;
				SuperActivate=false;
				Destroy(superFx_clone);
				effect_apply=true;
			}

		}


		if(!canAttack&&!SuperActivate){
			HeroMove.EnableMove();
			timer -= Time.deltaTime;
			if(timer<=0){
				canAttack = true;	
				timer = cooldown;
			}
		}
	}
}
