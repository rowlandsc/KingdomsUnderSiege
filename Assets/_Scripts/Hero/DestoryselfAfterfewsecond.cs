using UnityEngine;
using System.Collections;

public class DestoryselfAfterfewsecond : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start () {
		timer=0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>=9f){
			timer=0f;
			Destroy(this.gameObject);
		}
	}
}
