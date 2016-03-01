using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
/**
 * A class to handle the Minion navigation.
 * Not quited fully finished, but a good basic start.
 */
public class Minions_Navigation : MonoBehaviour {

    /**
     * Public Variables
     * MoveTarget - The end goal position of the Minion
     */
    public string MoveTarget;

    /**
     * Private Variables
     * _navMeshAgent - The NavMesh Agent Attached to the Minion
     */
    private NavMeshAgent _navMeshAgent;
    private Transform _endTarget;

	/**
     * Called when a Minion is created.
     * Initializes _navMeshAgent and sets it's desired destination.
     */
	void Start (){
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._endTarget = GameObject.Find(MoveTarget).transform;
        this._navMeshAgent.SetDestination(this._endTarget.position);
	}

    public Transform getTarget(){
        return this._endTarget;
    }

    void Update(){
        if (RoundManager.Instance)
        {
            if (RoundManager.Instance.IsPreround())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
