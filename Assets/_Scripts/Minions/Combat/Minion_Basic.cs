using UnityEngine;
using System.Collections;

/**
 * The basic Minion implementation.
 * Has a basic melee attack.
 */
public class Minion_Basic : MonoBehaviour, IMinion_Attack {

    /**
     * Public Variables:
     * Target - The end target of the minion
     */
    public Transform Target;

    /**
     * Private Variables
     * _stats - The stats of the minion
     * _navMeshAgent - The NavMesh Agent on the minion
     */ 
    private Minion_Stats _stats;
    private NavMeshAgent _navMeshAgent;

    /**
     * Called when Minion is created.
     * Initializes _stats and _navMeshAgent.
     */
	void Start () {
        this._stats = GetComponent<Minion_Stats>();
        this._navMeshAgent = GetComponent<NavMeshAgent>();
	}

    /**
     * The way the Minion attacks. 
     * The basic Minion is a simple melee attack.
     * 
     * target - the enemy the minion is attacking.
     */
    public IEnumerator Attack(Transform target) {

        // If the target is still alive
        if (target != null) {

            // Do the attack damage
            target.gameObject.GetComponent<TEST_HEALTH>().Health -= this._stats.AttackPower;

            // Wait the attack range
            yield return new WaitForSeconds(this._stats.AttackSpeed);

            // Attack Again
            StartCoroutine("Attack", target);
        }
        else {

            // Start pursuing the main target again
            this._navMeshAgent.SetDestination(this.Target.position);
        }
    }
}
