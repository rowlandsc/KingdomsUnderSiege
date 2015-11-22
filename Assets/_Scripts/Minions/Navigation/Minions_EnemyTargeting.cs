using UnityEngine;
using System.Collections;

/**
 * A class for how a Minion targets an opponent.
 */
public class Minions_EnemyTargeting : MonoBehaviour {

    /**
     * Public Variables
     * DetectionRange - The range in which the Minion will detect enemies
     * AttackTarget - The tag on the enemy it should be attacking
     */
    public float DetectionRange;
    public string AttackTarget;

    /**
     * _stats - The stats of the minion
     * _attack - The attack function of the Minion
     * _attackRange - the range of detection.
     * _navMeshAgent - The NavMesh Agent on the minion
     */
    private Minion_Stats _stats;
    private IMinion_Attack _attack;
    private BoxCollider _attackRange;
    private NavMeshAgent _navMeshAgent;

	/**
     * Called when a Minion is created.
     * Initializes _stats, _navMeshAgent, _attackRange, _attack.
     */
	void Start () {
        this._stats = GetComponent<Minion_Stats>();
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._attackRange = GetComponent<BoxCollider>();
        this._navMeshAgent.stoppingDistance = this._stats.AttackRange;
        this._attackRange.size = new Vector3(this.DetectionRange, this.DetectionRange, this.DetectionRange);
        this._attack = GetComponent<IMinion_Attack>();
	}

    /**
     * Called when an object enters the attack range
     */
    void OnTriggerEnter(Collider collider) {
        // If the the object collides with an AttackTarget
        if (collider.gameObject.tag == AttackTarget) {

            // Set the destination to the new target
            this._navMeshAgent.SetDestination(collider.gameObject.transform.position);

            // Start Attacking
            StartCoroutine(this._attack.Attack(collider.gameObject.transform));
        }
    }
}
