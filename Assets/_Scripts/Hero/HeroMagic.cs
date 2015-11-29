using UnityEngine;
using System.Collections;

public class HeroMagic : MonoBehaviour {

	public GameObject smoke;
	public GameObject spark;
	public GameObject Magic;
	public float magicuse = 10;

	public GameObject MagicPrefeb;
	public GameObject hero;
	static public float magicSpeed = 25;
	static public float magicDistance = 15;
	static public float magicDamage = 40;

	

	public float magicCoolDown = 5.0f;
	private bool magicshot = false;


	// Use this for initialization
	void Start () {
		magicReady();
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButton(1)){

			smoke.GetComponent<Renderer>().enabled = true;

			magicCoolDown -= Time.deltaTime;

			if(magicCoolDown< (magicCoolDown/2)){
				spark.GetComponent<Renderer>().enabled = true;
			}

			if(magicCoolDown < 0)
			{
				Magic.GetComponent<Renderer>().enabled = true;
				magicshot = true;
			}
		}
		else{
			if(magicshot==false){
				magicReady();}
		}


		if(Input.GetMouseButtonUp(1)){
			if(magicCoolDown < 0){
				print("nice");
				HeroProfile.mp-=magicuse;
				magicReady();
				shot ();
			}
		}
	}

	public void magicReady(){
		smoke.GetComponent<Renderer>().enabled = false;
		spark.GetComponent<Renderer>().enabled = false;
		Magic.GetComponent<Renderer>().enabled = false;
		magicCoolDown = 3f;
		magicshot = false;
	}

	public void shot(){

		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))  
		{  
			print (hit.transform.gameObject.name);

		}  

		GameObject newBall = Instantiate(MagicPrefeb, new Vector3(hero.transform.position.x+0.68f,hero.transform.position.y+1.67f,hero.transform.position.z), transform.rotation) as GameObject;
		newBall.GetComponent<Rigidbody>().velocity = (hit.point - new Vector3(hero.transform.position.x+0.68f,hero.transform.position.y+1.67f,hero.transform.position.z)).normalized * magicSpeed;


	}



}
