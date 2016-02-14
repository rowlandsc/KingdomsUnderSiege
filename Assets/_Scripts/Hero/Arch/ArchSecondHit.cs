using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchSecondHit : MonoBehaviour {

	private float kill_time;
	private float memory_saving_timer;

	private Vector3 hitposition;
	
	private GameObject player;

	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;
		
		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);
			
		}
	}
	
	
	void OnTriggerEnter(Collider col){
		if(col.tag=="OverseerPlayer"){
			print("you hit");

			player.GetComponent<ArchSecond>().ifhit=true;
			Destroy(col.gameObject);
		}

		if(col.gameObject.tag!="Player"){
			Destroy(this.gameObject);}
	}


}
