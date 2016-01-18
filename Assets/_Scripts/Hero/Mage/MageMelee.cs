using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MageMelee : MonoBehaviour {

	public GameObject iceball;
	public float cooldown = 0.5f;

	private bool canAttack;
	private float radius = 0.5f;
	private float timer;
	
	// Use this for initialization
	void Start () {
		canAttack = true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;
		
			GameObject iceball_clone = Instantiate(iceball, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+radius), transform.rotation) as GameObject;
	
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))  {  
				iceball_clone.transform.DOMove(hit.point,0.3f,false);
			} 
		}

		if(!canAttack){
			timer -= Time.deltaTime;
			if(timer<0){
				canAttack = true;	
				timer = cooldown;
			}
		}





	}
}
