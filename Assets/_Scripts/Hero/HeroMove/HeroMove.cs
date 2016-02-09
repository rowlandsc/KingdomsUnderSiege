using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HeroMove : MonoBehaviour {

	public float movespeed=1.2f;
	public float backwardspeed=1.0f;
	public float sidespeed=1.1f;

	static public bool CanMove=true;

	private GameObject Maincamera;
	private GameObject[] NewGameObject;

	// Use this for initialization
	void Start () {
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.W)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* movespeed, Space.World);
		}

		if(Input.GetKey(KeyCode.A)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.D)&&CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(moveDirection * Time.deltaTime* sidespeed, Space.World);
		}
		if(Input.GetKey(KeyCode.S)&&CanMove){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			transform.Translate(-moveDirection * Time.deltaTime* backwardspeed, Space.World);
		}


		NewGameObject=GameObject.FindGameObjectsWithTag("Untagged"); 
		for(int i = 0; i < (NewGameObject.Length); i++)
		{
			if(NewGameObject[i].name=="New Game Object");
			{Destroy(NewGameObject[i]);}
			
		}
	}

	static public void EnableMove(){
		CanMove = true;
	}

	static public void DisableMove(){
		CanMove = false;
	}
}
