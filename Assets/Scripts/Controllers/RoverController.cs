using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController {

  int CurrentPower = 0;
  int MaxPower = 100;

  GameObject roverGo;
  GameObject hoverBoxGo;

  SpriteManager spriteManager;

  public RoverController(SpriteManager spriteManager) {
    this.spriteManager = spriteManager;
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

    roverGo.transform.position = currentTilePosition;
  }

  public void CreateRover() {
    roverGo = new GameObject();
    roverGo.name = "Rover";
    roverGo.transform.position = new Vector3(0, 0, 0);
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
