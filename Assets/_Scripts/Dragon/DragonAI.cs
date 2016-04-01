using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragonAI : MonoBehaviour {


	public const int STATE_HOME=0;
	public const int STATE_ATTACK=1;
	public const int STATE_DEAD=2;
	public const int STATE_SECOND=3;

	public int NowState;

	private GameObject[] Hero;
	private GameObject HeroChoose;


	public const int AI_ATTACT_DISTANCE=15;
	public const int AI_ATTACT_MINDISTANCE = 10;


	private Vector3 ori_position;
	private Animator dragon_anim;
	private GameObject fireballstart;

	public GameObject powerball;
	private GameObject powerball_clone;

	public GameObject ground;
	private GameObject ground_clone;

	private float timer;
	private float timer2;
	private bool canAttack;
	private int attacktimes;
	private bool canAttackSecond;

	void Start () 
	{
		canAttack=true;
		attacktimes=0;
		canAttackSecond=false;
		timer=0;
		timer2=0;
		ori_position = this.transform.position;
		dragon_anim = this.gameObject.GetComponent<Animator>();
		fireballstart = GameObject.Find("DragonDPSPosition");
	}

	void Update () 
	{
		dragon_anim.SetInteger("NowState",NowState);

		if(NowState!=STATE_DEAD){

		if(GameObject.FindGameObjectsWithTag("Player").Length>0)   
		{
			Hero = GameObject.FindGameObjectsWithTag("Player");
				int choose_ = Random.Range(0,Hero.Length);

				HeroChoose = Hero[choose_];
	
		
			if(Vector3.Distance(transform.position,HeroChoose.transform.position)<AI_ATTACT_DISTANCE){

				this.gameObject.GetComponent<ProfileSystem>().HealthRegen=0;
				this.transform.LookAt(HeroChoose.transform);

				if(canAttack&&attacktimes<=4){

					NowState=STATE_ATTACK;
					attacktimes++;
					canAttack=false;

				}
				else if(attacktimes>4){
					canAttackSecond=true;
					canAttack=false;
					NowState=STATE_SECOND;
				}



			}else{

				this.gameObject.GetComponent<ProfileSystem>().HealthRegen=200;

				if(NowState!=STATE_HOME){

					Quaternion mRotation=Quaternion.Euler(0,Random.Range(1,5)*90,0);
					this.transform.rotation=Quaternion.Slerp(transform.rotation,mRotation,Time.deltaTime*1000);

					NowState=STATE_HOME;
				}
			}
		}



		if(!canAttack&&!canAttackSecond){
			timer+=Time.deltaTime;
			if(timer>0.3f){
				NowState=STATE_HOME;
			}
				
			if(timer>2f){
				canAttack=true;
				timer=0;

			}
		}

		if(canAttackSecond){
			timer2+=Time.deltaTime;

			if(timer2>1.5f){
				NowState=STATE_HOME;
				canAttackSecond=false;
				canAttack=true;
				timer2=0;
				attacktimes=0;
			}
		}
		}
	}

	public void Attack(){
		powerball_clone = Instantiate(powerball, fireballstart.transform.position, this.transform.rotation) as GameObject;
		powerball_clone.transform.DOMove(HeroChoose.transform.position,1f,false);
	}

	public void SecondAttack(){

		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(0F, 10F),19.99634f,Random.Range(0, 10F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(-10F, 0F),19.99634f,Random.Range(0, 10F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(0F, 10F),19.99634f,Random.Range(-10, 0F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(-10F, 0F),19.99634f,Random.Range(-10, 0F)), this.transform.rotation) as GameObject;

		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(0F, 10F),19.99634f,Random.Range(0, 10F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(-10F, 0F),19.99634f,Random.Range(0, 10F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(0F, 10F),19.99634f,Random.Range(-10, 0F)), this.transform.rotation) as GameObject;
		ground_clone = Instantiate(ground, this.gameObject.transform.position+new Vector3(Random.Range(-10F, 0F),19.99634f,Random.Range(-10, 0F)), this.transform.rotation) as GameObject;
	}


}



