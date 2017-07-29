using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController {

  SpriteManager spriteManager;
  GameObject go;

  public RoverController(SpriteManager spriteManager) {
    this.spriteManager = spriteManager;
  }

  public void RegisterActionCallbacks(InputController inputController) {
    inputController.ClickTile += MoveRover;
  }

  void MoveRover(Vector3 currentTilePosition) {
    Debug.Log(currentTilePosition);
    go.transform.position = currentTilePosition;
  }

  public void CreateRover() {
    go = new GameObject();
    go.name = "Rover";
    go.transform.position = new Vector3(0, 0, 0);
    go.transform.localScale = new Vector3(1, 1, 0);

    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
    renderer.sortingLayerName = "Units";
    renderer.sprite = spriteManager.Get("Units", "Rover");
  }
}
