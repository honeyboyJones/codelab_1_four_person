using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHover : MonoBehaviour {

    PlayerController playerConrtoller;

    SpriteRenderer renderer;
    [SerializeField] Sprite[] sprites;
    int index;

    private void Start() {
        if (PlayerController.instance != null) {
            playerConrtoller = PlayerController.instance;
        }

        renderer = gameObject.GetComponent<SpriteRenderer>();
        index = 1;
        
    }

    private void Update() {
        renderer.sprite = sprites[index];
    }

    public void OnMouseOver() {
        index = 0;
    }

    public void OnMouseExit() {
        index = 1;
    }

    public void OnClicked() {
        playerConrtoller.TargetSelected(transform.position);
        Debug.Log("Clicked " + this.name);

    }

}
