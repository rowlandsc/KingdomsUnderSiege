using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Transform camera;
	private float ratio=0.02453401f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.forward = new Vector3(transform.position.x-camera.position.x , 0, transform.position.z-camera.position.z);

		ratio = (OverseerMinion.hp)/100;

		Vector3 temp = this.transform.localScale;
		temp.x = -0.02453401f*ratio;
		this.transform.localScale = temp;

	}
}
