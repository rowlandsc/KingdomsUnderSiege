using UnityEngine;
using System.Collections;

public class MagicBallAttack : MonoBehaviour {
    public string Enemy;
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
        
		if(collision.gameObject.tag == Enemy){
            Debug.Log("MAGICBALL HIT THE " + collision.gameObject.name + "\n" +
                "It did " + HeroMagic.magicDamage + " to it.\n The health went from " + collision.gameObject.GetComponent<Health>().HitPoints);
			collision.gameObject.GetComponent<Health>().HitPoints-=HeroMagic.magicDamage;
            Debug.Log(" to " + collision.gameObject.GetComponent<Health>().HitPoints);
		}
		Destroy(this.gameObject);
	}
}
