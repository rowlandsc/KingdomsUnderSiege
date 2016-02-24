using UnityEngine;
using System.Collections;

public class DragonSuperHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;
	private GameObject dragon;

	private float kill_time;
	private float memory_saving_timer;

	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
		dragon=GameObject.Find("Dragon");
	}

	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);

		}
	}


	void OnTriggerEnter(Collider col){


		if(col.tag!="IceBullet"&&col.name!="Dragon"&&col.name!="HealthBar"&&col.tag!="MainCamera"){

			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
			ending.AddComponent<DestoryselfAfterfewsecond>();


			if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(10f));


			}

			Destroy(this.gameObject);
		}

	}


}
