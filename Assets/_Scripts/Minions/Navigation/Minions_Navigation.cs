using UnityEngine;
using System.Collections;

/**
 * A class to handle the Minion navigation.
 * Not quited fully finished, but a good basic start.
 */
public class Minions_Navigation : MonoBehaviour {

    /**
     * Public Variables
     * MoveTarget - The end goal position of the Minion
     */
    public Transform MoveTarget;

    /**
     * Private Variables
     * _navMeshAgent - The NavMesh Agent Attached to the Minion
     */
    private NavMeshAgent _navMeshAgent;

	/**
     * Called when a Minion is created.
     * Initializes _navMeshAgent and sets it's desired destination.
     */
	void Start () {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshAgent.SetDestination(this.MoveTarget.position);
	}
}
