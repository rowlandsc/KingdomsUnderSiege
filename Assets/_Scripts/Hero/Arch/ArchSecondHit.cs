using UnityEngine;
using System.Collections;

public class ArchSecondHit : MonoBehaviour {

	public float effect_time;
	private float timer;

	private GameObject player;

	// Use this for initialization
	void Start () {
		effect_time=10f;
		timer=effect_time;
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		timer-=Time.deltaTime;
		if(timer<=0f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider col){

		if(col.tag=="OverseerPlayer"){

			if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().secondDamageDealt*0.01f))
				{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

			}
		}
			
	}

}
