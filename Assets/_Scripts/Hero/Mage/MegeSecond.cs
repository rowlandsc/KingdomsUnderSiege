using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MegeSecond : MonoBehaviour {

	public GameObject icebullet;
	public float cooldown = 1.0f;

	private bool canAttack;
	private float radius = 0.5f;
	private float cooldown_timer;
	
	// Use this for initialization
	void Start () {
		canAttack = true;
		cooldown_timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(1)&&canAttack)
		{
			canAttack = false;
		
			GameObject icebullet_clone = Instantiate(icebullet, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+radius), transform.rotation) as GameObject;
			GameObject icebullet_clone2 = Instantiate(icebullet, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+radius), transform.rotation) as GameObject;
			GameObject icebullet_clone3 = Instantiate(icebullet, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+radius), transform.rotation) as GameObject;

			//icebullet_clone.transform.parent = this.gameObject.transform;

			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))  {  
				icebullet_clone.transform.DOMove(hit.point,0.1f,false);
			} 

			Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2-30f,Screen.height/2,0));
			RaycastHit hit2;
			if (Physics.Raycast(ray2, out hit2))  {  
				icebullet_clone2.transform.DOMove(hit2.point,0.1f,false);
			} 

			Ray ray3 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2+30f,Screen.height/2,0));
			RaycastHit hit3;
			if (Physics.Raycast(ray3, out hit3))  {  
				icebullet_clone3.transform.DOMove(hit3.point,0.1f,false);
			} 

		}

		if(!canAttack){
			cooldown_timer -= Time.deltaTime;
			if(cooldown_timer<0){
				canAttack = true;	
				cooldown_timer = cooldown;
			}
		}





	}
}
