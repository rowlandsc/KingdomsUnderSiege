using UnityEngine;
using System.Collections;

public class KnightSecondHit : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player=GameObject.Find("Knight(Clone)");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider col){

		if(col.tag=="OverseerPlayer"){


				if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().secondDamageDealt*0.05f))
					{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}}

		}


	}
}
