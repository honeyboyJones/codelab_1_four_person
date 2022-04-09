using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Battler Stats", menuName = "Battler Stats")]
public class BattlerStats : ScriptableObject {

    [Header("Cosmetics")]
    public Sprite portrait;
    public string _name;
    public GameObject prefab;

    [Header("Core Stats")]
    public int maxHP;
    public int currentHP;
    public int currentShield;
    public int speed;
    public int initiative;
    public int strength;
    public int attackRange;
    public int defence;

    public int turnIndex;
}

