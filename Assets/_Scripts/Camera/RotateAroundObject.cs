using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.LookAt(target);
		transform.Translate(Vector3.right * Time.deltaTime);
	
	}


}
