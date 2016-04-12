using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MortarTowerAttack : MonoBehaviour {


	public GameObject attack;
	private GameObject attack_;

	private GameObject target;

	private float attackDamage;
	public float range;
	public float Arrowspeed;

	private bool canAttack;
	private float rest_time = 2f;
	private float timer;

    private ProfileSystem profile;

	// Use this for initialization
	void Start () {
		timer=0;
		canAttack=true;
        profile = GetComponent<ProfileSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(canAttack){
			if(target==null || Vector3.Distance(this.gameObject.transform.position,target.transform.position) > profile.AttackRange) findTarget();
			if(target!=null){
                attack_=Instantiate(attack,this.gameObject.transform.position,Quaternion.identity)as GameObject;
				attack_.GetComponent<MortarTowerHit>().Tower = this.gameObject;
				attack_.GetComponent<MortarTowerHit>().canShot = true;
				attack_.GetComponent<MortarTowerHit>().hitPosition = target.transform.position;

                canAttack =false;
			}
		}

		if(!canAttack){
			timer+=Time.deltaTime;
			if(timer > profile.AttackFrequency) {
				canAttack=true;
				timer=0;
			}
		}


	}

	public void findTarget(){

		GameObject[] targetList = GameObject.FindGameObjectsWithTag("HeroPlayer");
        target = null;
		for(int i=0;i<targetList.Length;i++){
			if(Vector3.Distance(this.gameObject.transform.position,targetList[i].transform.position)<range){
				target = targetList[i];
				break;
			}
		}
	}
}
