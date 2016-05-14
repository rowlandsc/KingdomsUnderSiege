using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Networking;

public class ArchSuper : MonoBehaviour {
	
	private float cooldown=25f;
	private bool canAttack;
	private float skytime=10f;
	public bool SuperActivate;
	private int shot_times_left;

	public float mp_use=40f;

	public float timer;
	
	public GameObject superbullet;
	private GameObject superbullet_clone;

	public GameObject superFx;
	private GameObject superFx_clone;
	private bool effect_apply;

	private GameObject spellPosition;

    private NetworkPlayerInput _playerInput;
    private NetworkIdentity _netid;

	// Use this for initialization
	void Start () {
		spellPosition = GameObject.Find("SpellPosition_arch");
		shot_times_left=3;
		canAttack=true;
		SuperActivate=false;
		effect_apply=true;
		timer = cooldown;
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
        _netid = GetComponent<NetworkIdentity>();
    }
	
	// Update is called once per frame
	void Update () {
	
		if(_playerInput.HeroMeleeSuperInputDown > 0 && canAttack && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use)){
		
			canAttack = false;
			SuperActivate=true;
			this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);

			this.gameObject.transform.DOMove(this.gameObject.transform.position+new Vector3(0f,2.5f,0f),0.5f,false);
	
		}

		if(SuperActivate){
			HeroMove.DisableMove();

			if(effect_apply){
                //superFx_clone = Instantiate(superFx, this.gameObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject;
                //superFx_clone.transform.parent=this.gameObject.transform;
                KUSNetworkManager.HostPlayer.CmdArcherSuperStart(_netid, transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));

				effect_apply=false;
			}

			this.gameObject.GetComponent<Rigidbody>().useGravity=false;

			if(Input.GetMouseButtonDown(0)&&shot_times_left>0){
				shot_times_left-=1;

				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit))  {  

					if(hit.transform.gameObject.tag!="Player"){
						superbullet_clone = Instantiate(superbullet, spellPosition.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
                        superbullet_clone.GetComponent<ArchSuperHit>().Initialize(GetComponent<NetworkIdentity>());
						superbullet_clone.GetComponent<ArchSuperHit>().velocity = (hit.point - transform.position).normalized*0.9f;
					}
				} 
			}

		
			skytime-=Time.deltaTime;
			if(skytime<=0||shot_times_left<=0){
				this.gameObject.GetComponent<Rigidbody>().useGravity=true;
				SuperActivate=false;
                KUSNetworkManager.HostPlayer.CmdArcherSuperDestroy();
                effect_apply =true;
				skytime=10f;
				shot_times_left=3;
			}

		}


		if(!canAttack&&!SuperActivate){
			HeroMove.EnableMove();
			timer -= Time.deltaTime;
			if(timer<=0){
				canAttack = true;	
				timer = cooldown;
			}
		}
	}

	public float gettimer(){
		return timer;
	}

	public float getcooldown(){
		return cooldown;
	}
}
