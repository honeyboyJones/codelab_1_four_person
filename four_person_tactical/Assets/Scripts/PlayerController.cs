using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Actions { Move, Attack, Defend }
    public Actions currentAction;

    public delegate void OnPlayerAction(BattleManager.BattleState targetState);
    public event OnPlayerAction PlayerTurnOverCallback; //anything can subscribe to this event as long as it has a reference to this instance

    public bool takingTurn;

    List<Battler> playerBattlers = new List<Battler>();
    public Battler currentBattler;

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

    public IEnumerator TakeTurn() {
        while(takingTurn) { //execute this loop while player is taking their turn
            yield return null;
        }
        Debug.Log("Sending Event"); 
        PlayerTurnOverCallback?.Invoke(BattleManager.BattleState.EnemyTurn);
    }

    public void VisualizeTarget(int actionIndex) {
        currentBattler.VisualizeAction(actionIndex);
        currentAction = (Actions)actionIndex;
    }
    
    //Hooked up to a button
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

    public IEnumerator MoveBattler(Vector2 coord) {
        yield return StartCoroutine(currentBattler.Move(coord));
        takingTurn = false;
        Debug.Log("Taking Turn false");
    }

    public IEnumerator BattlerAttack(Vector2 coord) {
        yield return StartCoroutine(currentBattler.Attack(coord));
        takingTurn = false;
        Debug.Log("Taking Turn false");
    }

    public void AddDefense() {
        currentBattler.AddDefence();
    }
}
