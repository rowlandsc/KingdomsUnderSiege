using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class TowerAttackHit : MonoBehaviour, IShootable {

	public GameObject Tower ;
    private ProfileSystem towerStats;

	private float kill_time;
	private float memory_saving_timer;

    public Vector3 velocity;

    public string PrefabCacheId
    {
        get
        {
            return "ArcherTowerShot";
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
            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Tower.GetComponent<NetworkIdentity>().netId, healthPointsAdd: -1 * towerStats.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }
        }

		Destroy(this.gameObject);
	}
}
