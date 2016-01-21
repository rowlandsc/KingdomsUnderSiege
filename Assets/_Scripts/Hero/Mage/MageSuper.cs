using UnityEngine;
using System.Collections;

public class MageSuper : MonoBehaviour {

	public GameObject superAnim1;
	public GameObject superAnim2;

	private bool Startcooldown = false;
	public float spelltime=3f;
	private float real_spelltime=0f;

	private GameObject super1 = new GameObject();
	private GameObject super2 = new GameObject();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R)&&!Startcooldown){
			Startcooldown=true;
			HeroMove.DisableMove();
			super1 = Instantiate(superAnim1, this.transform.position, this.transform.rotation) as GameObject;
			super2 = Instantiate(superAnim2, this.transform.position, this.transform.rotation) as GameObject;
		}

		if(Startcooldown){
			real_spelltime+=Time.deltaTime;

			if(real_spelltime>=spelltime){
				HeroMove.EnableMove();

				Destroy(super1);
				Destroy(super2);
				real_spelltime=0f;
				Startcooldown=false;
			}
		}

	}
}
