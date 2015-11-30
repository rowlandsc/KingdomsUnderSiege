using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Transform camera;
	private float ratio=1f;

	// Use this for initialization
	void Start () {
        this.camera = GameObject.Find("Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.forward = new Vector3(transform.position.x-camera.position.x , 0, transform.position.z-camera.position.z);

		ratio = (GetComponentInParent<Health>().HitPoints)/100;

		Vector3 temp = this.transform.localScale;
		temp.x = -1*ratio;
		this.transform.localScale = temp;

	}
}
