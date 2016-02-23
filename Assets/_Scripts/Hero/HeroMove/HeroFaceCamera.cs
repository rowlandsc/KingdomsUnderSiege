﻿using UnityEngine;
using System.Collections;

public class HeroFaceCamera : MonoBehaviour {

	private GameObject Maincamera;

	float rotateSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	
	// Update is called once per frame
	void Update () {
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

		if(this.gameObject.name=="Mage(Clone)"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}
		if(this.gameObject.name=="Mage"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}

		if(this.gameObject.name=="Knight(Clone)"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y-90f,0);}
		if(this.gameObject.name=="Knight"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y-90f,0);}

		if(this.gameObject.name=="Arch(Clone)"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}
		if(this.gameObject.name=="Arch"){transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}

	}
}
