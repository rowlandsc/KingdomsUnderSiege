using UnityEngine;
using System.Collections;

public class SecondHit : MonoBehaviour {



	private float kill_time=2f;
	static public float FreezeTime=5f;

	private GameObject hit = new GameObject();
	private float memory_saving_timer=0;
	private float freeze_timer=0;

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

		FreezeEffect(col.gameObject);
	}

	void FreezeEffect(GameObject temp){
		if(temp.GetComponent<Testmove>())
			temp.GetComponent<Testmove>().canMove=false;
	}

	void UnFreezeEffect(GameObject temp){
		if(temp.GetComponent<Testmove>())
			temp.GetComponent<Testmove>().canMove=true;
	}
}
