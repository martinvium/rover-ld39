using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  GameObject mapTilesGo;
  public GameObject roverPrefab;

  Text powerLabel;
  Text pendingSamplesLabel;
  Text missionStatusLabel;
  Text hoursUntilDuskLabel;

  World world;

  SpriteManager spriteManager;
  DataManager dataManager;
  InputController inputController;
  RoverController roverController;

	// Use this for initialization
	void Start () {
    powerLabel = GameObject.Find("PowerLabel").GetComponent<Text>();
    pendingSamplesLabel = GameObject.Find("PendingSamplesAmount").GetComponent<Text>();
    missionStatusLabel = GameObject.Find("MissionStatusLabel").GetComponent<Text>();
    hoursUntilDuskLabel = GameObject.Find("Clock").GetComponent<Text>();

    mapTilesGo = new GameObject();
    mapTilesGo.name = "MapTiles";

    spriteManager = new SpriteManager();
    spriteManager.LoadAll();

    dataManager = DataManager.Instance();

    world = new World(dataManager);
    world.LoadTiles();
    world.CreateRover();
    world.CreateHQ();

    world.HQ.SamplesChanged += UpdateMissionStatus;
    world.DaysChanged += UpdateMissionStatus;
    world.HoursChanged += UpdateHoursUntilDusk;

    CreateTileMap();

    inputController = new InputController(Camera.main);

    world.Rover.CurrentPowerChanged += UpdatePowerLabel;
    world.Rover.PendingSamplesChanged += UpdatePendingSamplesLabel;

    roverController = new RoverController(spriteManager, roverPrefab, world.Rover);
    roverController.RegisterActionCallbacks(inputController);
    roverController.CreateRover();
	}
	
	void Update () {
    inputController.Update();
    world.Update();
	}

  void UpdatePowerLabel(int currentPower) {
    powerLabel.text = "POWER: " + currentPower.ToString();
    if(currentPower < 1) {
      powerLabel.color = Color.red;
    } else {
      powerLabel.color = Color.green;
    }
  }

  void UpdatePendingSamplesLabel(int pending) {
    pendingSamplesLabel.text = pending.ToString();
  }

  void UpdateMissionStatus() {
    missionStatusLabel.text = world.HQ.MissionStatusText();
  }

  void UpdateHoursUntilDusk() {
    hoursUntilDuskLabel.text = world.HoursUntilDusk.ToString() + " HOURS UNTIL DUSK";
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

  public void EndTurn() {
    world.NextDay();
  }

  public void TransmitAnalyzedSamples() {
    world.Rover.TransmitPendingSamples(world.HQ);
  }
}
