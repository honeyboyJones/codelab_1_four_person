using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    /// <summary>
    /// Battlemanager contains all references to battlers
    /// Runs state machine for player and enemy turns
    /// Facilitates interactions between battlers
    /// </summary>
    ///

    // references
    PlayerController playerController;
    GridManager gridManager;

    // enum for battle states
    public enum BattleState {
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        RoundOver,
        BattleOver
    }

    [SerializeField] private BattleState currentState;

    // all references to battlers
    public List<Battler> battlers = new List<Battler>();
    public List<Battler> playerBattlers = new List<Battler>();
    public List<Battler> enemyBattlers = new List<Battler>();
    List<Battler> turnOrderBattlers = new List<Battler>();
    [SerializeField] Vector2[] spawnPositions;



    //public delegate void OnBattleState(float dummy);
    //public event OnBattleState BattleStartCallback;


    //not currently used
    /*#region Singleton

    public static BattleManager instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning! More than one instance of BattleManager found!");
            return;
        }
        instance = this;
    }
    #endregion */

    private void Start()
        // get references
    {
        if (PlayerController.instance != null) {
            playerController = PlayerController.instance;
            playerController.PlayerTurnOverCallback += TransitionStates;
        }
        if (GridManager.instance != null) {
            gridManager = GridManager.instance;
        }

        battlers = GetAllBattlers();

        //start state machine

        currentState = BattleState.BattleStart;
        StartCoroutine(RunCurrentState());
        
    }

    // transition states to target state
    void TransitionStates(BattleState targetState) {
        currentState = targetState;
        StartCoroutine(RunCurrentState()); 
    }

    // primary state machine based on current state
    IEnumerator RunCurrentState() {
        switch(currentState) {
            case BattleState.BattleStart:
                InstanceBattlers(); // spawn battlers

                EstablishTurnOrder(); // add battlers to turn order
                TransitionStates(BattleState.PlayerTurn); // start player's turn
                break;
            case BattleState.PlayerTurn: 
                // trigger player methods
                playerController.takingTurn = true;
                playerController.currentBattler = CurrentBattler();
                StartCoroutine(playerController.TakeTurn());

                break;
                //future states to be developed
            case BattleState.EnemyTurn:
                break;
            case BattleState.RoundOver:
                EstablishTurnOrder();
                //TransitionStates(turnOrderBattlers[0].controlledByPlayer ? BattleState.PlayerTurn | BattleState.EnemyTurn);
                break;
            case BattleState.BattleOver:
                break;
        }
        yield return null;
    }
    // debug method 
    void EstablishTurnOrder() {     

        turnOrderBattlers.Add(battlers[0]);
        turnOrderBattlers.Add(battlers[1]);
    }
    // function for spawning battlers and saving their reference
    private void InstanceBattlers() {
        int index = 0;

        for (int i = 0; i <= battlers.Count - 1; i++) { // find every battler to be spawned
            // initialize battler and gridactor component
            Coordinate targetCoord = gridManager.coords.Find(x => x.coordinate == spawnPositions[index]);
            Battler newBattler = Instantiate
                (battlers[i].stats.prefab,
                targetCoord.worldPosition,
                Quaternion.identity).GetComponent<Battler>();

            newBattler.thisActor = newBattler.GetComponent<GridActor>();
            newBattler.thisActor.coord = targetCoord;
            newBattler.BattlerAttackCallback +=  BattlerCombat;
            battlers[i] = newBattler; // save instance of battler to reference


            // debug
            if(!newBattler.controlledByPlayer)
            {
                targetCoord.occupiedBy = newBattler.thisActor;
            }

            index ++;
        }
    }
    // simple attack framework
    void BattlerCombat(Vector2 coord, int amount){
        Battler defendingBattler = gridManager.coords.Find(x => x.coordinate == coord).occupiedBy.GetComponent<Battler>();

        defendingBattler.TakeDamage(amount);
    }
    // combine battler lists
    List<Battler> GetAllBattlers() { 
        List<Battler> allBattlers = new List<Battler>();
        foreach (Battler battler in playerBattlers) {
            allBattlers.Add(battler);
        }
        foreach (Battler battler in enemyBattlers) {
            allBattlers.Add(battler);
        }
        return allBattlers;
    }
    // move next in turn order index
    Battler CurrentBattler() {
        return turnOrderBattlers[0];
        turnOrderBattlers.RemoveAt(0);
    }

}
