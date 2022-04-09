using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the stats used for the battlers
/// This is a scriptable object, which means we dont have access to any of the Unity-specific functions
/// It is something to get a reference to that we need to create a lot of (equipment, enemies, etc)
/// Good for things that dont need instanced, something that is always accessable
/// </summary>

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
    public int initiative; // not yet used
    public int strength;
    public int attackRange;
    public int defence; // not yet used
    public int turnIndex; // not yet used
}

