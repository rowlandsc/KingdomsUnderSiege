using UnityEngine;
using System.Collections;

public class HeroRespawn : MonoBehaviour {


	public bool need_rspawn;
	public string hero_name;
	private float timer;

	// Use this for initialization
	void Start () {
		timer=0f;
		need_rspawn=false;
	
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>20f){

			timer=0f;
		}
	
	}


}
