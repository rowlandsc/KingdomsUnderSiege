using UnityEngine;
using System.Collections;

public class SuperHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	private float kill_time;
	private float memory_saving_timer;
	
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

		ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
		ending.AddComponent<DestoryselfAfterfewsecond>();

		if(col.GetComponent<Testmove>())
		{col.GetComponent<Testmove>().canMove=true;}
	
		Destroy(col.gameObject);
		Destroy(this.gameObject);
		
	}
	
	
}
