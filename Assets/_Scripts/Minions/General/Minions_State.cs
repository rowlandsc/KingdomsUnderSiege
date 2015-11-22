using UnityEngine;
using System.Collections;

public class Minions_State : MonoBehaviour {

    public MINION_STATE State;

	// Use this for initialization
	void Start () {
        this.State = MINION_STATE.WALKING;
	}
}

public enum MINION_STATE{
    WALKING,
    ATTACKING
}
