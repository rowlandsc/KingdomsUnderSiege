using UnityEngine;
using System.Collections;

public class ArchMeleeHit : MonoBehaviour {
	
	public GameObject ending;
	private GameObject ending_;

	private GameObject player;

	private float kill_time;
	private float memory_saving_timer;
	
	// Use this for initialization
	void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
		player=GameObject.FindGameObjectWithTag("Player");
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

			if(col.gameObject.GetComponent<ProfileSystem>()){
			if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().MeleeDamageDealt))
			{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}
		
			}

			Destroy(this.gameObject);
		}
	}

}
