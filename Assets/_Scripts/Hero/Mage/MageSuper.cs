using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MageSuper : MonoBehaviour {

	public GameObject superAnim1;
	public GameObject superAnim2;
	public GameObject superBallanim;

	public GameObject[] hits = new GameObject[9];

	private bool Startcooldown = false;
	public float spelltime=3f;
	private float real_spelltime=0f;

	private GameObject super1_;
	private GameObject super2_;
	private GameObject superBallanim_;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R)&&!Startcooldown){
			Startcooldown=true;
			HeroMove.DisableMove();

			hits=GameObject.FindGameObjectsWithTag("Freezed");
	
			for(int i = 0; i < hits.Length; i++)
			{
				print ("find one");
				superBallanim_ = Instantiate(superBallanim, this.transform.position+new Vector3(0,40,0), this.transform.rotation) as GameObject;
				superBallanim_.transform.DOLocalMove(hits[i].transform.position+new Vector3(0,-2f,-0),4f,false);

			}

			super1_ = Instantiate(superAnim1, this.transform.position-new Vector3(0,0.4f,0), this.transform.rotation) as GameObject;
			super2_ = Instantiate(superAnim2, this.transform.position-new Vector3(0,0.4f,0), this.transform.rotation) as GameObject;
		}

		if(Startcooldown){
			real_spelltime+=Time.deltaTime;

			if(real_spelltime>=spelltime){
				HeroMove.EnableMove();

				Destroy(super1_);
				Destroy(super2_);
				real_spelltime=0f;
				Startcooldown=false;
			}
		}

	}
}
