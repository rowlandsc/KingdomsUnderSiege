using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MortarTowerAttack : MonoBehaviour {


	public GameObject attack;
	private GameObject attack_;
    public GameObject ShootPoint;

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
            Vector2 direction = (target != null) ? new Vector2(target.transform.position.x, target.transform.position.z) - new Vector2(transform.position.x, transform.position.z) : Vector2.zero;
            if (target==null || 
                Vector3.Distance(this.gameObject.transform.position,target.transform.position) > profile.AttackRange ||
                Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), direction) > 50)

                findTarget();

			if(target!=null){
                attack_=Instantiate(attack,ShootPoint.transform.position,Quaternion.identity)as GameObject;
                attack_.GetComponent<MortarTowerHit>().Initialize(gameObject);
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
        /*GameObject archer = GameObject.Find("Arch(Clone)");
        Vector2 direction2 = new Vector2(archer.transform.position.x, archer.transform.position.z) - new Vector2(transform.position.x, transform.position.z);
        float angle2 = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), direction2);
        Debug.Log(angle2 + " " + Vector3.Distance(this.gameObject.transform.position, archer.transform.position));
        */
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("HeroPlayer");
        target = null;
		for(int i=0;i<targetList.Length;i++){
            Vector2 direction = new Vector2(targetList[i].transform.position.x, targetList[i].transform.position.z) - new Vector2(transform.position.x, transform.position.z);
            float angle = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), direction);
            //Debug.Log(angle + " " + Vector3.Distance(this.gameObject.transform.position, targetList[i].transform.position));
            if (Vector3.Distance(this.gameObject.transform.position,targetList[i].transform.position) < profile.AttackRange && 
                angle <= 50){
                
				target = targetList[i];
                Debug.Log("Found " + target.gameObject.name);
				break;
			}
		}
	}
}
