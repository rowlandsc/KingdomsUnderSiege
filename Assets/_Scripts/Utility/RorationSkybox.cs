using UnityEngine;
using System.Collections;

public class RorationSkybox : MonoBehaviour {

	public float rotation_speed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotation_speed);
	}
}
