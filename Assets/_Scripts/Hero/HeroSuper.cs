using UnityEngine;
using System.Collections;

public class HeroSuper : MonoBehaviour {

	public float SuperCoolDown=60f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		SuperCoolDown -= Time.deltaTime;
		if(SuperCoolDown<0){




		}

	
	}
}
