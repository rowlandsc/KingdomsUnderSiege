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
    NetworkPlayerOwner owner;

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
        owner = GetComponent<NetworkPlayerOwner>();
        if(owner && KUSNetworkManager.OverseerPlayer) owner.Owner = KUSNetworkManager.OverseerPlayer;
    }

    void OnEnable()
    {
        if (KUSNetworkManager.LocalPlayer.isServer) RoundManager.AddListener("RoundEnded", OnDeath);
        if (KUSNetworkManager.OverseerPlayer.isLocalPlayer) RegisterAsSelectable();
    }

    void OnDisable()
    {
        if (KUSNetworkManager.LocalPlayer.isServer) RoundManager.RemoveListener("RoundEnded", OnDeath);
        if (KUSNetworkManager.OverseerPlayer.isLocalPlayer) UnregisterAsSelectable();
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
                NetworkInstanceId netID = NetworkInstanceId.Invalid;
                if (owner != null && owner.Owner != null) netID = owner.Owner.netId;
                KUSNetworkManager.HostPlayer.CmdMinionMelee(GetComponent<NetworkIdentity>(), transform.position, transform.rotation);
                ProfileEffect hitEffect = new ProfileEffect(netID, healthPointsAdd: -1 * GetComponent<ProfileSystem>().MeleeDamageDealt);
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
            if (KUSNetworkManager.LocalPlayer.isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(this._ps.Killer);
                NetworkPlayerStats playerStats;
                NetworkPlayerOwner playerOwner = player.GetComponent<NetworkPlayerOwner>();

                if (playerOwner)
                {
                    playerStats = playerOwner.GetComponent<NetworkPlayerStats>();
                }
                else
                {
                    playerStats = player.GetComponent<NetworkPlayerStats>();
                }
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
