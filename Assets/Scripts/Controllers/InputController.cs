﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class InputController {

  const int LeftMouseButton = 0;

  Camera camera;
  Vector3 currentMousePosition;

  public Action<int, int> HoverTile;
  public Action<int, int> ClickTile;

  public InputController(Camera camera) {
    this.camera = camera;
  }

  public void Update() {
		currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

    int x = Mathf.FloorToInt(currentMousePosition.x);
    int y = Mathf.CeilToInt(currentMousePosition.y);

    HoverTile(x, y);

    if(Input.GetMouseButtonDown(LeftMouseButton)) {
      ClickTile(x, y);
    }
  }
}
