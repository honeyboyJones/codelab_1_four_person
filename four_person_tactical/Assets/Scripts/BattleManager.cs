using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    PlayerController playerController;

    List<Battler> playerBattlers = new List<Battler>();
    List<Battler> enemyBattlers = new List<Battler>();

    public enum BattleState
    {
        BattleStart,
        PlayerTurn,
        EnemyTurn,
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


    void TrackTurns() {
    
    }

    void PlayerTurn() { 
    
    }


    void EnemyTurn() { 
    
    }

    void TransitionStates(BattleState targetState)
    {
        currentState = targetState;

        StartCoroutine(RunCurrentState()); 
    }

    IEnumerator RunCurrentState()
    {
        switch(currentState)
        {
            case BattleState.BattleStart:
                break;
            case BattleState.PlayerTurn:
                break;
            case BattleState.EnemyTurn:
                break;
            case BattleState.BattleOver:
                break;
        }
        yield return null;
    }

}
