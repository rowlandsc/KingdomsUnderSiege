using UnityEngine;
using System.Collections;

/**
 * This Script is for Testing Purposes.
 * It shall be used to test navigation with towers on the map.
 * This is not intended to be a final product script.
 */
public class TEST_HEALTH : MonoBehaviour {
    public float Health = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Health < float.Epsilon){
            DestroySelf();
        }
	
	}

    void DestroySelf(){
        Destroy(this.gameObject);
    }
}
