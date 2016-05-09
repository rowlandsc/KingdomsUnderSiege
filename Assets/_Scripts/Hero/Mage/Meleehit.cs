using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Meleehit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

    public NetworkIdentity Mage;
    private ProfileSystem mageStats;

    private float kill_time;
	private float memory_saving_timer;

	public Vector3 velocity;

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
			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
			ending.AddComponent<DestoryselfAfterfewsecond>();
			Destroy(this.gameObject);


		}

		transform.position += velocity;
	}
	
	
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag != "HeroPlayer") {
            ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
            ending.AddComponent<DestoryselfAfterfewsecond>();

            /*if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().MeleeDamageDealt))
				{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

			}}*/

            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Mage.netId, healthPointsAdd: -1 * mageStats.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }
        }
		Destroy(this.gameObject);
	}


}
