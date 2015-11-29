using UnityEngine;
using System.Collections;

public class MagicBallAttack : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start () {
		timer = HeroMagic.magicDistance/ HeroMagic.magicSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer<0){
			Destroy(this.gameObject);
		}

	}

	void OnCollisionEnter(Collision collision) {

		if(collision.gameObject.tag=="Enemy"){

			OverseerMinion.hp-=HeroMagic.magicDamage;

		}
		Destroy(this.gameObject);
	}
}
