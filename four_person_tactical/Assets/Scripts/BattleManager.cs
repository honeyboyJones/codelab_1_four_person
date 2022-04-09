using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    PlayerController playerController;
    [SerializeField] GridManager gridManager;

    public enum BattleState {
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        RoundOver,
        BattleOver
    }
    [SerializeField] private BattleState currentState;

    public List<Battler> battlers = new List<Battler>();
    public List<Battler> playerBattlers = new List<Battler>();
    public List<Battler> enemyBattlers = new List<Battler>();
    List<Battler> turnOrderBattlers = new List<Battler>();
    [SerializeField] Vector2[] spawnPositions;

    public delegate void OnBattleState(float dummy);
    public event OnBattleState BattleStartCallback;


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

    private void Start() {
        if (PlayerController.instance != null) {
            playerController = PlayerController.instance;
            playerController.PlayerTurnOverCallback += TransitionStates;
        }
        if (GridManager.instance != null) {
            gridManager = GridManager.instance;
        }

        battlers = GetAllBattlers();

        currentState = BattleState.BattleStart;
        StartCoroutine(RunCurrentState());
        
    }
    void TransitionStates(BattleState targetState) {
        currentState = targetState;
        StartCoroutine(RunCurrentState()); 
    }

    IEnumerator RunCurrentState() {
        switch(currentState) {
            case BattleState.BattleStart:
                InstanceBattlers();

                EstablishTurnOrder();
                TransitionStates(BattleState.PlayerTurn);
                break;
            case BattleState.PlayerTurn:

                playerController.takingTurn = true;
                playerController.currentBattler = CurrentBattler();
                StartCoroutine(playerController.TakeTurn());

                break;
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
    
    void EstablishTurnOrder() {     

        turnOrderBattlers.Add(battlers[0]);
        turnOrderBattlers.Add(battlers[1]);
    }

    private void InstanceBattlers() {
        int index = 0;

        for (int i = 0; i <= battlers.Count - 1; i++) {

            Coordinate targetCoord = gridManager.coords.Find(x => x.coordinate == spawnPositions[index]);
            Battler newBattler = Instantiate
                (battlers[i].stats.prefab,
                targetCoord.worldPosition,
                Quaternion.identity).GetComponent<Battler>();

            newBattler.thisActor = newBattler.GetComponent<GridActor>();
            newBattler.thisActor.coord = targetCoord;
            newBattler.BattlerAttackCallback +=  BattlerCombat;
            battlers[i] = newBattler;
            
            if(!newBattler.controlledByPlayer)
            {
                targetCoord.occupiedBy = newBattler.thisActor;
            }

            index ++;
        }
    }

    void BattlerCombat(Vector2 coord, int amount){
        Battler defendingBattler = gridManager.coords.Find(x => x.coordinate == coord).occupiedBy.GetComponent<Battler>();

        defendingBattler.TakeDamage(amount);
    }

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

    Battler CurrentBattler() {
        return turnOrderBattlers[0];
        turnOrderBattlers.RemoveAt(0);
    }

}
