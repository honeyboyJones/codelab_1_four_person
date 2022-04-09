using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enables the player to interact with its battlers when its their turn
/// is a sub-state machine of battle manager
/// communicates only with the battle manager by being referenced by it 
/// </summary>
public class PlayerController : MonoBehaviour {

    // events for triggering player actions
    public delegate void OnPlayerAction(BattleManager.BattleState targetState);
    public event OnPlayerAction PlayerTurnOverCallback; //anything can subscribe to this event as long as it has a reference to this instance

    //player state machine enum
    public enum Actions { Move, Attack, Defend }
    public Actions currentAction;

    public bool takingTurn;                             //battler manager triggering bool
    public Battler currentBattler;                      //currentBattler is fed to this by battle manager

    #region Singleton

    public static PlayerController instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning! More than one instance of PlayerController found!");
            return;
        }
        instance = this;
    }
    #endregion

    //coroutine runs while player takes turn ends with an event to the battle manager
    public IEnumerator TakeTurn() {
        while(takingTurn) { //execute this loop while player is taking their turn
            yield return null;
        }
        Debug.Log("Sending Event"); 
        PlayerTurnOverCallback?.Invoke(BattleManager.BattleState.EnemyTurn);
    }

    // middleman function to trigger battler function
    public void VisualizeTarget(int actionIndex) {
        currentBattler.VisualizeAction(actionIndex);
        currentAction = (Actions)actionIndex;
    }
    
    //Hooked up to the TargetHover class. Starts battler action sequences
    public void TargetSelected(Vector2 coord) { 


        switch (currentAction) {
            case Actions.Move:
                StartCoroutine(MoveBattler(coord));
                break;
            case Actions.Attack:
                StartCoroutine(BattlerAttack(coord));
                break;
            case Actions.Defend:
                break;

        }
    }

    // coroutines that wait on other coroutines to finish animating
    // triggers battler/grid actor animation sequence
    public IEnumerator MoveBattler(Vector2 coord) {
        yield return StartCoroutine(currentBattler.Move(coord));
        takingTurn = false;
        Debug.Log("Taking Turn false");
    }

    //trigger battler/battle manager functions
    public IEnumerator BattlerAttack(Vector2 coord) {
        yield return StartCoroutine(currentBattler.Attack(coord));
        takingTurn = false;
        Debug.Log("Taking Turn false");
    }

    //not in use yet
    public void AddDefense() {
        currentBattler.AddDefence();
    }
}
