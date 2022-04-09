using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GridActor is stored in the GridManager and stores its local coordinate
/// All battlers require a GridActor componant
/// 
/// </summary>

public class GridActor : MonoBehaviour {

    // reference to GridManager
    GridManager gridManager;

    // local coordinate
    public Coordinate coord = new Coordinate();

    // coroutine animation variables
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float actionDelay = 0.25f;

    // set the references
    void Start() { 
        if (GridManager.instance != null) {
            gridManager = GridManager.instance;
        }
    }

    // not used yet
    public void UpdateGridActorCoord() { 
        
    }

    // returns list of coordinates adjacent to local coordinate by a factor of range
    // used by the battler class
    public List<Coordinate> CalculateAdjacentCoords(int range) {
        List<Coordinate> adjacentCoords = new List<Coordinate>();

        Vector2 coordOffset = coord.coordinate - new Vector2(range, range);
       
       // four-directional grid targeting for visualization and movement
        for (int r = 1; r <= range; r++) {
            // for left
            Vector2 offset = new Vector2(coord.coordinate.x - r, coord.coordinate.y); 
            Coordinate findAdjacent = gridManager.coords.Find(x => x.coordinate == offset); // the find constructor looks through a list and compares its contents to a control variable
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            // for right
            offset = new Vector2(coord.coordinate.x + r, coord.coordinate.y);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            // for down
            offset = new Vector2(coord.coordinate.x, coord.coordinate.y - r);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            // for up
            offset = new Vector2(coord.coordinate.x, coord.coordinate.y + r);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null)  adjacentCoords.Add(findAdjacent);
            
        }

        return adjacentCoords; // returns the full list after all the coordinates have been added
    }

    // coroutine controlling the movement
    public IEnumerator MoveToCoord(Vector2 targetCoord) {
        //coroutine animaition needing debugging later

         //bool isMoving = true;
         //while (isMoving) {

         //    int xMove = (int)Mathf.Abs(coord.coordinate.x - targetCoord.x);
         //    int xDirection = (coord.coordinate.x > targetCoord.x) ? -1 : 1;
         //    int yMove = (int)Mathf.Abs(coord.coordinate.y - targetCoord.y);
         //    int yDirection = (coord.coordinate.y > targetCoord.y) ? -1 : 1;

            
         //    for (int x = 0; x <= xMove; x++) {
         //        Vector2 nextCoord = new Vector2(coord.coordinate.x + xDirection, coord.coordinate.y);
         //        Coordinate nextCoordinate = gridManager.coords.Find(x => x.coordinate == nextCoord);
         //        while (Vector3.Distance(transform.position, nextCoordinate.worldPosition) < 0.05f) {                                  
         //            transform.position = Vector3.Lerp(transform.position, nextCoordinate.worldPosition, Time.deltaTime * moveSpeed);
         //            yield return new WaitForFixedUpdate();                   
         //        }
         //        transform.position = nextCoordinate.worldPosition;
         //        coord = nextCoordinate;
         //    }
         //    for (int y = 0; y <= yMove; y++) { 
         //        Vector2 nextCoord = new Vector2(coord.coordinate.x, coord.coordinate.y + yDirection);
         //        Coordinate nextCoordinate = gridManager.coords.Find(x => x.coordinate == nextCoord);
         //        while (Vector3.Distance(transform.position, nextCoordinate.worldPosition) < 0.05f) {                                  
         //            transform.position = Vector3.Lerp(transform.position, nextCoordinate.worldPosition, Time.deltaTime * moveSpeed);
         //            yield return new WaitForFixedUpdate();                   
         //        }
         //        transform.position = nextCoordinate.worldPosition;
         //        coord = nextCoordinate;
         //    }

         //    isMoving = false;
         //    yield return new WaitForFixedUpdate();
         //}

        // finds target coordinate and changes the transform of the battler
        Coordinate nextCoordinate = gridManager.coords.Find(x => x.coordinate == targetCoord);
        transform.position = nextCoordinate.worldPosition;
        coord = nextCoordinate;

        yield return null; // every coroutine needs this to function
    }

}
