using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MageSuper : MonoBehaviour {


	public GameObject[] hits = new GameObject[9];

	public float cooldown=30f;
	public float timer;

	private bool canAttack;
	private float spelltime=2f;
	private float real_spelltime;

	public float mp_use=40f;

	private GameObject super1_;
	private GameObject superBallanim_;

	//FX
	public GameObject superAnim1;
	public GameObject superBallanim;


	// Use this for initialization
	void Start () {

		timer=cooldown;
		real_spelltime=0f;
		canAttack=true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R)&&canAttack&&this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use)){

			canAttack=false;
			HeroMove.DisableMove();
			this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);

			hits=GameObject.FindGameObjectsWithTag("Freezed");
	
			for(int i = 0; i < hits.Length; i++)
			{
				print ("find one");
				superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,500,0), this.transform.rotation) as GameObject;
				superBallanim_.transform.DOLocalMove(hits[i].transform.position+new Vector3(0,-2f,-0),3f,false);

			}

			for(int i = 0; i < 40; i++)
			{
			superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,500,0), this.transform.rotation) as GameObject;
			superBallanim_.transform.DOLocalMove(this.gameObject.transform.position+new Vector3(Random.Range(-20F, 20F),-10f,Random.Range(-20F, 20F)),Random.Range(2F, 7F),false);
			}




			super1_ = Instantiate(superAnim1, this.transform.position-new Vector3(0,0.4f,0), this.transform.rotation) as GameObject;

		}

		if(!canAttack){
			real_spelltime+=Time.deltaTime;

			if(real_spelltime>=spelltime){
				HeroMove.EnableMove();

				Destroy(super1_);
				real_spelltime=0f;
			}

			timer-=Time.deltaTime;
			if(timer<=0){
				canAttack=true;
				timer=cooldown;
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
