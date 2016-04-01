using UnityEngine;
using System.Collections;


public class HealthBar : MonoBehaviour {

	private Transform camera;
	private float defalut_ratio;
	private float ratio;

	// Use this for initialization
	void Start () {
		defalut_ratio = Mathf.Abs(this.gameObject.transform.localScale.x);
		this.camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

		this.transform.forward = new Vector3(transform.position.x-camera.position.x , 0, transform.position.z-camera.position.z);

		if(GetComponentInParent<ProfileSystem>()){
			ratio = ((GetComponentInParent<ProfileSystem>().HealthPoints)/(GetComponentInParent<ProfileSystem>().MAX_HealthPoints))*defalut_ratio;}

		Vector3 temp = this.transform.localScale;
		temp.x = -1*ratio;
		this.transform.localScale = temp;

	}
}
