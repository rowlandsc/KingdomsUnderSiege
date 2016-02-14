using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchMelee : MonoBehaviour {

	public GameObject arch;
	private float cooldown;
	private float distance;
	
	private bool canAttack;
	public float timer;
	
	private GameObject arch_clone;
	
	// Use this for initialization
	void Start () {
		cooldown = 0.5f;
		distance=30f;
		
		canAttack = true;
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0)&&canAttack&&!this.gameObject.GetComponent<ArchSuper>().SuperActivate)
		{
			canAttack = false;
			
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;

			int layerMask = new int();
			layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
			layerMask = ~layerMask;

			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  {  
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.transform.position); 

					
					if(hit.transform.gameObject.tag!="Player"){
					arch_clone = Instantiate(arch, this.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
						arch_clone.transform.DOMove(hit.point,1f,false);
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
