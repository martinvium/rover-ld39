using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  GameObject mapTilesGo;
  public GameObject roverPrefab;
  Text powerLabel;
  World world;

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

    world = new World(dataManager);
    world.LoadTiles();
    world.CreateRover();

    CreateTileMap();

    inputController = new InputController(Camera.main);

    world.Rover.CurrentPowerChanged += UpdatePowerLabel;

    roverController = new RoverController(spriteManager, roverPrefab, world.Rover);
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
    for(int x = 0; x < world.Tiles.GetLength(0); x++) {
      for(int y = 0; y < world.Tiles.GetLength(1); y++) {
        Tile tile = world.Tiles[x, y];

        GameObject go = new GameObject();
        go.name = tile.Type() + "_" + tile.x + "_" + tile.y;
        go.transform.SetParent(mapTilesGo.transform);
        go.transform.position = new Vector3(tile.x, tile.y, 0);
        go.transform.localScale = new Vector3(1, 1, 0);

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sortingLayerName = "Tiles";
        renderer.sprite = spriteManager.Get("Tiles", tile.Type());
      }
    }
  }

  public void SoilSampleClick() {
    Tile tile = GetTileUnderRover();

    if(tile.Soil > 0) {
      world.Rover.GetSoilSample();
      tile.Soil = 0;
    } else {
      Debug.Log("No soil left to sample!");
    }
  }

  Tile GetTileUnderRover() {
    return world.Tiles[world.Rover.X, world.Rover.Y];
  }
}
