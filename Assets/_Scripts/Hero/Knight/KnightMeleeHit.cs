using UnityEngine;
using System.Collections;

public class KnightMeleeHit : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		
		bool canAttack=!gameObject.GetComponentInParent<KnightMelee>().canAttack;

		if(canAttack){
			if(col.tag=="OverseerPlayer"){
				Destroy(col.gameObject);}
		}

	}
}
