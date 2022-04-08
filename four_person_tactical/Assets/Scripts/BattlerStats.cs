using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattlerStats {

    [Header("Cosmetics")]
    public Sprite portrait;
    public string name;
    public GameObject prefab;

    [Header("Core Stats")]
    public int maxHP;
    public int currentHP;
    public int currentShield;
    public int speed;
    public int inititive;
    public int strength;
    public int defence;

    public int turnIndex;
}

