using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Testmove : MonoBehaviour {

	public bool canMove;
	public GameObject FreezeAnim;
	private bool canProduceAnim;

	private float timer=0;
	private float FreezeTimer=0;
	private GameObject FreezeAnim_;

	// Use this for initialization
	void Start () {
		canMove = true;
		canProduceAnim=true;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 moveDirection = this.gameObject.transform.forward;


		timer+=Time.deltaTime;

		if(timer<=3&&canMove){
			transform.Translate(moveDirection * Time.deltaTime, Space.World);
		}
		else if(timer>3&&timer<=6&&canMove){
			transform.Translate(-moveDirection * Time.deltaTime, Space.World);

		}
		else{
			timer=0;
		}

		if(!canMove){
			if(canProduceAnim){
				canProduceAnim=false;
				this.transform.gameObject.tag = "Freezed";
				FreezeAnim_ = Instantiate(FreezeAnim, this.transform.position, transform.rotation) as GameObject;
				FreezeAnim_.transform.parent=this.transform;
			}


			FreezeTimer+=Time.deltaTime;

			if(FreezeTimer>=SecondHit.FreezeTime){
				this.transform.gameObject.tag = "Enemy";
				Destroy(FreezeAnim_);
				canMove=true;
				canProduceAnim=true;
				FreezeTimer=0;
			}
		}
	}
}
