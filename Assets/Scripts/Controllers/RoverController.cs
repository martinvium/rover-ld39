using System.Collections;
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

  void ShowTileHoverBox(int x, int y) {
    if(hoverBoxGo == null) {
      hoverBoxGo = new GameObject();
      hoverBoxGo.name = "HoverBox";
      SpriteRenderer renderer = hoverBoxGo.AddComponent<SpriteRenderer>();
      renderer.sortingLayerName = "Units";
      renderer.sprite = spriteManager.Get("Tiles", "Tiles_3");
    }

    if(IsValidDestination(x, y)) {
      hoverBoxGo.SetActive(true);
    } else {
      hoverBoxGo.SetActive(false);
      return;
    }

    hoverBoxGo.transform.position = new Vector3(x, y, 0);
  }

  void MoveRover(int x, int y) {
    if(!IsValidDestination(x, y)) {
      return;
    }

    if(!rover.Move(x, y)) {
      Debug.Log("Not enough power to move!");
    }

    roverGo.transform.position = new Vector3(rover.X, rover.Y, 0);
  }

  public void CreateRover() {
    roverGo = GameObject.Instantiate(roverPrefab);
    roverGo.transform.position = new Vector3(rover.X, rover.Y, 0);
    roverGo.transform.localScale = new Vector3(1, 1, 0);

    SpriteRenderer renderer = roverGo.AddComponent<SpriteRenderer>();
    renderer.sortingLayerName = "Units";
    renderer.sprite = spriteManager.Get("Units", "Rover");
  }

  bool IsValidDestination(int mouseX, int mouseY) {
    if(mouseX == rover.X) {
      if(mouseY -1 == rover.Y) {
        return true;
      } else if(mouseY + 1 == rover.Y) {
        return true;
      }
    } else if(mouseY == rover.Y) {
      if(mouseX -1 == rover.X) {
        return true;
      } else if(mouseX + 1 == rover.X) {
        return true;
      }
    }

    return false;
  }
}
