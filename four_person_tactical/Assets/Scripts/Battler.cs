using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Battler is a generic class both the player and enemy use
/// Instance in a battle scene stored in BattleManager and GridManager
/// Basic battling functions for interaction
/// </summary>
///

// GridActor communicates with the GridManager, battlers use it to move around the grid
[RequireComponent(typeof(GridActor))]
public class Battler : MonoBehaviour {

    // events for battler action
    public delegate void OnBattlerAction(Vector2 coord, int amount);
    public event OnBattlerAction BattlerAttackCallback;

    //references
    public BattlerStats stats;
    public GridActor thisActor;

    // player-dependant variables
    public bool controlledByPlayer;
    [SerializeField] bool targeting;

    [SerializeField] GameObject[] targets;
    List<GameObject> instancedTargets = new List<GameObject>();

    [SerializeField]
    Text hpText;

    // get references
    public void Start() {
        stats.currentHP = stats.maxHP;
        thisActor = GetComponent<GridActor>(); // this isn't getting called before battlers are instanced, so it's also set int the InstanceBattlers() function

        if(! controlledByPlayer){
            StartCoroutine(DebugStaticText());
        }
    }

    // instantiate targets on adjacent spaces
    public void VisualizeAction(int actionIndex) {
        EndVisualization();
        List<Coordinate> targetCoords = new List<Coordinate>();

        // state machine for what action is being taken (moving, attacking, etc)
        // instantiate target game objects by GridActor adjacent coordinates
        switch (actionIndex) {
            case 0:
                targetCoords = thisActor.CalculateAdjacentCoords(stats.speed); // GridActor function takes range variable
                foreach(Coordinate coord in targetCoords) { // looop through all adjacent coordinates
                    GameObject newTarget = Instantiate(targets[0], coord.worldPosition, Quaternion.identity); // spawn target
                    newTarget.GetComponent<TargetHover>().coord = new Vector2(coord.coordinate.x, coord.coordinate.y); // set target coords
                    instancedTargets.Add(newTarget); // store target for later
                }            
                break;
            case 1:
                targetCoords = thisActor.CalculateAdjacentCoords(stats.attackRange);
                foreach(Coordinate coord in targetCoords) { 
                    if (coord.occupiedBy != null) {
                        GameObject newTarget = Instantiate(targets[1], coord.worldPosition, Quaternion.identity);
                        newTarget.GetComponent<TargetHover>().coord = new Vector2(coord.coordinate.x, coord.coordinate.y);
                        instancedTargets.Add(newTarget);
                    }
                }
                break;
            case 2:
                break;
        }        
    }

    // delete all spawn targets
    public void EndVisualization() { 
        foreach(GameObject go in instancedTargets) {
            Destroy(go);
        }
    }

    // trigger Gridactor function and visualization
    public IEnumerator Move(Vector2 coord) {

        yield return StartCoroutine(thisActor.MoveToCoord(coord));

        EndVisualization();
    }

    public void AddDefence() {
        stats.currentShield += stats.defence;


    }

    // invoke event for battle manager, end the visualization
    public IEnumerator Attack(Vector2 coord) {
        BattlerAttackCallback?.Invoke(coord, stats.strength);
        EndVisualization();

        yield return null;
    }

 // logic for subtracting damage from HP and shield
    public void TakeDamage(int damageTaken) {

        int excessDamage;
        if(stats.currentShield > 0) {
            if (damageTaken > stats.currentShield) {
                excessDamage = damageTaken - stats.currentShield;
                stats.currentShield = 0;
                stats.currentHP -= excessDamage;
            } else
                stats.currentShield -= damageTaken;

        } else 
        {
            if(damageTaken > stats.currentHP){
                stats.currentHP = 0;
                StartCoroutine(BattlerDeath());
            } else
            stats.currentHP -= damageTaken;

        }
    }

    // placeholder enemy HP UI
    IEnumerator DebugStaticText(){
        hpText = GameObject.FindGameObjectWithTag("Static UI").GetComponent<Text>();
        while(true){
            hpText.text = stats.currentHP.ToString();
            yield return new WaitForFixedUpdate();
        }
    }

    // destroy battler on death
    IEnumerator BattlerDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
