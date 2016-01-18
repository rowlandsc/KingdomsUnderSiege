using UnityEngine;
using System.Collections;

public class DrawShotStar : MonoBehaviour {
	public Texture2D texture;
	// Use this for initialization
	void OnGUI()
	{
		Rect rect = new Rect(Screen.width/2-20f,
		                     Screen.height/2-23f,
		                     texture.width/10, texture.height/10);
		
		GUI.DrawTexture(rect, texture);
	}
}
