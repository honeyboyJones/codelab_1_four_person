using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridActor))]
public class Battler : MonoBehaviour {

    [SerializeField] BattlerStats stats;
    //GridActor thisActor;

    private void Start() {
        stats.currentHP = stats.maxHP;
        //thisActor = GetComponent<GridActor>();

    }


}
