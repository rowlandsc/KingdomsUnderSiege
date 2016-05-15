using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Networking;

public class KnightSecond : MonoBehaviour {

	public float effect_time;
	public float cooldown=10f;
	public float mp_use=10f;

	private bool canAttack;
	private bool Startcooldown;
	
	public float timer;
	private float timer_for_max_effect;
	public bool knightsecond_Activate;
	private float movespeed;

	private GameObject Maincamera;

    private ProfileSystem _profile;
    private NetworkPlayerInput _playerInput;
    private NetworkIdentity _netid;


    //FX
    public GameObject effect_fire;
	private GameObject effect_fire_clone;
	private bool effect_fire_clone_runonce;

	// Use this for initialization
	void Start () {
		
		canAttack = true;
		Startcooldown = false;
		timer = cooldown;
		timer_for_max_effect=0f;
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
		movespeed = gameObject.GetComponent<ProfileSystem>().MoveSpeed;
		knightsecond_Activate=false;
		effect_fire_clone_runonce=true;
        _profile = GetComponent<ProfileSystem>();
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
        _netid = GetComponent<NetworkIdentity>();
    }
	
	// Update is called once per frame
	void Update () {
	
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");

		if(_playerInput.HeroMeleeChargeAttackInput > 0 && canAttack && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use))
		{
			knightsecond_Activate=true;

			timer_for_max_effect += Time.deltaTime;
            effect_time = _profile.SecondDamageDealt;

            if (timer_for_max_effect>=effect_time){
				knightsecond_Activate=false;
				timer_for_max_effect=0;
				canAttack=false;
			}
		}

		if(Input.GetMouseButtonUp(1)&&knightsecond_Activate)
		{
			this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);
			knightsecond_Activate=false;
			canAttack=false;
		}


		if(knightsecond_Activate){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);
			if(Input.GetKey(KeyCode.W)){transform.Translate(moveDirection * Time.deltaTime* movespeed*3f, Space.World);}
			else if(!Input.GetKey(KeyCode.W)){transform.Translate(moveDirection * Time.deltaTime* movespeed*3f, Space.World);}

			if(effect_fire_clone_runonce){
                KUSNetworkManager.HostPlayer.CmdKnightSecond(_netid, transform.position, transform.rotation);
                effect_fire_clone_runonce=false;
			}
		}

		if(!knightsecond_Activate){
            KUSNetworkManager.HostPlayer.CmdKnightSecondDestroy();
			effect_fire_clone_runonce=true;
		}

		if(!canAttack){
			timer -= Time.deltaTime;
			if(timer<0){
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

