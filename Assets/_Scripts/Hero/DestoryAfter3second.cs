using UnityEngine;
using System.Collections;

public class DestoryAfter3second : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start () {
		timer=0f;
	}

	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>=0.5f){
			timer=0f;
			Destroy(this.gameObject);
		}
	}
}