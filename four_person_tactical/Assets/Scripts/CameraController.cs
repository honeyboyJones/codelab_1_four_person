using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple class for raycasting clicks onto the interactable colliders
public class CameraController : MonoBehaviour {

	Camera cam;
	public LayerMask interactionMask;

	void Start() {
		if(cam == null) {
			if (GetComponent<Camera>() != null) {
				cam = GetComponent<Camera>();
			}
		}
	}

	void Update() {
	//Check for clicks, raycast, execute TargetHover class function
	    if(Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider != null) {
				hit.collider.GetComponent<TargetHover>().OnClicked();
			}
		}
	}

}
