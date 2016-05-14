using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;
using System;

public class MortarTowerHit : MonoBehaviour, IShootable {

	public GameObject Tower ;
    public float SplashDamageRadius = 4f;
    public LayerMask LayersEffectedBySplash;

    private ProfileSystem towerStats;

	private float kill_time;
	private float memory_saving_timer;

	public bool canShot = false;
	public Vector3 hitPosition;

    private float speed;

    private float hitPositionX;
	private float hitPositionY;
	private float hitPositionZ;

    private float localpositionX;
    private float localpositionY;
    private float localpositionZ;



    public string PrefabCacheId
    {
        get
        {
            return "MortarTowerShot";
        }
    }

    // Use this for initialization
    void Start () {
		kill_time=10f;
		memory_saving_timer=0f;
		localpositionX = transform.position.x;
        localpositionY = transform.position.y;
		localpositionZ = transform.position.z;
	}

    public void Initialize(GameObject tower) {
        Tower = tower;
        towerStats = Tower.GetComponent<ProfileSystem>();
        speed = towerStats.AttackSpeed;
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

            float xdistance = hitPositionX - localpositionX;
            float zdistance = hitPositionZ - localpositionZ;
            float ydistance = Mathf.Sqrt(xdistance * xdistance + zdistance * zdistance) / 2;

            float duration = Mathf.Sqrt((xdistance * xdistance) / 4 + (ydistance * ydistance) / 4 + (zdistance * zdistance) / 4) * Time.deltaTime / speed;




			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(this.transform.DOMove(new Vector3(localpositionX + xdistance/2, localpositionY + ydistance, localpositionZ + zdistance/2 ), duration, false));
			mySequence.Append(this.transform.DOMove(new Vector3(hitPositionX,hitPositionY-1f,hitPositionZ), duration / 1.5f, false));
            

			canShot = false;
		}
        

       
       
	}

	void OnTriggerEnter(Collider col){
        if (col.gameObject == Tower) return;
        
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
