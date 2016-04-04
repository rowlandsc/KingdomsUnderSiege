using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent (typeof(Minion_Stats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Minions_State))]
[RequireComponent(typeof(Minions_Navigation))]
/**
 * The basic Minion implementation.
 * Has a basic melee attack.
 */
public class Minion_Basic : NetworkBehaviour, IMinion_Attack, IKillable {

    /**
     * Private Variables
     * _stats - The stats of the minion
     * _navMeshAgent - The NavMesh Agent on the minion
     */ 
    private Minion_Stats _stats;
    private NavMeshAgent _navMeshAgent;
    private Minions_State _state;
    private Transform _endTarget;

    /**
     * Called when Minion is created.
     * Initializes _stats and _navMeshAgent.
     */
	void Start () {
        this._stats = GetComponent<Minion_Stats>();
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._state = GetComponent<Minions_State>();
        this._endTarget = GameObject.Find(GetComponent<Minions_Navigation>().MoveTarget).transform;
        MinionManager.AddActiveMinion(this.gameObject);
    }

    void OnEnable()
    {
        RoundManager.AddListener("RoundEnded", OnDeath);
    }

    void OnDisable()
    {
        RoundManager.RemoveListener("RoundEnded", OnDeath);
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

            this.gameObject.transform.LookAt(target);

            // Do the attack damage
            ProfileEffect hitEffect = new ProfileEffect(NetworkInstanceId.Invalid, healthPointsAdd: -1 * GetComponent<ProfileSystem>().MeleeDamageDealt);
            KUSNetworkManager.HostPlayer.CmdAddProfileEffect(target.GetComponent<NetworkIdentity>(), hitEffect);

            if (target == null){
                this._navMeshAgent.SetDestination(this._endTarget.position);

                //Set state to walking
                this._state.State = MINION_STATE.WALKING;
            }
            else{
                // Wait the attack range
                yield return new WaitForSeconds(this._stats.AttackSpeed);

                // Attack Again
                StartCoroutine("Attack", target);
            }
        }
        else {
            if (this.gameObject != null){
                // Start pursuing the main target again
                try
                {
                    this._navMeshAgent.SetDestination(this._endTarget.position);
                }
                catch { }

                //Set state to walking
                this._state.State = MINION_STATE.WALKING;
            }
        }
    }
    
    public void OnDeath()
    {
        MinionManager.RemoveActiveMinion(this.gameObject);
        
        NetworkInstanceId killer = this.GetComponent<ProfileSystem>().Killer;
        if(killer != NetworkInstanceId.Invalid)
        {
            ProfileSystem ps = this.GetComponent<ProfileSystem>();
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(killer);
                player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>().AddGold((int)ps.Worth);
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(killer);
                player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>().AddGold((int)ps.Worth);
            }
        }

        // Unsubscribe
        RoundManager.RemoveListener("RoundEnded", OnDeath);

        Destroy(this.gameObject);
    }
}
