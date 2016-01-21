using UnityEngine;
using System.Collections;

public class SuperHit : MonoBehaviour {

	public GameObject hitanim_;

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
			Destroy(hitanim_);
		}
	}
	
	
	void OnTriggerEnter(Collider col){
		Instantiate(hitanim_, this.transform.position, transform.rotation);
		if(col.GetComponent<Testmove>())
		{col.GetComponent<Testmove>().canMove=true;}
		Destroy(col.gameObject);
		Destroy(this.gameObject);
		
	}
	
	
}
