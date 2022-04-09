using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class interfaces with the camera controller and triggers the player controller
/// This component is on all target tile objects
/// </summary>

public class TargetHover : MonoBehaviour {

    //refrences
    PlayerController playerConrtoller;  
    SpriteRenderer renderer;                
    //local cooridnate
    public Vector2 coord;

    [SerializeField] Sprite[] sprites;                                  //array of sprites to toggle between
    int index;

    //get reference and set default state
    private void Start() {
        if (PlayerController.instance != null) {
            playerConrtoller = PlayerController.instance;
        }

        renderer = gameObject.GetComponent<SpriteRenderer>();
        index = 1;
        
    }

    //functions to toggle sprite states
    private void Update() {
        renderer.sprite = sprites[index];
    }

    public void OnMouseOver() {
        index = 0;
    }

    public void OnMouseExit() {
        index = 1;
    }

    // trigger player controller from camera controller
    public void OnClicked() {
        playerConrtoller.TargetSelected(coord);

    }

}
