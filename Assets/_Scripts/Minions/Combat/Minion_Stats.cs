using UnityEngine;
using UnityEngine.Networking;

/**
 * A class to hold the stats of a Minion.
 */
public class Minion_Stats : NetworkBehaviour {

    [SyncVar]
    public float Health = 20f;

    [SyncVar]
    public float AttackSpeed = 2f;

    [SyncVar]
    public float AttackPower = 5f;

    [SyncVar]
    public float AttackRange = 3f;
}
