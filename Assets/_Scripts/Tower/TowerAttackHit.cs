using UnityEngine;
using System.Collections;

public class TowerAttackHit : MonoBehaviour {

	public GameObject tower ;

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

		if(col.gameObject.tag!="OverseerPlayer"){

			print("I hit it!");

			if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(tower.GetComponent<ProfileSystem>().meleeDamageDealt))
				{tower.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

			}}

		Destroy(this.gameObject);
	}
}
