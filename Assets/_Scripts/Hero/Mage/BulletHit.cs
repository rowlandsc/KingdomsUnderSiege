using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollision(Collision col)
	{
		print ("nice");
		Destroy(this.gameObject);
	}
}
