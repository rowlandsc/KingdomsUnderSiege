using UnityEngine;
using System.Collections;

public class KnightMeleeHit : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player=GameObject.Find("Knight(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		
		bool canAttack=!gameObject.GetComponentInParent<KnightMelee>().canAttack;

		if(canAttack){
			if(col.tag=="OverseerPlayer"){

				if(col.gameObject.GetComponent<ProfileSystem>()){

					if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().MeleeDamageDealt))
					{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}}

				}
		}

	}
}
