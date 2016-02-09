using UnityEngine;
using System.Collections;

public class HeroRoundManager : MonoBehaviour {

	private float preround_time;
	private float round_time;
	private float timer;
	//true means pre-round, false means round
	private bool cureent_status;
	
	private GameObject door;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		preround_time=5f;
		round_time=10f;
		timer=0;
		door= GameObject.Find("2partDoor");
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;

		if(timer>=preround_time&&cureent_status==true){
			cureent_status=false;
			timer=0f;
		}

		if(timer>=round_time&&cureent_status==false){
			cureent_status=true;
			timer=0f;
		}

		print(cureent_status);

		//pre-round
		if(cureent_status==true){
			//close the door
			door.SetActive(true);
		}

		//round
		if(cureent_status==false){
			//open the door
			door.SetActive(false);
		}
	}
	
}
