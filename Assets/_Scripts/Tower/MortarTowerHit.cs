using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;

public class MortarTowerHit : MonoBehaviour {

	public GameObject Tower ;
    private ProfileSystem towerStats;

	private float kill_time;
	private float memory_saving_timer;

	public bool canShot = false;
	public Vector3 hitPosition;

    private float hitPositionX;
	private float hitPositionY;
	private float hitPositionZ;

    private float localpositionX;
	private float localpositionZ;

    // Use this for initialization
    void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
		localpositionX = this.gameObject.transform.position.x;
		localpositionZ = this.gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		memory_saving_timer+=Time.deltaTime;

		if(memory_saving_timer>=kill_time){
			Destroy(this.gameObject);
		}

		while(canShot){

			hitPositionX = hitPosition.x;
			hitPositionY = hitPosition.y;
			hitPositionZ = hitPosition.z;

			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(this.transform.DOMove(new Vector3((localpositionX+hitPositionX)/2,20f,(localpositionZ+hitPositionZ)/2 ),2f,false));
			mySequence.Append(this.transform.DOMove(new Vector3(hitPositionX,hitPositionY-5f,hitPositionZ),2f,false));


			canShot = false;
		}
        

       
       
	}

	void OnTriggerEnter(Collider col){

		if(col.gameObject.tag!="OverseerPlayer"){
            /*if(col.gameObject.GetComponent<ProfileSystem>()) {
                if(col.gameObject.GetComponent<ProfileSystem>().KillAndGains(tower.GetComponent<ProfileSystem>().MeleeDamageDealt))
				{
                    tower.GetComponent<ProfileSystem>().haveMoney+=col.gameObject.GetComponent<ProfileSystem>().Worth;
                }
			}*/

            towerStats = Tower.GetComponent<ProfileSystem>();
            ProfileSystem colProfile = col.gameObject.GetComponent<ProfileSystem>();
            if (colProfile) {
                ProfileEffect hitEffect = new ProfileEffect(Tower.GetComponent<NetworkIdentity>().netId, healthPointsAdd: -1 * towerStats.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(col.GetComponent<NetworkIdentity>(), hitEffect);
            }
        }

		Destroy(this.gameObject);
	}
}
