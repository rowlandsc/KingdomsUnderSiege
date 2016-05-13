using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;

public class MortarTowerHit : MonoBehaviour {

	public GameObject Tower ;
    public float SplashDamageRadius = 4f;
    public LayerMask LayersEffectedBySplash;

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

		if(canShot){

			hitPositionX = hitPosition.x;
			hitPositionY = hitPosition.y;
			hitPositionZ = hitPosition.z;

			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(this.transform.DOMove(new Vector3((localpositionX+hitPositionX)/2,70f,(localpositionZ+hitPositionZ)/2 ),3f,false));
			mySequence.Append(this.transform.DOMove(new Vector3(hitPositionX,hitPositionY-1f,hitPositionZ),2f,false));


			canShot = false;
		}
        

       
       
	}

	void OnTriggerEnter(Collider col){
        Debug.Log("Collided with " + col.gameObject.name);
        Collider[] splashObjects = Physics.OverlapSphere(transform.position, this.SplashDamageRadius, this.LayersEffectedBySplash);

        for(int i = 0; i < splashObjects.Length; i++)
        {
            towerStats = Tower.GetComponent<ProfileSystem>();
            ProfileSystem colProfile = splashObjects[i].gameObject.GetComponent<ProfileSystem>();
            if (colProfile)
            {
                ProfileEffect hitEffect = new ProfileEffect(Tower.GetComponent<NetworkIdentity>().netId, healthPointsAdd: -1 * towerStats.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(splashObjects[i].GetComponent<NetworkIdentity>(), hitEffect);
            }
        }

		Destroy(this.gameObject);
	}
}
