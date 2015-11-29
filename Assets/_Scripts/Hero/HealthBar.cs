using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Transform camera;
	private float ratio=1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.forward = new Vector3(transform.position.x-camera.position.x , 0, transform.position.z-camera.position.z);

		ratio = (OverseerMinion.hp)/100;

		print (ratio);

		Vector3 temp = this.transform.localScale;
		temp.x = -1*ratio;
		this.transform.localScale = temp;

	}
}
