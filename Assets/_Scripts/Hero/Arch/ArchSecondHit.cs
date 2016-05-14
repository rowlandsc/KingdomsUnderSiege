using UnityEngine;
using UnityEngine.Networking;

public class ArchSecondHit : MonoBehaviour {

	public float effect_time;
	private float timer;

	public GameObject ending;
	private GameObject ending_;

    public NetworkIdentity Archer;
    private ProfileSystem archerStats;

    // Use this for initialization
    void Start () {
		effect_time=10f;
		timer=effect_time;
	}

    public void Initialize(NetworkIdentity archer) {
        Archer = archer;
        archerStats = archer.GetComponent<ProfileSystem>();
    }

    // Update is called once per frame
    void Update () {
		timer-=Time.deltaTime;
		if(timer<=0f){
            if (KUSNetworkManager.LocalPlayer.isServer) {
                ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
                ending_.AddComponent<DestoryselfAfterfewsecond>();
                NetworkServer.Spawn(ending_);
            }
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider col){
        if (!KUSNetworkManager.LocalPlayer.isServer) return;

		if(col.tag=="OverseerPlayer"){

            /*if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().SecondDamageDealt*0.02f))
				{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

			}*/

            ProfileSystem colProfile = col.transform.root.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Archer.netId, healthPointsAdd: -1 * archerStats.SecondDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }
        }
			
	}

}
