using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores a list of all coordinates on the grid, coordinates are updated based on battler conditions
/// 
/// </summary>

public class GridManager : MonoBehaviour {

    // grid definitions
    [SerializeField] Vector2 gridDimensions;
    [SerializeField] float tileSize;

    // list of all coordinates
    public List<Coordinate> coords = new List<Coordinate>();

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
                    )); // use coordinate class function to store coordinate information
                
                coords.Add(newCoordinate); // store coordinate to declared list
            }
        }  
    }
}

/// <summary>
/// Utility class containing coordinate info and functions to update that info
/// 
/// </summary>

[System.Serializable]
public class Coordinate {

    // coordinate info
    public Vector2 coordinate;
    public Vector3 worldPosition;
    public GridActor occupiedBy;

    // set all fields of coordinate info
    public void InitializeCoordinate(Vector2 coord, Vector3 worldPos, GridActor occupied = null) {
        coordinate = coord;
        worldPosition = worldPos;
        occupiedBy = occupied; 
    }

    // update coordinate grid actor (not yet used)
    public void UpdateCoordinateOccupancy(GridActor occupied = null) {
    
    
    }
}

