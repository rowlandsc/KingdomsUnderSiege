using UnityEngine;
using System.Collections;

public class DragonSecondHit : MonoBehaviour {

	private GameObject dragon;
	private float damage;

	public GameObject ending;
	private GameObject ending_;

	private float kill_time;
	private float memory_saving_timer;

	// Use this for initialization
	void Start () {
		kill_time=Random.Range(10f,12f);
		memory_saving_timer=0f;
		dragon=GameObject.Find("ChaDragon");
		damage = dragon.GetComponent<ProfileSystem>().returnSecondDamage();
	}

	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);

		}
	}


	void OnTriggerStay(Collider col){


		if(col.tag!="OverseerPlayer"&&col.name!="HealthBar"&&col.tag!="IceBullet"){
			
			if(col.gameObject.GetComponent<ProfileSystem>()){
				col.gameObject.GetComponent<ProfileSystem>().KillAndGains(damage*0.05f);
			}

		 


		}

	}
}

