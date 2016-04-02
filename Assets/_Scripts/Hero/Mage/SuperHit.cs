using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SuperHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

    public NetworkIdentity Mage;
    private ProfileSystem mageStats;

    private float kill_time;
	private float memory_saving_timer;
	
	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
	}

    public void Initialize(NetworkIdentity mage) {
        Mage = mage;
        mageStats = mage.GetComponent<ProfileSystem>();
    }

    // Update is called once per frame
    void Update () {
		memory_saving_timer+=Time.deltaTime;
		
		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);

		}
	}
	
	
	void OnTriggerEnter(Collider col){


		if(col.tag!="IceBullet"&&col.tag!="Player"&&col.tag!="HealthBar"){
			
		ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
		ending.AddComponent<DestoryselfAfterfewsecond>();

		if(col.GetComponent<Testmove>())
		{col.GetComponent<Testmove>().canMove=true;}

            /*if(col.gameObject.GetComponent<ProfileSystem>()){

                if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().SuperDamageDealt))
                {player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

            }*/
            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Mage.netId, healthPointsAdd: -1 * mageStats.SuperDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }

            Destroy(this.gameObject);
		}
		
	}
	
	
}
