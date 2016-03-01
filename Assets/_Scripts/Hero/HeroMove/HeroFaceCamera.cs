using UnityEngine;
using System.Collections;

public class HeroFaceCamera : MonoBehaviour {

	private GameObject Maincamera;

	float rotateSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	
	// Update is called once per frame
	void Update () {
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

		if(this.gameObject.name=="Mage(Clone)"){

			this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}
			//transform.rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);}
		if(this.gameObject.name=="Mage") this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);

		if(this.gameObject.name=="Knight(Clone)") this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y-90f,0);
		if(this.gameObject.name=="Knight")this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y-90f,0);

		if(this.gameObject.name=="Arch(Clone)")this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);
		if(this.gameObject.name=="Arch")this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,Maincamera.transform.eulerAngles.y,0);

	}
}
