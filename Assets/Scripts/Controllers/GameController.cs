using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  GameObject mapTilesGo;

  SpriteManager spriteManager;
  DataManager dataManager;
  InputController inputController;
  RoverController roverController;

	// Use this for initialization
	void Start () {
    mapTilesGo = new GameObject();
    mapTilesGo.name = "MapTiles";

    spriteManager = new SpriteManager();
    spriteManager.LoadAll();

    dataManager = DataManager.Instance();

    CreateTileMap();

    inputController = new InputController(Camera.main);

    roverController = new RoverController(spriteManager);
    roverController.RegisterActionCallbacks(inputController);
    roverController.CreateRover();
	}
	
	void Update () {
    inputController.Update();
	}

  void CreateTileMap() {
    TileLayer layer = dataManager.tileMap.layers[0];

    foreach(Tile tile in layer.tiles) {
      GameObject go = new GameObject();
      go.name = tile.Type() + "_" + tile.x + "_" + tile.y;
      go.transform.SetParent(mapTilesGo.transform);
      go.transform.position = new Vector3(tile.x * 1, tile.y * 1 * -1, 0);
      go.transform.localScale = new Vector3(1, 1, 0);

      SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
      renderer.sortingLayerName = "Tiles";
      renderer.sprite = spriteManager.Get("Tiles", tile.Type());
    }
  }
}
