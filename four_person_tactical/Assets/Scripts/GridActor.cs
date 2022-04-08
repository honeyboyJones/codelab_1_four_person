using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActor : MonoBehaviour {

    GridManager gridManager;
    public Vector2 coordinate;

    void Start() { 
        if (GridManager.instance != null) {
            gridManager = GridManager.instance;
        }
    }

}
