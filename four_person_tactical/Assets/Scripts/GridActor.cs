using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActor : MonoBehaviour {

    GridManager gridManager;
    public Coordinate coord = new Coordinate();

    [SerializeField] float moveSpeed = 3;
    [SerializeField] float actionDelay = 0.25f;

    void Start() { 
        if (GridManager.instance != null) {
            gridManager = GridManager.instance;
        }
    }

    public void UpdateGridActorCoord() { 
        
    }

    public List<Coordinate> CalculateAdjacentCoords(int range) {
        List<Coordinate> adjacentCoords = new List<Coordinate>();

        Vector2 coordOffset = coord.coordinate - new Vector2(range, range);
       
        for (int r = 1; r <= range; r++) {
            Vector2 offset = new Vector2(coord.coordinate.x - r, coord.coordinate.y);
            Coordinate findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            offset = new Vector2(coord.coordinate.x + r, coord.coordinate.y);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            offset = new Vector2(coord.coordinate.x, coord.coordinate.y - r);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null) adjacentCoords.Add(findAdjacent);
            
            offset = new Vector2(coord.coordinate.x, coord.coordinate.y + r);
            findAdjacent = gridManager.coords.Find(x => x.coordinate == offset);
            if (findAdjacent != null)  adjacentCoords.Add(findAdjacent);
            
        }

        return adjacentCoords;
    }

    public IEnumerator MoveToCoord(Vector2 targetCoord) {
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

        Coordinate nextCoordinate = gridManager.coords.Find(x => x.coordinate == targetCoord);
        transform.position = nextCoordinate.worldPosition;
        coord = nextCoordinate;

        yield return null;
    }

}
