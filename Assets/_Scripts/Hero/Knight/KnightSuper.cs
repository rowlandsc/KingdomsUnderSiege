﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class KnightSuper : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	public float mp_use=40f;

	public float cooldown=30f;
	public float duration;
	
	public bool canAttack;
	public float timer;
	public bool super_activation;
	private float duration_timer;

	private Vector3 normal_hero_size;
	private Vector3 super_hero_size;

    private ProfileSystem _profile;
    private NetworkPlayerInput _playerInput;
    private NetworkIdentity _netId;

    //FX
    public GameObject super;
	private bool super_clone_runonce;

	// Use this for initialization
	void Start () {


		canAttack=true;
		timer=cooldown;
		duration_timer=0f;
		super_activation=false;
		normal_hero_size=this.gameObject.transform.localScale;
		super_hero_size= normal_hero_size+new Vector3(12f, 12f, 12f);
		super_clone_runonce=true;
        _profile = GetComponent<ProfileSystem>();
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
        _netId = GetComponent<NetworkIdentity>();
        duration = _profile.SuperDamageDealt;
    }
	
	// Update is called once per frame
	void Update () {


		if(_playerInput.HeroMeleeSuperInputDown > 0 && canAttack && !super_activation && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use)){
			super_activation=true;
			_profile.useMagic(mp_use);
		}

		if(super_activation){
			//apply hero_increased_effect here
			if(super_clone_runonce){

                //ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
                //ending_.transform.parent=this.gameObject.transform;
                KUSNetworkManager.HostPlayer.CmdKnightSuper(_netId, transform.position, Quaternion.identity);

				this.gameObject.transform.localScale =super_hero_size;
                /*this.gameObject.GetComponent<ProfileSystem>().AddMeleeDamage(20);
				this.gameObject.GetComponent<ProfileSystem>().AddSecondDamage(30);
				this.gameObject.GetComponent<ProfileSystem>().AddSuperDamage(0);
				this.gameObject.GetComponent<ProfileSystem>().AddArmor(100);*/

                ProfileEffect superEffect = new ProfileEffect(_netId.netId, 
                    startingDuration: duration,
                    healthRegenAdd: 2,
                    meleeDamageMult: 1.5f);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(_netId, superEffect);

				super_clone_runonce=false;
			}

			duration_timer += Time.deltaTime;
            duration = _profile.SuperDamageDealt;

            if (duration_timer>=duration){
				duration_timer=0;
				super_activation=false;
				canAttack=false;

				this.gameObject.transform.localScale =normal_hero_size;
				/*this.gameObject.GetComponent<ProfileSystem>().AddMeleeDamage(-20);
				this.gameObject.GetComponent<ProfileSystem>().AddSecondDamage(-30);
				this.gameObject.GetComponent<ProfileSystem>().AddSuperDamage(0);
				this.gameObject.GetComponent<ProfileSystem>().AddArmor(-100);*/

				super_clone_runonce=true;
                KUSNetworkManager.HostPlayer.CmdKnightSuperDestroy();

			}
		}



		if(!canAttack){
			timer-=Time.deltaTime;
			if(timer<=0){
				canAttack=true;
				timer=cooldown;
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
