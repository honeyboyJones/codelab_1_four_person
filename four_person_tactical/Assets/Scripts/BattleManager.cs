using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    PlayerController playerController;

    List<Battler> playerBattlers = new List<Battler>();
    List<Battler> enemyBattlers = new List<Battler>();
    List<Battler> turnOrderBattlers = new List<Battler>();

    public enum BattleState
    {
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        RoundOver,
        BattleOver
    }

    private BattleState currentState;

    private void Start() {
        if(PlayerController.instance != null)
        {
            playerController = PlayerController.instance;

            playerController.PlayerTurnOverCallback += TransitionStates;
        }

        currentState = BattleState.BattleStart;
        
    }
    void TransitionStates(BattleState targetState) {
        currentState = targetState;

        StartCoroutine(RunCurrentState()); 
    }

    IEnumerator RunCurrentState()
    {
        switch(currentState)
        {
            case BattleState.BattleStart:
                EstablishTurnOrder();
                TransitionStates(BattleState.PlayerTurn);
                break;
            case BattleState.PlayerTurn:
                playerController.takingTurn = true;


                break;
            case BattleState.EnemyTurn:
                break;
            case BattleState.RoundOver:
                EstablishTurnOrder();
                TransitionStates(turnOrderBattlers[0].controlledByPlayer ? BattleState.PlayerTurn | BattleState.EnemyTurn);
                break;
            case BattleState.BattleOver:
                break;
        }
        yield return null;
    }
    
    void EstablishTurnOrder() {
        List<Battler> allBattlers = new List<Battler>();
        foreach (Battler battler in playerBattlers) {
            allBattlers.Add(battler);
        }
        foreach (Battler battler in enemyBattlers) {
            allBattlers.Add(battler);
        }

        turnOrderBattlers.Add(playerBattlers[0]);
        turnOrderBattlers.Add(enemyBattlers[0]);
    }

    Battler CurrentBattler() {
        return turnOrderBattlers[0];
        turnOrderBattlers.RemoveAt(0);
    }

}
