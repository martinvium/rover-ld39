using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class InputController {

  const int LeftMouseButton = 0;

  Camera camera;
  Vector3 currentMousePosition;

  public Action<Vector3> HoverTile;
  public Action<Vector3> ClickTile;

  public InputController(Camera camera) {
    this.camera = camera;
  }

  public void Update() {
		currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

    var currentTilePosition = new Vector3(
      Mathf.FloorToInt(currentMousePosition.x),
      Mathf.CeilToInt(currentMousePosition.y)
    );

    /* HoverTile(currentMousePosition); */

    if(Input.GetMouseButtonDown(LeftMouseButton)) {
      ClickTile(currentTilePosition);
    }
  }
}
