using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour{

    /**
     * Public Variable Description
     * HitPoints - The hitpoints of the character.
     */
    public float HitPoints = 20;

    // Update is called once per frame
    void Update(){

        // If the character has not hit points left
        if (HitPoints < float.Epsilon){

            // Destroy it
            DestroySelf();
        }
    }

    /**
     * Performs the destruction of the character
     */
    void DestroySelf(){
        Destroy(this.gameObject);
    }
}
