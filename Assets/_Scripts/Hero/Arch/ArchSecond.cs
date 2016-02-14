using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArchSecond : MonoBehaviour {

	public GameObject electric;
	public GameObject electric_linked;

	private float cooldown;
	private float distance;
	private float linked_distance;
	
	private bool canAttack;
	public float timer;

	private GameObject electric_clone;
	private GameObject electric_linked_clone1;
	private GameObject electric_linked_clone2;
	private GameObject electric_linked_clone3;


	private GameObject closest_enemy_one;
	private GameObject closest_enemy_two;
	private GameObject closest_enemy_three;

	private bool canAttackone;
	private bool canAttacktwo;

	public bool ifhit;
	public Vector3 hitpoint;
	private Vector3[] delete = new Vector3[2];



	private GameObject Eletric_linked_one,Eletric_linked_two,Eletric_linked_three;
	
	// Use this for initialization
	void Start () {
		cooldown = 1f;
		distance=30f;
		linked_distance=20f;
		
		canAttack = true;
		timer = cooldown;
		ifhit=false;

		canAttackone=false;
		canAttacktwo=false;

		delete[0]=new Vector3(0f,0f,0f);
		delete[1]=new Vector3(0f,0f,0f);

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(1)&&canAttack)
		{
			canAttack = false;
			
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))  {  
				float real_distance=Vector3.Distance(this.gameObject.transform.position,hit.transform.position); 

					
					if(hit.transform.gameObject.tag!="Player"){
						electric_clone = Instantiate(electric, this.transform.position, Quaternion.LookRotation(ray.direction)) as GameObject;
						electric_clone.transform.DOMove(hit.point,real_distance/30f,false);
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


		if(ifhit){
			delete[0]=hitpoint;
			closest_enemy_one = FindClosestEnemy(hitpoint,delete);
			ifhit=false;
			canAttackone=true;
			canAttacktwo=true;
		}

	
		if(closest_enemy_one.transform.position!=new Vector3(0,0,0)&&canAttackone){
			if(Vector3.Distance(closest_enemy_one.transform.position,hitpoint)<=linked_distance){
				electric_linked_clone1 = Instantiate(electric_linked, hitpoint, Quaternion.identity) as GameObject;
				electric_linked_clone1.transform.DOMove(closest_enemy_one.transform.position,0.5f,false);
			}
			canAttackone=false;
		}

		delete[1]=closest_enemy_one.transform.position;
		closest_enemy_two = FindClosestEnemy(closest_enemy_one.transform.position,delete);
		if(closest_enemy_two.transform.position!=new Vector3(0,0,0)&&canAttacktwo){
			if(Vector3.Distance(closest_enemy_two.transform.position,closest_enemy_one.transform.position)<=linked_distance){
			electric_linked_clone2 = Instantiate(electric_linked, closest_enemy_one.transform.position, Quaternion.identity) as GameObject;
			electric_linked_clone2.transform.DOMove(closest_enemy_two.transform.position,0.5f,false);
			}
			canAttacktwo=false;
		}
			
}


	public GameObject FindClosestEnemy(Vector3 temp,Vector3[] delete) {

		GameObject[] enemy;
		GameObject cloest = new GameObject();
		cloest.transform.position=new Vector3(0f,0f,0f);

		enemy = GameObject.FindGameObjectsWithTag("OverseerPlayer");
		if(enemy.Length>=1){
			if(delete.Length>=1)
			{
			
			for(int i = 0; i < (enemy.Length); i++)
			{
				for(int j=0;j< delete.Length; j++){

					if(enemy[i].transform.position==delete[j]){
							enemy[i].transform.position= new Vector3(0f,-2f,0f);
					}
				}
			}
			}
		}
	

		if(enemy.Length>=2){
			cloest=enemy[0];
			
			for(int i = 0; i < (enemy.Length-1); i++)
			{
				
				float distance1 = Vector3.Distance(temp,cloest.transform.position); 
				float distance2 = Vector3.Distance(temp,enemy[i+1].transform.position); 
				
				if(distance1>=distance2){
					cloest=enemy[i+1];
				}


			}
			
		}
		
		return cloest;
		
	}

}
