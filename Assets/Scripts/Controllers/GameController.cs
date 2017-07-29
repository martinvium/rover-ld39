using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  GameObject mapTilesGo;
  public GameObject roverPrefab;
  Text powerLabel;
  Rover rover;

  SpriteManager spriteManager;
  DataManager dataManager;
  InputController inputController;
  RoverController roverController;

	// Use this for initialization
	void Start () {
    powerLabel = GameObject.Find("PowerLabel").GetComponent<Text>();

    mapTilesGo = new GameObject();
    mapTilesGo.name = "MapTiles";

    spriteManager = new SpriteManager();
    spriteManager.LoadAll();

    dataManager = DataManager.Instance();

    CreateTileMap();

    inputController = new InputController(Camera.main);

    rover = new Rover(
      Mathf.FloorToInt(dataManager.tileMap.tileswide / 2),
      Mathf.FloorToInt(dataManager.tileMap.tileshigh / 2) * -1
    );

    rover.CurrentPowerChanged += UpdatePowerLabel;

    roverController = new RoverController(spriteManager, roverPrefab, rover);
    roverController.RegisterActionCallbacks(inputController);
    roverController.CreateRover();
	}
	
	void Update () {
    inputController.Update();
	}

  void UpdatePowerLabel(int currentPower) {
    powerLabel.text = "POWER: " + currentPower.ToString();
    if(currentPower < 1) {
      powerLabel.color = Color.red;
    }
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
