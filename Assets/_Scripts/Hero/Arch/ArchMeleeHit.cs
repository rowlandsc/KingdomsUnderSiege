using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ArchMeleeHit : MonoBehaviour {
	
	public GameObject ending;
	private GameObject ending_;

	public NetworkIdentity Archer;
    private ProfileSystem archerStats;

    private float kill_time;
	private float memory_saving_timer;
	
	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
	}

    public void Initialize(NetworkIdentity archer) {
        Archer = archer;
        archerStats = archer.GetComponent<ProfileSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;
		
		if(memory_saving_timer>=1f){
			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
			ending.AddComponent<DestoryselfAfterfewsecond>();
			Destroy(this.gameObject);
			
		}
	}
	
	void OnTriggerEnter(Collider col){

		if(col.gameObject.tag!="Player"){
			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
			ending.AddComponent<DestoryselfAfterfewsecond>();

            /*if(col.gameObject.GetComponent<ProfileSystem>()){
			if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().MeleeDamageDealt))
			{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}
		
			}*/

            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Archer.netId, healthPointsAdd: -1 * archerStats.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }

			Destroy(this.gameObject);
		}
	}

}
