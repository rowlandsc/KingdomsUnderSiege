using UnityEngine;
using System.Collections;

public class ArchSecondHit_Linked : MonoBehaviour {
	private float kill_time;
	private float memory_saving_timer;
	
	private Vector3 hitposition;

	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
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

			Destroy(col.gameObject);
		}
	}
	

}
