using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MageMelee : MonoBehaviour {

	public GameObject iceball;
	public float cooldown=0.5f;
	private float distance=20f;

	private bool canAttack;
	public float timer;

	private GameObject spellPosition;
	private GameObject iceball_clone;
	
	// Use this for initialization
	void Start () {

		spellPosition = GameObject.Find("SpellPosition");
		canAttack = true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0)&&canAttack)
		{
			canAttack = false;

			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;

			int layerMask = new int();
			layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
			layerMask = ~layerMask;


			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  {  
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.point); 
				  
				if(real_distance<=distance){
					iceball_clone = Instantiate(iceball, spellPosition.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
				    	iceball_clone.transform.DOMove(hit.point,real_distance/40f,false);
				   }
				else{
					canAttack=true;
				}

			} 
		}

		if(!canAttack){
			timer -= Time.deltaTime;
			if(timer<=0){
				canAttack = true;	
				timer = cooldown;
			}
		}





	}
}
