using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridActor))]
public class Battler : MonoBehaviour {

    public BattlerStats stats;
    public GridActor thisActor;

    public bool controlledByPlayer;
    [SerializeField] bool targeting;

    [SerializeField] GameObject[] targets;
    List<GameObject> instancedTargets = new List<GameObject>();

    public void Start() {
        stats.currentHP = stats.maxHP;
        thisActor = GetComponent<GridActor>(); // this isn't getting called before battlers are instanced, so it's also set int the InstanceBattlers() function

    }

    public void VisualizeAction(int actionIndex) {
        EndVisualization();
        List<Coordinate> targetCoords = new List<Coordinate>();
        switch (actionIndex) {
            case 0:
                targetCoords = thisActor.CalculateAdjacentCoords(stats.speed);
                foreach(Coordinate coord in targetCoords) {
                    GameObject newTarget = Instantiate(targets[0], coord.worldPosition, Quaternion.identity);
                    newTarget.GetComponent<TargetHover>().coord = new Vector2(coord.coordinate.x, coord.coordinate.y);
                    instancedTargets.Add(newTarget);
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

    public void EndVisualization() { 
        foreach(GameObject go in instancedTargets) {
            Destroy(go);
        }
    }

    public IEnumerator Move(Vector2 coord) {

        yield return StartCoroutine(thisActor.MoveToCoord(coord));

        EndVisualization();
    }

    public void AddDefence() {
        stats.currentShield += stats.defence;


    }

    public void Attack() {

    }

 
    void TakeDamage(int damageTaken) {

        int excessDamage;
        if(stats.currentShield > 0) {
            if (damageTaken > stats.currentShield) {
                excessDamage = damageTaken - stats.currentShield;
                stats.currentShield = 0;
                stats.currentHP -= excessDamage;
            } else
                stats.currentShield -= damageTaken;

        } else 
            stats.currentHP -= damageTaken;       
    }

}
