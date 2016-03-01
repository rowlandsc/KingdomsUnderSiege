using UnityEngine;
using UnityEngine.Networking;

public class Minions_State : NetworkBehaviour {

    [SyncVar]
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
