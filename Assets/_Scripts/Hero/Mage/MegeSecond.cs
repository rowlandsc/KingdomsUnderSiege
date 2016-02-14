using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MegeSecond : MonoBehaviour {


	private float holdtime;
	public float cooldown;

	private bool canAttack;
	private bool Startcooldown;
	private float holdingtime;
	public float cooldown_timer;
	public int bullet_shot;

	private GameObject icebullet_clone1,icebullet_clone2,icebullet_clone3,icebullet_clone4,icebullet_clone5;

	//FX
	public GameObject charingAnim;
	public GameObject charingAnim2;
	public GameObject charingAnim3;


	private GameObject chargingAnim_;
	private GameObject chargingAnim2_;
	private GameObject chargingAnim3_;


	public GameObject icebullet;

	private bool anim_2_once,anim_3_once;
	private float size_attack;
	
	// Use this for initialization
	void Start () {
		holdtime=3f;
		cooldown=6f;

		holdingtime=0;
		bullet_shot=0;
		canAttack = true;
		Startcooldown = false;
		cooldown_timer = cooldown;

		anim_2_once=true;
		anim_3_once=true;

		size_attack= Screen.width/15;
	}
	
	// Update is called once per frame
	void Update () {

	
	
		if(Input.GetMouseButtonDown(1)&&canAttack)
		{
			chargingAnim_ = Instantiate(charingAnim, this.gameObject.transform.position-new Vector3(0,0.2f,0), transform.rotation) as GameObject;
			chargingAnim_.transform.parent = this.gameObject.transform;
		}


		if(Input.GetMouseButton(1)&&canAttack)
		{

			holdingtime += Time.deltaTime;

			//charing make the bullets different
			if(holdingtime>=0){
				// (shot 1 bullets)
				bullet_shot =1;

				if(holdingtime>=holdtime*0.5f)
				{
					if(anim_2_once){
						chargingAnim2_ = Instantiate(charingAnim2, this.gameObject.transform.position-new Vector3(0,0.2f,0), transform.rotation) as GameObject;
						chargingAnim2_.transform.parent = this.gameObject.transform;
						anim_2_once=false;
					}
						
					bullet_shot =3;

					if(holdingtime>=holdtime*1.0f)
					{
						if(anim_3_once){
							chargingAnim3_ = Instantiate(charingAnim3, this.gameObject.transform.position-new Vector3(0,0.2f,0), transform.rotation) as GameObject;
							chargingAnim3_.transform.parent = this.gameObject.transform;
							anim_3_once=false;
						}

						bullet_shot =5;

					}
				}
			}

		}



		if(Input.GetMouseButtonUp(1)&&canAttack){

			anim_2_once=true;
			anim_3_once=true;

			Destroy(chargingAnim_);
			Destroy(chargingAnim2_);
			Destroy(chargingAnim3_);

			holdingtime=0;

			if(bullet_shot>=1){
				Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
				RaycastHit hit1;
				if (Physics.Raycast(ray1, out hit1))  {  
					if(hit1.transform.gameObject.tag!="Player"){
						icebullet_clone1 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray1.direction)) as GameObject;
						icebullet_clone1.transform.DOMove(hit1.point,0.4f,false);}
				} 

			if(bullet_shot>=3){

				
					Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2-size_attack,Screen.height/2,0));
					Ray ray3 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+size_attack,Screen.height/2,0));
				RaycastHit hit2,hit3;

				
				if (Physics.Raycast(ray2, out hit2))  {  
					if(hit2.transform.gameObject.tag!="Player"){
							icebullet_clone2 = Instantiate(icebullet, this.transform.position+new Vector3(0.2f,0,0), Quaternion.LookRotation(ray2.direction)) as GameObject;
						icebullet_clone2.transform.DOMove(hit2.point,0.3f,false);}
				} 
				if (Physics.Raycast(ray3, out hit3))  {  
					if(hit3.transform.gameObject.tag!="Player"){
							icebullet_clone3 = Instantiate(icebullet, this.transform.position+new Vector3(-0.2f,0,0), Quaternion.LookRotation(ray3.direction)) as GameObject;
						icebullet_clone3.transform.DOMove(hit3.point,0.4f,false);}
				} 

				if(bullet_shot>=5){


						Ray ray4 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2-2*size_attack,Screen.height/2,0));
						Ray ray5 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+2*size_attack,Screen.height/2,0));
					RaycastHit hit4,hit5;

					if (Physics.Raycast(ray4, out hit4))  {  
						if(hit4.transform.gameObject.tag!="Player"){
								icebullet_clone4 = Instantiate(icebullet, this.transform.position+new Vector3(0.4f,0,0), Quaternion.LookRotation(ray4.direction)) as GameObject;
							icebullet_clone4.transform.DOMove(hit4.point,0.5f,false);}
					} 
					if (Physics.Raycast(ray5, out hit5))  {  
						if(hit5.transform.gameObject.tag!="Player"){
								icebullet_clone5 = Instantiate(icebullet, this.transform.position+new Vector3(-0.4f,0,0), Quaternion.LookRotation(ray5.direction)) as GameObject;
							icebullet_clone5.transform.DOMove(hit5.point,0.5f,false);}
					} 
							
				}
				
			}
			}
			Startcooldown=true;
		}

		if(Startcooldown){
			
				canAttack=false;
				cooldown_timer-=Time.deltaTime;

			if(cooldown_timer<0){
				bullet_shot=0;
				canAttack = true;	
				Startcooldown= false;
				cooldown_timer = cooldown;
			}
		}
	}
}
