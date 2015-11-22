using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance = null;
    public float ScrollDistance = 0.1f;

    public static float OverseerZoom {
        get { return Input.GetAxisRaw("OverseerZoom"); }
    }
    public static float OverseerScrollHorizontal {
        get {
            float input = Input.GetAxisRaw("OverseerScrollHorizontal");
            return input;

        }
    }
    public static float OverseerScrollVertical {
        get {
            return Input.GetAxisRaw("OverseerScrollVertical");
        }
    }

    void Awake() {
        if (!Instance) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }
	
}
