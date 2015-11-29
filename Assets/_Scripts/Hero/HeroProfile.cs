using UnityEngine;
using System.Collections;

public class HeroProfile : MonoBehaviour {


	static public float hp=100;
	static public float mp=100;
	public float mp_recCoolDown=10f;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		mp_recCoolDown -= Time.deltaTime;
		if(mp_recCoolDown<0){
			if(mp<=95){
				mp+=5;
				mp_recCoolDown=10f;
			}
		}


	}

	public float getHP(){
		return hp;
	}

	public float getMP(){
		return mp;
	}


	void OnGUI(){
		GUI.Label(new Rect(10f, 10f, 100f, 50f), "HP "+ hp.ToString());
		GUI.Label(new Rect(10f, 20f, 100f, 50f), "MP "+ mp.ToString());
	}
}
