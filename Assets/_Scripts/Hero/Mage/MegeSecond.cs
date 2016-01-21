using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MegeSecond : MonoBehaviour {

	public GameObject charingAnim;
	public GameObject icebullet;
	public float holdtime=9.0f;
	public float cooldown = 9.0f;
	
	private bool canAttack;
	private bool Startcooldown;
	private float holdingtime = 0;
	private float cooldown_timer;
	private int bullet_shot = 0;
	

	private GameObject icebullet_clone1,icebullet_clone2,icebullet_clone3,icebullet_clone4,icebullet_clone5,icebullet_clone6,icebullet_clone7;
	
	// Use this for initialization
	void Start () {
		canAttack = true;
		Startcooldown = false;
		cooldown_timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(1)&&canAttack)
		{
			GameObject chargingAnim_ = Instantiate(charingAnim, this.gameObject.transform.position, transform.rotation) as GameObject;
			chargingAnim_.transform.parent = this.gameObject.transform;
		}


		if(Input.GetMouseButton(1)&&canAttack)
		{

			holdingtime += Time.deltaTime;

			//charing make the bullets different
			if(holdingtime>=0){
				// (shot 0 bullets)
				bullet_shot =0;
				if(holdingtime>=holdtime*0.3f)
				{
					bullet_shot =3;

					if(holdingtime>=holdtime*0.6f)
					{
						// (shot 5 bullets)
						bullet_shot =5;

						if(holdingtime>=holdtime)
						{
							// (shot 7 bullets)
							bullet_shot =7;
						}
					}
				}
			}

		}



		if(Input.GetMouseButtonUp(1)&&canAttack){

			GameObject anim=GameObject.FindGameObjectWithTag("Anim");
			Destroy(anim);

			holdingtime=0;

			if(bullet_shot>=3){

				Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+0f,Screen.height/2,0));
				Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+-40f,Screen.height/2,0));
				Ray ray3 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+40f,Screen.height/2,0));
				RaycastHit hit1,hit2,hit3;

				if (Physics.Raycast(ray1, out hit1))  {  
					if(hit1.transform.gameObject.tag!="Player"){
						icebullet_clone1 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray1.direction)) as GameObject;
						icebullet_clone1.transform.DOMove(hit1.point,0.4f,false);}
				} 
				if (Physics.Raycast(ray2, out hit2))  {  
					if(hit2.transform.gameObject.tag!="Player"){
						icebullet_clone2 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray2.direction)) as GameObject;
						icebullet_clone2.transform.DOMove(hit2.point,0.3f,false);}
				} 
				if (Physics.Raycast(ray3, out hit3))  {  
					if(hit3.transform.gameObject.tag!="Player"){
						icebullet_clone3 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray3.direction)) as GameObject;
						icebullet_clone3.transform.DOMove(hit3.point,0.4f,false);}
				} 

				if(bullet_shot>=5){

					Ray ray4 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2-60f,Screen.height/2,0));
					Ray ray5 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+60f,Screen.height/2,0));
					RaycastHit hit4,hit5;

					if (Physics.Raycast(ray4, out hit4))  {  
						if(hit4.transform.gameObject.tag!="Player"){
							icebullet_clone4 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray4.direction)) as GameObject;
							icebullet_clone4.transform.DOMove(hit4.point,0.5f,false);}
					} 
					if (Physics.Raycast(ray5, out hit5))  {  
						if(hit5.transform.gameObject.tag!="Player"){
							icebullet_clone5 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray5.direction)) as GameObject;
							icebullet_clone5.transform.DOMove(hit5.point,0.5f,false);}
					} 

					if(bullet_shot>=7){

						Ray ray6 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2-80f,Screen.height/2,0));
						Ray ray7 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+80f,Screen.height/2,0));
						RaycastHit hit6,hit7;

						if (Physics.Raycast(ray6, out hit6))  {  
							if(hit6.transform.gameObject.tag!="Player"){
								icebullet_clone6 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray6.direction)) as GameObject;
								icebullet_clone6.transform.DOMove(hit6.point,0.6f,false);}
						} 
						if (Physics.Raycast(ray7, out hit7))  {  
							if(hit7.transform.gameObject.tag!="Player"){
								icebullet_clone7 = Instantiate(icebullet, this.transform.position, Quaternion.LookRotation(ray7.direction)) as GameObject;
								icebullet_clone7.transform.DOMove(hit7.point,0.6f,false);}
						} 
					}
				}
				Startcooldown=true;
			}
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
