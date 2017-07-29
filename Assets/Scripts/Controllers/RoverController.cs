﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController {

  Rover rover;
  GameObject roverPrefab;
  GameObject roverGo;
  GameObject hoverBoxGo;

  SpriteManager spriteManager;

  public RoverController(SpriteManager spriteManager, GameObject roverPrefab, Rover rover) {
    this.spriteManager = spriteManager;
    this.roverPrefab = roverPrefab;
    this.rover = rover;
  }

  public void RegisterActionCallbacks(InputController inputController) {
    inputController.HoverTile += ShowTileHoverBox;
    inputController.ClickTile += MoveRover;
  }

  public Vector3 Position() {
    return roverGo.transform.position;
  }

  void ShowTileHoverBox(Vector3 currentTilePosition) {
    if(hoverBoxGo == null) {
      hoverBoxGo = new GameObject();
      hoverBoxGo.name = "HoverBox";
      SpriteRenderer renderer = hoverBoxGo.AddComponent<SpriteRenderer>();
      renderer.sortingLayerName = "Units";
      renderer.sprite = spriteManager.Get("Tiles", "Tiles_3");
    }

    if(IsValidDestination(currentTilePosition)) {
      hoverBoxGo.SetActive(true);
    } else {
      hoverBoxGo.SetActive(false);
      return;
    }

    hoverBoxGo.transform.position = currentTilePosition;
  }

  void MoveRover(Vector3 currentTilePosition) {
    if(!IsValidDestination(currentTilePosition)) {
      return;
    }

    if(!rover.Move(currentTilePosition)) {
      Debug.Log("Not enough power to move!");
    }

    roverGo.transform.position = rover.Position;
  }

  public void CreateRover() {
    roverGo = GameObject.Instantiate(roverPrefab);
    roverGo.transform.position = rover.Position;
    roverGo.transform.localScale = new Vector3(1, 1, 0);

    SpriteRenderer renderer = roverGo.AddComponent<SpriteRenderer>();
    renderer.sortingLayerName = "Units";
    renderer.sprite = spriteManager.Get("Units", "Rover");
  }

  bool IsValidDestination(Vector3 currentTilePosition) {
    Vector3 roverPos = Position();

    int roverX = Mathf.FloorToInt(roverPos.x);
    int roverY = Mathf.FloorToInt(roverPos.y);

    int mouseX = Mathf.FloorToInt(currentTilePosition.x);
    int mouseY = Mathf.FloorToInt(currentTilePosition.y);

    if(mouseX == roverX) {
      if(mouseY -1 == roverY) {
        return true;
      } else if(mouseY + 1 == roverY) {
        return true;
      }
    } else if(mouseY == roverY) {
      if(mouseX -1 == roverX) {
        return true;
      } else if(mouseX + 1 == roverX) {
        return true;
      }
    }

    return false;
  }


}
