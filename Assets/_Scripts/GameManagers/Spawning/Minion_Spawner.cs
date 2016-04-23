using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

/**
 * A class for managing the Minion spawner.
 */
public class Minion_Spawner : NetworkBehaviour, IKillable {

    /**
     * Constants Description
     * The following string constants are the names of the coroutines in this class. Used in the StartCoroutine()
     * function.
     * 
     * The floating point numbers are the constants 0 and 1. They are used to check if the timer is complete, and 
     * wait for one second in the WaitForSeconds() function.
     */
    public const string PREROUND_WAIT = "PreroundWait", SPAWN_MINIONS = "SpawnMinions", START_TIMER = "StartTimer";
    public const float ZERO = 0f, ONE = 1f;

    /**
     * Public Variable Description
     * Minion - The Minion to be spawned.
     * SpawnPoint - The location the minion will spawn at.
     * NumberToSpawn - The number of minions to spawn in the SpawnMinions() function.
     * SpawnDelay - How long to wait between each minion spawn sequence.
     * TimeBetweenMinions - THe time waited between each minion in the spawn sequence.
     */
    public GameObject Minion;
    public Transform SpawnPoint;
    public float NumberToSpawn=5f, SpawnDelay=20f, TimeBetweenMinions=1f;

    /**
     * Private Variable Description
     * _timer - A float to hold how much time is being counted down.
     * _roundManager - Holds the Instance of the RoundManager class.
     */
    [SyncVar]
    private float _timer;
    private RoundManager _roundManager;

    /**
     * Used to initialize _roundManager and start the spawn cycle.
     */
    void Start(){
        this._roundManager = RoundManager.Instance;

        if (isServer)
        {
            // Start spawn cycle
            StartCoroutine(PREROUND_WAIT);
        }
    }

    /**
     * Waits to spawn minions until a round is started.
     */
    private IEnumerator PreroundWait()
    {
        // If it is still a preround, wait
        // Otherwise spawn minions
        if (this._roundManager.IsPreround()){

            // Wait for one second
            yield return new WaitForSeconds(ONE);

            // Recurse
            StartCoroutine(PREROUND_WAIT);
        }
        else{

            // Spawn Minions
            StartCoroutine(SPAWN_MINIONS);
        }
    }

    /**
     * Spawns NumberToSpawn minions and then starts a timer
     * for the SpawnDelay amount.
     */
    private IEnumerator SpawnMinions(){

        // Spawn NumberToSpawnMinions
        for (int i = 0; i < this.NumberToSpawn; i++){

            // Create a minion
            GameObject minionToSpawn = (GameObject) Instantiate(Minion, this.SpawnPoint.transform.position, this.SpawnPoint.transform.rotation);
            NetworkServer.Spawn(minionToSpawn);

            // Wait TimeBetweenMinions until next one is spawned
            yield return new WaitForSeconds(this.TimeBetweenMinions);
        }

        // Set timer to the SpawnDelay amount
        this._timer = this.SpawnDelay;

        // Start the timer
        StartCoroutine(START_TIMER);
    }

    /**
     * Ticks down the timer until it is zero. When it reaches zero,
     * it will spawn another set of minions. If a preround is started,
     * starts PreroundWait() instead.
     */
    private IEnumerator StartTimer(){

        // If there is still time left on the timer, tick down
        // Otherwise SpawnMinions()
        if (this._timer > ZERO){

            // Decrement timer
            --this._timer;

            // Wait for one second
            yield return new WaitForSeconds(ONE);

            // If it is a preround, set timer to zero
            if (this._roundManager.IsPreround()){
                this._timer = ZERO;
            }

            // Recurse
            StartCoroutine(START_TIMER);
        }
        else{

            // If it is not a preround, spawn minions
            // Otherwise wait until a round starts
            if (!this._roundManager.IsPreround()){
                StartCoroutine(SPAWN_MINIONS);
            }
            else{
                StartCoroutine(PREROUND_WAIT);
            }
        }
    }

    public void OnDeath()
    {
        NetworkPlayerObject player = KUSNetworkManager.OverseerPlayer;
        player.GetComponent<NetworkPlayerStats>().AddGold(1000);

        Destroy(this.gameObject);
    }
}
