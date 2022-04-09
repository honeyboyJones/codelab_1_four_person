using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] Vector2 gridDimensions;
    [SerializeField] float tileSize;
    public List<Coordinate> coords = new List<Coordinate>();

    //[SerializeField]
    //BattleManager battleManager;


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

    public void Start() {
        
        float tileSizeOffset = tileSize / 2; //Used for offsetting tile's worldPos

        //Declare list of Coordinates
        for ( int x = 0; x < gridDimensions.x; x++) {
            for (int y = 0; y < gridDimensions.y; y++) {

                Coordinate newCoordinate = new Coordinate();
                newCoordinate.InitializeCoordinate (
                    new Vector2(x, y),
                    new Vector3 (x - gridDimensions.x / 2 + tileSizeOffset, y - gridDimensions.y / 2 + tileSizeOffset, 0
                    ));
                
                coords.Add(newCoordinate);
            }
        }  

        //if(BattleManager.instance != null) {
        //    battleManager = BattleManager.instance;
        //}
    }
}


[System.Serializable]
public class Coordinate {

    public Vector2 coordinate;
    public Vector3 worldPosition;
    public GridActor occupiedBy;


    public void InitializeCoordinate(Vector2 coord, Vector3 worldPos, GridActor occupied = null) {
        coordinate = coord;
        worldPosition = worldPos;
        occupiedBy = occupied; 
    }

    public void UpdateCoordinateOccupancy(GridActor occupied = null) {
    
    
    }
}

