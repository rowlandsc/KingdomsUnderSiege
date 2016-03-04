using UnityEngine;
using System.Collections;

public class HeroRoundManager : MonoBehaviour {

	private float preround_time;
	private float round_time;
	private float timer;
	//true means pre-round, false means round
	private bool cureent_status;
	
	private GameObject door;
	private GameObject roundmanagerfinder;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		preround_time=5f;
		round_time=10f;
		timer=0;
		door= GameObject.Find("2partDoor");
	}
	
	// Update is called once per frame
	void Update () {

		if(roundmanagerfinder=GameObject.Find("RoundManager")){


			//pre round
			if(roundmanagerfinder.GetComponent<RoundManager>().IsPreround==true){
				//close the door
				door.SetActive(true);
			}

			//round
			if(roundmanagerfinder.GetComponent<RoundManager>().IsRound==false){
				//open the door
				door.SetActive(false);
			}


		}

	}
	
}
