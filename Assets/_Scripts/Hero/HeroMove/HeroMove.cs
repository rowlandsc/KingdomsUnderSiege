using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HeroMove : MonoBehaviour {

	public float movespeed=1.0f;
	public float backwardspeed=1.0f;
	public float sidespeed=1.0f;

	private GameObject Maincamera;

	// Use this for initialization
	void Start () {
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.W)){

			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* movespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.A)){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.D)){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.S)){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* backwardspeed, Space.World);
		}



	
	}
}
