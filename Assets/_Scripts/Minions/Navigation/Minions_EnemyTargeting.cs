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
    private Minion_Stats _stats;
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
        this._stats = GetComponent<Minion_Stats>();
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshAgent.stoppingDistance = this._stats.AttackRange;
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
            Debug.Log("Found " + this._enemiesFound[0]);
            // Go to first target found
            this._navMeshAgent.SetDestination(this._enemiesFound[ZERO].transform.position);

            // Set state to attacking
            this._state.State = MINION_STATE.ATTACKING;

            // Start Attacking
            StartCoroutine(this._attack.Attack(this._enemiesFound[ZERO].gameObject.transform));
        }
    }
}
