using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public List<Coordinate> coords = new List<Coordinate>();
    public List<Battler> battlers = new List<Battler>();


    #region Singleton

    public static GridManager instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning! More than one instance of GridManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start() {
        
    }


}

public struct Coordinate {

    Vector2 coord;

}
