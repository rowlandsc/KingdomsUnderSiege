﻿
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchSecond : MonoBehaviour {

	public GameObject electric;
	public float mp_use=10f;

	public float cooldown=4f;
	public float distance=20f;

	private bool canAttack;
	public float timer;

	private GameObject electric_clone;

	// Use this for initialization
	void Start () {

		canAttack = true;
		timer = cooldown;

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(1)&&canAttack&&this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use))
		{
			canAttack = false;


			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;

			int layerMask = new int();
			layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
			layerMask = ~layerMask;

			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  {  
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.point); 

				if(real_distance<=distance){
					this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);
					electric_clone = Instantiate(electric, hit.point, Quaternion.LookRotation(ray.direction)) as GameObject;
				}
				else{
					canAttack = true;
				}


			} 
		}

	
		if(!canAttack){
			timer -= Time.deltaTime;

			if(timer<=0){
				canAttack = true;	
				timer = cooldown;
			}
		}




	}
		
}
