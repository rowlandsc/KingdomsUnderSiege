
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using DG.Tweening;

public class ArchSecond : MonoBehaviour {

	public GameObject electric;
	public float mp_use=10f;

	public float cooldown=4f;
	public float distance=30f;

	private bool canAttack;
	public float timer;

	private GameObject electric_clone;

    private NetworkPlayerInput _playerInput;

	// Use this for initialization
	void Start () {
		
		canAttack = true;
		timer = cooldown;

        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }

	// Update is called once per frame
	void Update () {

		if(_playerInput.HeroMeleeChargeAttackInputDown > 0 && canAttack && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use))
		{
			canAttack = false;


			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;

			int layerMask = new int();
			layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
			layerMask = ~layerMask;

			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  {  
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.point); 

				if(real_distance<=distance){
					this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);
					electric_clone = Instantiate(electric, hit.point, Quaternion.LookRotation(ray.direction)) as GameObject;
                    electric_clone.GetComponent<ArchSecondHit>().Initialize(GetComponent<NetworkIdentity>());
				}
				else{
					canAttack = true;
				}


			} 
		}

	
		if(!canAttack){
			timer -= Time.deltaTime;

			if(timer<=0){
				canAttack = true;	
				timer = cooldown;
			}
		}




	}

	public float gettimer(){
		return timer;
	}

	public float getcooldown(){
		return cooldown;
	}
		
}
