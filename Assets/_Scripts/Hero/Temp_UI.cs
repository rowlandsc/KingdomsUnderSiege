using UnityEngine;
using System.Collections;

public class Temp_UI : MonoBehaviour {

	private float hp;
	private float mp;
	private float money;


	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		hp = this.gameObject.GetComponent<ProfileSystem>().healthPoints;
		mp = this.gameObject.GetComponent<ProfileSystem>().MagicPoints;
		money = this.gameObject.GetComponent<ProfileSystem>().haveMoney;
	
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), "hp " + hp);
		GUI.Label(new Rect(10, 30, 100, 20), "mp " + mp);
		GUI.Label(new Rect(10, 50, 100, 20), "money " + money);
	}
}
