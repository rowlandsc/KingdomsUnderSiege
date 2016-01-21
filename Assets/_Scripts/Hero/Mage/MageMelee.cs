﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MageMelee : MonoBehaviour {

	public GameObject iceball;
	public float cooldown = 0.5f;

	private bool canAttack;
	private float timer;

	private GameObject iceball_clone;
	
	// Use this for initialization
	void Start () {
		canAttack = true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;

			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;


			if (Physics.Raycast(ray, out hit))  {  
				if(hit.transform.gameObject.tag!="Player"){
					iceball_clone = Instantiate(iceball, this.transform.position, transform.rotation) as GameObject;
					iceball_clone.transform.DOMove(hit.point,0.3f,false);
				}
			} 
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
