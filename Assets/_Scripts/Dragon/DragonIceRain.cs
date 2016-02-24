using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragonIceRain : MonoBehaviour {

	public GameObject superBallanim;
	private GameObject superBallanim_;

	private Transform camera;

	private float timer;
	private GameObject[] player;

	// Use this for initialization
	void Start () {
		timer=0;
	}
	
	// Update is called once per frame
	void Update () {

		this.camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
		this.transform.forward = new Vector3(transform.position.x-camera.position.x , 0f, transform.position.z-camera.position.z);

		player = GameObject.FindGameObjectsWithTag("Player");

		for(int i = 0; i < player.Length; i++)
		{
			if(Vector3.Distance(player[i].transform.position,this.gameObject.transform.position)<20f){
				timer+=Time.deltaTime;
			}

		}


		if(timer>2f){
			
			for(int i = 0; i < player.Length; i++)
			{
				superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,500,0), this.transform.rotation) as GameObject;
				superBallanim_.transform.DOLocalMove(player[i].transform.position,0.5f,false);

				superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,500,0), this.transform.rotation) as GameObject;
				superBallanim_.transform.DOLocalMove(player[i].transform.position+new Vector3(3f,0f,3f),1f,false);

				superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,500,0), this.transform.rotation) as GameObject;
				superBallanim_.transform.DOLocalMove(player[i].transform.position+new Vector3(-3f,0f,-3f),1f,false);

			}

			timer=0;
		}




	
	}


}
