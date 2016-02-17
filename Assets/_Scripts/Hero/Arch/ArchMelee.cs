using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchMelee : MonoBehaviour {

	public GameObject arch;
	public float cooldown=0.5f;
	public float distance=30f;
	
	private bool canAttack;
	public float timer;
	
	private GameObject arch_clone;
	private GameObject spellPosition;

	// Use this for initialization
	void Start () {
		
		spellPosition = GameObject.Find("SpellPosition_arch");
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
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.point); 
			
				if(real_distance<=distance){
					arch_clone = Instantiate(arch, spellPosition.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
						arch_clone.transform.DOMove(hit.point,0.3f,false);
					}
				else{
					canAttack = true;
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
