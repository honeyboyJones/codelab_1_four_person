using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] Vector2 gridDimensions;
    public List<Coordinate> coords = new List<Coordinate>();
    public List<Battler> battlers = new List<Battler>();

    [SerializeField]
    Vector2[] spawnPositions;

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
        coords[3] = UpdateCoordinate(coords[3], battlers[0].GetComponent<GridActor>());

        if(BattleManager.instance != null)
        {
            battleManager = BattleManager.instance;
        }

        battleManager.BattleStartCallback += SpawnBattler;
    }

    public void SpawnBattler()
    {
        int index = 0;

        foreach (Battler battler in battlers)
        {
            Instantiate(battler.stats.prefab, new Vector3 (spawnPositions[index].x, 0, spawnPositions[index].y), Quaternion.identity);
            index ++;
        }
    }

    Coordinate UpdateCoordinate(Coordinate targetCoord, GridActor actor = null)
    {
        Coordinate newCoordinate = new Coordinate();
        newCoordinate.coord = targetCoord.coord;

        if (actor == null)
        {
            newCoordinate.occupiedBy = null;
        }
        else
        {
            Debug.Log("updating coord");
            newCoordinate.occupiedBy = actor;
        }

        return newCoordinate;
    }

}
[System.Serializable]
public struct Coordinate{

    public Vector2 coord;
    public GridActor occupiedBy;



}

