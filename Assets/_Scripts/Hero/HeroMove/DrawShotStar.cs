using UnityEngine;
using System.Collections;

public class DrawShotStar : MonoBehaviour {
	public Texture2D texture;
	public Texture2D texture2;
	// Use this for initialization

	public bool canArrack=false;
	public bool showFS = true;

	void OnGUI()
	{
		if(showFS){
		Rect rect = new Rect(Screen.width/2-20f,
		                     Screen.height/2-23f,
		                     texture.width/10, texture.height/10);
		
		if(!canArrack){GUI.DrawTexture(rect, texture);}
			if(canArrack){GUI.DrawTexture(rect, texture2);}}
	}

	public void disableFS(){
		showFS=false;
	}

	public void enableFS(){
		showFS=true;
	}
}
