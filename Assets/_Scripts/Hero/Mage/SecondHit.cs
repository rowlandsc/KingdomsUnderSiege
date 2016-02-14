using UnityEngine;
using System.Collections;

public class SecondHit : MonoBehaviour {

	public GameObject ending;
	private GameObject ending_;

	private float kill_time;
	static public float FreezeTime;

	private GameObject hit = new GameObject();
	private float memory_saving_timer;
	public float freeze_timer;

	// Use this for initialization
	void Start () {
		kill_time=2f;
		FreezeTime=5f;
		memory_saving_timer=0f;
		freeze_timer=0f;
	}
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);
		
		}
	}
	
	void OnTriggerEnter(Collider col){
		ending_ = Instantiate(ending, this.transform.position, Quaternion.identity) as GameObject;
		ending.AddComponent<DestoryselfAfterfewsecond>();
		FreezeEffect(col.gameObject);

		if(col.gameObject.tag!="Player"){
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
