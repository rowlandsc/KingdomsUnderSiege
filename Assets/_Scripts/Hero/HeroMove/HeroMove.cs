﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HeroMove : MonoBehaviour {



	public float movespeed=1.5f;
	public float backwardspeed=1.0f;
	public float sidespeed=1.2f;

	static public bool CanMove=true;

	private GameObject Maincamera;
	private GameObject[] NewGameObject;

	private Vector3 cureent_position;
	public bool ifMoving;
	private Vector3 curPos, LastPos;

	// Use this for initialization
	void Start () {
		ifMoving=false;
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	
	// Update is called once per frame
	void Update () {

		curPos = this.gameObject.transform.position;

		if(Vector3.Distance(curPos - LastPos,new Vector3(0f,0f,0f)) <=0.1) {
			ifMoving=false;
		}
		else{
			ifMoving=true;
		}
		LastPos = curPos;

		if(Input.GetKey(KeyCode.W)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* movespeed, Space.World);
		}

		if(Input.GetKey(KeyCode.A)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.D)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.S)&&CanMove){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* backwardspeed, Space.World);
		}



	}

	static public void EnableMove(){
		CanMove = true;
	}

	static public void DisableMove(){
		CanMove = false;
	}
}
