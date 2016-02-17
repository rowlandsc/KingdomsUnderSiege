using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour {

	private Animator animator ;
	private bool ifmoving;
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	void Update()
	{
		ifmoving = gameObject.GetComponentInParent<HeroMove>().ifMoving;
		animator.SetBool("IfMoving",ifmoving);
		
	}
}
