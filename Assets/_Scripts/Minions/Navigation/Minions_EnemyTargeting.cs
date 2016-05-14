using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Minion_Stats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Minions_State))]
[RequireComponent(typeof(Minions_Navigation))]
/**
 * A class for how a Minion targets an opponent.
 */
public class Minions_EnemyTargeting : MonoBehaviour
{
    public const int ZERO = 0;

    /**
     * Public Variables
     * DetectionRange - The range in which the Minion will detect enemies
     */
    public float DetectionRange;
    public LayerMask TargetEnemy;

    /**
     * _stats - The stats of the minion
     * _attack - The attack function of the Minion
     * _attackRange - the range of detection.
     * _navMeshAgent - The NavMesh Agent on the minion
     * _state - The state the minion is in
     */
    private IMinion_Attack _attack;
    private NavMeshAgent _navMeshAgent;
    private Collider[] _enemiesFound;
    private Minions_State _state;

    /**
     * Called when a Minion is created.
     * Initializes _stats, _navMeshAgent, _attackRange, _attack, _state.
     */
    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshAgent.stoppingDistance = this.GetComponent<ProfileSystem>().AttackRange;
        this._attack = GetComponent<IMinion_Attack>();
        this._state = GetComponent<Minions_State>();
    }

    // Called every frame
    void Update(){

        // If it's not already attacking a target
        if (this._state.State != MINION_STATE.ATTACKING){

            // Search for enemies
            this._enemiesFound = Physics.OverlapSphere(
                this.transform.position,
                this.DetectionRange,
                this.TargetEnemy
            );
        }

        // If an enemy is found, attack the first one found.
        if (this._enemiesFound.Length > ZERO && this._state.State != MINION_STATE.ATTACKING){
            // Go to first target found
            GameObject target = ClosestTarget(_enemiesFound);
            this._navMeshAgent.SetDestination(target.transform.position);

            // Set state to attacking
            this._state.State = MINION_STATE.ATTACKING;

            // Start Attacking
            StartCoroutine(this._attack.Attack(target.transform));
        }
    }

    GameObject ClosestTarget(Collider[] targets)
    {
        GameObject closestTarget = targets[0].gameObject;
        float distance = Vector3.Distance(this.transform.position, targets[0].transform.position);
        float tDistance;
        for(int i = 1; i < targets.Length; ++i)
        {
            tDistance = Vector3.Distance(this.transform.position, targets[i].transform.position);
            if (tDistance < distance)
            {
                distance = tDistance;
                closestTarget = targets[i].gameObject;
            }
        }

        return closestTarget;
    }
}
