﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class MagicTowerHit : MonoBehaviour, IShootable {

	public GameObject Tower ;
	private ProfileSystem towerStats;

	private float kill_time;
	private float memory_saving_timer;

	public Vector3 velocity;

    public string PrefabCacheId
    {
        get
        {
            return "MagicTowerShot";
        }
    }

    // Use this for initialization
    void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
	}

	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);
		}

		transform.position += velocity;
	}

	void OnTriggerEnter(Collider col){

		if(col.gameObject.tag!="OverseerPlayer"){
			/*if(col.gameObject.GetComponent<ProfileSystem>()) {
                if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(tower.GetComponent<ProfileSystem>().MeleeDamageDealt))
				{
                    tower.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;
                }
			}*/

			towerStats = Tower.GetComponent<ProfileSystem>();
			ProfileSystem colProfile = col.transform.root.GetComponent<ProfileSystem>();
			if (colProfile) {
				ProfileEffect slowdown = new ProfileEffect(Tower.GetComponent<NetworkIdentity>().netId,  startingDuration: 3, moveSpeedAdd: -1 * 0.5f);
				ProfileEffect hitEffect = new ProfileEffect(Tower.GetComponent<NetworkIdentity>().netId, healthPointsAdd: -1 * towerStats.SecondDamageDealt);
				KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
				KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), slowdown);
			}
		}

		Destroy(this.gameObject);
	}
}
