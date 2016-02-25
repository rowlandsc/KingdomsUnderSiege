using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragonAI : MonoBehaviour {

	//定义怪物的四种状态：站立、行走、奔跑、无所事事
	public const int STATE_HOME=0;
	public const int STATE_ATTACK=1;
	public const int STATE_DEAD=2;

	private int NowState;

	private GameObject[] Hero;
	private GameObject HeroChoose;


	public const int AI_ATTACT_DISTANCE=15;
	public const int AI_ATTACT_MINDISTANCE = 10;


	private Vector3 ori_position;
	private Animator dragon_anim;
	private GameObject fireballstart;

	public GameObject powerball;
	private GameObject powerball_clone;

	private float timer;
	private bool canAttack;

	void Start () 
	{
		canAttack=true;
		timer=0;
		ori_position = this.transform.position;
		dragon_anim = this.gameObject.GetComponent<Animator>();
		fireballstart = GameObject.Find("DragonDPSPosition");
	}

	void Update () 
	{
		dragon_anim.SetInteger("NowState",NowState);


		if(GameObject.FindGameObjectsWithTag("Player").Length>0)   
		{
			Hero = GameObject.FindGameObjectsWithTag("Player");
			HeroChoose = Hero[0];
	
		
			if(Vector3.Distance(transform.position,HeroChoose.transform.position)<AI_ATTACT_DISTANCE)
			{

				this.transform.LookAt(HeroChoose.transform);

				if(canAttack){

					NowState=STATE_ATTACK;
					canAttack=false;

				}



					
			}else{
				
				if(NowState!=STATE_HOME){

					Quaternion mRotation=Quaternion.Euler(0,Random.Range(1,5)*90,0);
					this.transform.rotation=Quaternion.Slerp(transform.rotation,mRotation,Time.deltaTime*1000);

					//改变位置
					NowState=STATE_HOME;
				}
	
			}
		}



		if(!canAttack){
			timer+=Time.deltaTime;
			if(timer>0.1f){

				NowState=STATE_HOME;


			}


			if(timer>0.3f){
				
				NowState=STATE_HOME;


			}

			if(timer>2f){
				canAttack=true;
				timer=0;

			}
		}
	}

	public void Attack(){
		powerball_clone = Instantiate(powerball, fireballstart.transform.position, this.transform.rotation) as GameObject;
		powerball_clone.transform.DOMove(HeroChoose.transform.position,1f,false);
	}


}



