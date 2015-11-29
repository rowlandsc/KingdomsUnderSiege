﻿using UnityEngine;
using System.Collections;

public class HeroMelee : MonoBehaviour {

	public float meleedamage=100;
	public float meleeDistance = 0.5f;
	private bool pressyes = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0)){
			pressyes = true;
		}

		if(Input.GetMouseButtonUp(0)){
			pressyes = false;
		}

	}


	void OnCollisionEnter(Collision collision) {
		
		if(collision.gameObject.tag=="Enemy"&&pressyes){

			print ("you hit it!");
			OverseerMinion.hp-=meleedamage;
			
		}

	}

}
