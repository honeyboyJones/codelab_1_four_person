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

    void TakeDamage(int damageTaken)
    {
        int excessDamage;

        if(stats.currentShield > 0)
        {
            if(damageTaken > stats.currentShield)
            {
                excessDamage = damageTaken - stats.currentShield;
                stats.currentShield = 0;
                stats.currentHP -= excessDamage;
            }

        }
        else
        {
            stats.currentHP -= damageTaken;
        }

       





    }

    void AddDefence()
    {
        stats.currentShield += stats.defence;

       
    }

    void Attack()
    {

    }

    void Move()
    {
        
    }

}
