using UnityEngine;
using System.Collections;

public class Meleehit : MonoBehaviour {

	public float kill_time=10f;
	private float memory_saving_timer=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;
		
		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);

		}
	}
	
	
	void OnTriggerEnter(Collider col){


	}
	

}
