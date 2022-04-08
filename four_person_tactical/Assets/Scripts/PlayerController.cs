using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public delegate void OnPlayerAction(BattleManager.BattleState targetState);
    public event OnPlayerAction PlayerTurnOverCallback; //anything can subscribe to this event as long as it has a reference to this instance


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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        takingTurn = false;
        StartCoroutine(TakeTurn());
    }

    public bool takingTurn;

    List<Battler> playerBattlers = new List<Battler>();
    public Battler currentBattler;


    IEnumerator TakeTurn() {
        while(takingTurn) {

            yield return null;

        }

        Debug.Log("Sending Event");
        PlayerTurnOverCallback?.Invoke(BattleManager.BattleState.EnemyTurn);
    }
}
