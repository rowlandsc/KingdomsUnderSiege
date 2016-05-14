using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SecondHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	private float kill_time;
	static public float FreezeTime;

    public NetworkIdentity Mage;
    private ProfileSystem mageStats;

    private GameObject hit;
	private float memory_saving_timer;
	public float freeze_timer;

	// Use this for initialization
	void Start () {
		kill_time=2f;
		FreezeTime=5f;
		memory_saving_timer=0f;
		freeze_timer=0f;
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
		

		if(col.gameObject.tag!="HeroPlayer"&&col.gameObject.tag!="IceBullet"){
            if (!KUSNetworkManager.LocalPlayer.isServer) return;

			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(ending_);

            /*if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().SuperDamageDealt))
				{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

			}*/
            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Mage.netId, healthPointsAdd: -1 * mageStats.SecondDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }

            Destroy(this.gameObject);}
	}

	void FreezeEffect(GameObject temp){
		if(temp.GetComponent<Testmove>())
			temp.GetComponent<Testmove>().canMove=false;
	}

	void UnFreezeEffect(GameObject temp){
		if(temp.GetComponent<Testmove>())
			temp.GetComponent<Testmove>().canMove=true;
	}
}
