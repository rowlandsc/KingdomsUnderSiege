using UnityEngine;
using System.Collections;

public class BulletDisaperWhenTooFar : MonoBehaviour {

	public float iceballdistance=10f;
	public float iceBulletdistance=20f;

	private GameObject player;
	private float distance;
	private bool kill;

	// Use this for initialization
	void Start () {

		kill = true;
		if(this.gameObject.tag=="IceBall"){distance=iceballdistance;}
		if(this.gameObject.tag=="IceBullet"){distance=iceBulletdistance;}

		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
		float real_distance = Vector3.Distance(player.transform.position,this.transform.position);
        if(real_distance>distance&&kill){
			Destroy(this.gameObject);
		}

	}

	void OnCollision(Collision collisionInfo)
	{
		kill = false;
	}
}
