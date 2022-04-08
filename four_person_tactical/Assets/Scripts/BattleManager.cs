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

    private void Start() {
        if(PlayerController.instance != null)
        {
            playerController = PlayerController.instance;
        }
        
    }


    void TrackTurns() {
    
    }

    void PlayerTurn() { 
    
    }


    void EnemyTurn() { 
    
    }


}
