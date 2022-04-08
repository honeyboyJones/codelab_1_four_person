using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] Vector2 gridDimensions;
    [SerializeField] float tileSize;
    public List<Coordinate> coords = new List<Coordinate>();
    public List<Battler> battlers = new List<Battler>();

    [SerializeField]
    Vector2[] spawnPositions;

    [SerializeField]
    BattleManager battleManager;


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

        //Declare list of Coordinates
        for( int x = 0; x<gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                Coordinate newCoordinate = new Coordinate();
                newCoordinate.coord = new Vector2(x, y);
                float tileSizeOffset = tileSize / 2;
                
                newCoordinate.worldPos = new Vector3(x - gridDimensions.x / 2 + tileSizeOffset, 0, y - gridDimensions.y / 2);
                coords.Add(newCoordinate);
            }
        }

        
        

        if(BattleManager.instance != null)
        {
            battleManager = BattleManager.instance;
            battleManager.BattleStartCallback += SpawnBattler;
        }
    }

    private void SpawnBattler(float dummy)
    {
        int index = 0;

        foreach (Battler battler in battlers)
        {
            Coordinate targetCoord = new Coordinate();
            targetCoord.coord = spawnPositions[index];
            Coordinate targetWorldPos = coords.Find(x => x.coord == targetCoord.coord);
            Instantiate(battler.stats.prefab, new Vector3 (targetWorldPos.worldPos.x, 0, targetWorldPos.worldPos.z), Quaternion.identity);
            index ++;
        }
    }

    Coordinate UpdateCoordinate(Coordinate targetCoord, GridActor actor = null) {
        Coordinate newCoordinate = new Coordinate();
        newCoordinate.coord = targetCoord.coord;

        if (actor == null)
        {
            newCoordinate.occupiedBy = null;
        }
        else
        {
            newCoordinate.occupiedBy = actor;
        }

        return newCoordinate;
    }

}
[System.Serializable]
public class Coordinate {

    public Vector2 coord;
    public Vector3 worldPos;
    public GridActor occupiedBy;



}

