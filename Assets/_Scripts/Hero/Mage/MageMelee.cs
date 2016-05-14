using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Networking;

public class MageMelee : MonoBehaviour {

	public GameObject iceball;
	public float cooldown=0.5f;

	private bool canAttack;
	public float timer;

	private GameObject spellPosition;
	private GameObject iceball_clone;

    private NetworkPlayerInput _playerInput;
	
	// Use this for initialization
	void Start () {

		spellPosition = GameObject.Find("SpellPosition");
		canAttack = true;
		timer = cooldown;
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }
	
	// Update is called once per frame
	void Update () {

		if(_playerInput.HeroMeleeAttackInputDown > 0 && canAttack)
		{
			canAttack = false;

			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;

			int layerMask = new int();
			layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
			layerMask = ~layerMask;


			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  {
                KUSNetworkManager.HostPlayer.CmdMageMelee(GetComponent<NetworkIdentity>(), spellPosition.transform.position, Quaternion.LookRotation(ray.direction), (hit.point - transform.position).normalized * 0.5f);
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
