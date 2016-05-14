using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent (typeof(ProfileSystem))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Minions_State))]
[RequireComponent(typeof(Minions_Navigation))]
/**
 * The basic Minion implementation.
 * Has a basic melee attack.
 */
public class Minion_Basic : NetworkBehaviour, IMinion_Attack, IKillable, ObjectSelector.ISelectable {

    /**
     * Private Variables
     * _stats - The stats of the minion
     * _navMeshAgent - The NavMesh Agent on the minion
     */ 
    private NavMeshAgent _navMeshAgent;
    private Minions_State _state;
    private Transform _endTarget;
    private ProfileSystem _ps;

    /**
     * Called when Minion is created.
     * Initializes _stats and _navMeshAgent.
     */
	void Start () {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._state = GetComponent<Minions_State>();
        this._endTarget = GameObject.Find(GetComponent<Minions_Navigation>().MoveTarget).transform;
        MinionManager.AddActiveMinion(this.gameObject);
        this._ps = this.GetComponent<ProfileSystem>();
    }

    void OnEnable()
    {
        RoundManager.AddListener("RoundEnded", OnDeath);
        RegisterAsSelectable();
    }

    void OnDisable()
    {
        RoundManager.RemoveListener("RoundEnded", OnDeath);
        UnregisterAsSelectable();
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
            
            if(Vector3.Distance(this.transform.position, target.transform.position) <= _ps.AttackRange)
            {
                this.gameObject.transform.LookAt(target);

                // Do the attack damage
                ProfileEffect hitEffect = new ProfileEffect(NetworkInstanceId.Invalid, healthPointsAdd: -1 * _ps.MeleeDamageDealt);
                KUSNetworkManager.HostPlayer.CmdAddProfileEffect(target.GetComponent<NetworkIdentity>(), hitEffect);
            }
            else
            {
                this._navMeshAgent.SetDestination(target.transform.position);
            }
            if (target == null)
            {
                this._navMeshAgent.SetDestination(this._endTarget.position);

                //Set state to walking
                this._state.State = MINION_STATE.WALKING;
            }
            else {
                // Wait the attack range
                yield return new WaitForSeconds(this._ps.AttackSpeed);

                // Attack Again
                StartCoroutine("Attack", target);
            }

        }
        else {
            if (this.gameObject != null){
                // Start pursuing the main target again
                this._navMeshAgent.SetDestination(this._endTarget.position);

                //Set state to walking
                this._state.State = MINION_STATE.WALKING;
            }
        }
    }
    
    public void OnDeath()
    {
        MinionManager.RemoveActiveMinion(this.gameObject);
        
        if(this._ps.Killer != NetworkInstanceId.Invalid)
        {
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(this._ps.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)this._ps.Worth);
                playerStats.AddMinionKill();
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(this._ps.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)this._ps.Worth);
                playerStats.AddMinionKill();
            }
        }

        // Unsubscribe
        RoundManager.RemoveListener("RoundEnded", OnDeath);

        Destroy(this.gameObject);
    }


    public GameObject GameObject {
        get { return gameObject; }
    }

    public void RegisterAsSelectable() {
        if (KUSNetworkManager.LocalPlayer.Class == NetworkPlayerObject.PlayerClass.OVERSEER) ObjectSelector.Selectables.Add(this);
    }
    public void UnregisterAsSelectable() {
        if (KUSNetworkManager.LocalPlayer.Class == NetworkPlayerObject.PlayerClass.OVERSEER) ObjectSelector.Selectables.Remove(this);
    }

    public Collider GetSelectionCollider() {
        return GetComponent<Collider>();
    }
}
