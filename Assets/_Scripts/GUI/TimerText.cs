using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerText : MonoBehaviour {

    private Text _timeLeft;
    private float _time = 0f;

	// Use this for initialization
	void Start () {
        this._timeLeft = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        this._timeLeft.text = RoundManager.Instance.CountDownTime.ToString();
	}
}
