using UnityEngine;
using System.Collections;

public class Minions_Navigation : MonoBehaviour {

    public Transform MoveTarget;

    private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshAgent.SetDestination(this.MoveTarget.position);
	}
	
	// Update is called once per frame
	void Update () {
      
	}
}
