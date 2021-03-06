﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/**
 *  A class for handling the round/pre-round system.
 */
public class RoundManager : NetworkBehaviour{

    /**
     * Constants Description
     * All constants are used in the StartCoroutine calls in order to
     * signify which Coroutine it is starting.
     * 
     * The name of the constant signifies which coroutine it should be used for.
     * 
     * ZERO just = 0 to check when the timer runs out.
     */
    public const string START_ROUND = "StartRound", START_FIRST_PREROUND = "StartFirstPreround",
        START_PREROUND = "StartPreround", START_TIMER = "StartTimer";
    public const float ZERO = 0f;

    /**
     * Public Variable Description
     * Instance - The instance of the manager. Used to create a singleton pattern.
     * RoundTime - How long a round should last.
     * PreroundTime - How long a pre-round should last.
     * FirstPreroundTime - How long the first pre-round should last.
     * NumberOfRounds - The number of rounds the game should have.
     * CountDownTime - The time left on the clock to count down.
     * RoundEvents - A dictionary to hold events based on rounds ending/starting.
     * RoundNumber - The current round number the game is on.
     * PreroundNumber - The current preround number the game is on.
     * NextPhase - What the next phase will be of the game.
     */
    public static RoundManager Instance = null;
    public float RoundTime = 120f, PreroundTime = 30f, FirstPreroundTime = 120f, NumberOfRounds = 10f;
    public Dictionary<string, UnityEvent> RoundEvents;
    [SyncVar]
    public float CountDownTime = 120f;
    public float RoundNumber = 0f, PreroundNumber = 0f;
    public string NextPhase = START_ROUND;

    /**
     * Private Variable Description
     * _isRound - True when it's currently a round phase, otherwise false.
     * _isPreround - True when it's currently a preround phase, otherwise false.
     * _isFirstPreround - True when it's currently the first preround phase, otherwise false.
     */
    [SyncVar]
    private bool _isRound = false;

    [SyncVar]
    private bool _isPreround = true;

    [SyncVar]
    private bool _isFirstPreround=true;
    private GameObject _door;
   
    /**
     * Called when a new instance of the RoundManager is created.
     * Automatically starts the game.
     */
    public override void OnStartServer(){
        
        if (isServer)
        {
            StartCoroutine(DoorLock());
        }
    }

    void Awake()
    {
        // If it doesn't already exist, create it
        // Otherwise destroy it
        if (Instance == null)
        {
            this.RoundEvents = new Dictionary<string, UnityEvent>();
            
            Instance = this;
        }
		else {
			Destroy(this);
        }
    }

    /**
     * Starts the game
     */
    public void StartGame(){
        StartCoroutine(START_FIRST_PREROUND);
    }

    /**
     * Starts a preround. Will set the boolean values, and 
     * the clock appropriately. When finished, will start a round.
     */
    private IEnumerator StartPreround(){


        RoundManager.TriggerEvent("RoundEnded");

        // Update what preround it is
        ++this.PreroundNumber;

        // Set the timer
        this.CountDownTime = this.PreroundTime;
        _door.SetActive(true);

        // Set booleans correctly
        this._isPreround = true;
        this._isFirstPreround = false;
        this._isRound = false;

        // Set what the next phase will be
        this.NextPhase = START_ROUND;

        // Start the count down
        yield return StartCoroutine(START_TIMER); 
    }

    /**
     * Starts a round. Will set the boolean values, and 
     * the clock appropriately. When finished, will start
     * a preround or end the game.
     */
    private IEnumerator StartRound(){

        RoundManager.TriggerEvent("PreroundEnded");
        _door.SetActive(false);

        // Update the round number
        ++this.RoundNumber;

        // Set the timer
        this.CountDownTime = this.RoundTime;

        // Set the booleans correctly
        this._isRound = true;
        this._isPreround = false;
        this._isFirstPreround = false;

        // If this is not the last round, start a preround after
        // this. Otherwise end the game.
        if (this.RoundNumber < this.NumberOfRounds){

            // Set the next phase
            this.NextPhase = START_PREROUND;

            // Start the count down
            yield return StartCoroutine(START_TIMER);
        }
        else{

            // End the game
            this.EndGame();
        }
    }

    /**
     * Starts the first preround of the game. Will set the boolean values, and 
     * the clock appropriately. When finished, will start a round.
     */
    private IEnumerator StartFirstPreround(){

        _door.SetActive(true);
        // Update the preround number
        ++this.PreroundNumber;

        // Set the timer
        this.CountDownTime = this.FirstPreroundTime;

        // Set the boolean values correctly
        this._isPreround = true;
        this._isFirstPreround = true;
        this._isRound = false;

        // Set the next phase
        this.NextPhase = START_ROUND;

        // Start the count down
        yield return StartCoroutine(START_TIMER);
    }

    /**
     * Starts counting down the timer. Starts the next phase after 
     * the timer hits 0. Uses the WaitForSeconds method to make the
     * timer 'tick'
     */
    private IEnumerator StartTimer(){

        // If it's still counting down, keep doing so.
        // Otherwise start the next phase.
        if (this.CountDownTime > ZERO){

            // Decrease countdown time by 1
            --CountDownTime;

            // Wait for a second
            yield return new WaitForSeconds(1);

            // Recurse
            StartCoroutine(START_TIMER);
        }
        else{

            // Start the next phase
            StartCoroutine(this.NextPhase);
        }
    }

    /**
     * Will end the game appropriately.
     */
    private void EndGame(){
        // TODO: END GAME FUNCTION
        Time.timeScale = 0;
    }

    /**
     * Returns true if the game is currently in a round. 
     * Otherwise returns false.
     */
    public bool IsRound(){
        return this._isRound;
    }

    /**
     * Returns true if the game is currently in a preround.
     * Otherwise returns false.
     */
    public bool IsPreround(){
        return this._isPreround;
    }

    /**
     * Returns true if the game is currently in the first preround.
     * Otherwise returns false.
     */
    public bool IsFirstPreround(){
        return this._isFirstPreround;
    }

    /// <summary>
    /// Adds a listener to the event dictionary
    /// </summary>
    /// <param name="eventName">Name of the event the listener should be added to</param>
    /// <param name="listener">The listener added to the dictionary</param>
    public static void AddListener(string eventName, UnityAction listener)
    {
        if(!eventName.Equals("RoundEnded") && !eventName.Equals("PreroundEnded"))
        {
            Debug.LogError("Only RoundEnded and PreroundEnded events can be registered here");
            return;
        }

        UnityEvent thisEvent = null;

        // If the eventName already exists in the dictionary, add the listener to that event
        if(RoundManager.Instance.RoundEvents.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }

        // Otherwise add a new event to the dictionary and add the listener to it
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            RoundManager.Instance.RoundEvents.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Removes a listener from an event in the dictionary
    /// </summary>
    /// <param name="eventName">The event to remove the listener from</param>
    /// <param name="listener">The listener to be removed</param>
    public static void RemoveListener(string eventName, UnityAction listener)
    {
        if (!eventName.Equals("RoundEnded") && !eventName.Equals("PreroundEnded"))
        {
            Debug.LogError("Only RoundEnded and PreroundEnded events can be unregistered here");
            return;
        }

        UnityEvent thisEvent = null;

        // Check to make sure the event exists, then remove the listener from it
        if(RoundManager.Instance.RoundEvents.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Triggers and event in the RoundEvent dictionary
    /// </summary>
    /// <param name="eventName">The name of the event to be triggered</param>
    public static void TriggerEvent(string eventName)
    {
        if (!eventName.Equals("RoundEnded") && !eventName.Equals("PreroundEnded"))
        {
            Debug.LogError("Only RoundEnded and PreroundEnded events can be triggered here");
            return;
        }

        UnityEvent thisEvent = null;

        // Make sure the event exists, then trigger it.
        if(RoundManager.Instance.RoundEvents.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public IEnumerator DoorLock()
    {
        while(GameObject.Find("HeroDoor(Clone)") == null)
        {
            yield return null;
        }

        this._door = GameObject.Find("HeroDoor(Clone)");
        StartGame();
    }
}
