using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class KnightSecondHit : MonoBehaviour {

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

	void OnTriggerStay(Collider col){

        if (!KUSNetworkManager.LocalPlayer.isServer) return;

		if(col.tag=="OverseerPlayer"){


            /*if(col.gameObject.GetComponent<ProfileSystem>()){

            if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().SecondDamageDealt*0.05f))
                {player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}}
                */

            ProfileSystem colProfile = col.transform.root.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Knight.netId, healthPointsAdd: -1 * knightStats.SecondDamageDealt * Time.deltaTime);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }
        }


	}
}
