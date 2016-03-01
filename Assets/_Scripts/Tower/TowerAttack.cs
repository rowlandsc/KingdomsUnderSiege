using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TowerAttack : MonoBehaviour {


	public GameObject attack;
	private GameObject attack_;

	private GameObject target;

	private float attackDamage;
	public float range;
	public float Arrowspeed;

	private bool canAttack;
	private float rest_time = 2f;
	private float timer;


	// Use this for initialization
	void Start () {
		timer=0;
		canAttack=true;
		attackDamage= this.gameObject.GetComponent<ProfileSystem>().returnMeleeDamage();
	}
	
	// Update is called once per frame
	void Update () {

		if(canAttack){

			if(target==null || Vector3.Distance(this.gameObject.transform.position,target.transform.position)>range) findTarget();
			if(target!=null){



				attack_=Instantiate(attack,this.gameObject.transform.position,Quaternion.identity)as GameObject;
				attack_.GetComponent<TowerAttackHit>().tower = this.gameObject;
				attack_.transform.DOMove(target.transform.position,(Vector3.Distance(this.gameObject.transform.position,target.transform.position)/Arrowspeed),false);

				canAttack=false;
			}
			

		}

		if(!canAttack){
			timer+=Time.deltaTime;
			if(timer>rest_time){
				canAttack=true;
				timer=0;
			}
		}


	}

	public void findTarget(){

		GameObject[] targetList = GameObject.FindGameObjectsWithTag("Player");

		for(int i=0;i<targetList.Length;i++){
			if(Vector3.Distance(this.gameObject.transform.position,targetList[i].transform.position)<range){
				target = targetList[i];
				break;
			}
		}
	}
}
