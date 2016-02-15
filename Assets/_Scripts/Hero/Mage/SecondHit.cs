using UnityEngine;
using System.Collections;

public class SecondHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	private float kill_time;
	static public float FreezeTime;

	private GameObject player;

	private GameObject hit = new GameObject();
	private float memory_saving_timer;
	public float freeze_timer;

	// Use this for initialization
	void Start () {
		kill_time=2f;
		FreezeTime=5f;
		memory_saving_timer=0f;
		freeze_timer=0f;
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);
		
		}
	}
	
	void OnTriggerEnter(Collider col){
		

		if(col.gameObject.tag!="Player"){
			ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
			ending.AddComponent<DestoryselfAfterfewsecond>();

			if(col.gameObject.GetComponent<ProfileSystem>()){

				if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(player.GetComponent<ProfileSystem>().secondDamageDealt))
				{player.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;}

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
