using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class KnightMeleeHit : MonoBehaviour {


    public NetworkIdentity Knight;
    private ProfileSystem knightStats;

    // Use this for initialization
    void Start () {
		
	}

    public void Initialize(NetworkIdentity knight) {
        Knight = knight;
        knightStats = knight.GetComponent<ProfileSystem>();
    }

    // Update is called once per frame
    void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		
		bool canAttack=!gameObject.GetComponentInParent<KnightMelee>().canAttack;

		if(canAttack){
			if(col.tag=="OverseerPlayer"){

                /*if(col.gameObject.GetComponent<ProfileSystem>()){

					if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().MeleeDamageDealt))
					{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}}
                    */

                ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
                if (colProfile) {
                    ProfileEffect hitEffect = new ProfileEffect(Knight.netId, healthPointsAdd: -1 * knightStats.MeleeDamageDealt);
                    KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
                }

            }

           
		}

	}
}
