using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] Vector2 gridDimensions;
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

        for( int x = 0; x<gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                Coordinate newCoordinate = new Coordinate();
                newCoordinate.coord = new Vector2(x, y);
                coords.Add(newCoordinate);
            }
        }

        UpdateCoordinate(coords[3], battlers[0].GetComponent<GridActor>());
    }

    void UpdateCoordinate(Coordinate targetCoord, GridActor actor = null)
    {
        if (actor == null)
        {
            targetCoord.occupiedBy = null;
        }
        else
        {
            Debug.Log("updating coord");
            targetCoord.occupiedBy = actor;
        }
    }

}
[System.Serializable]
public struct Coordinate{

    public Vector2 coord;
    public GridActor occupiedBy;



}

