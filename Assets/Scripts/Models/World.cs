using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class World {

  readonly int InitialHoursUntilDusk = 18;

  DataManager dataManager;

  public Tile[,] Tiles;
  public Rover Rover;
  public HQ HQ;
  public int Days { get; private set; }
  public int HoursUntilDusk { get; private set; }

  public Action DaysChanged;
  public Action HoursChanged;

  private float nextHourPass = 0.0f;
  public float nextHourPassOffset = 1f;

  public World(DataManager dataManager) {
    this.dataManager = dataManager;
    Days = 0;
    HoursUntilDusk = InitialHoursUntilDusk;
  }

  public void Update() {
    if (Time.time > nextHourPass) {
        nextHourPass += nextHourPassOffset;

        if(HoursUntilDusk > 0) {
          HoursUntilDusk -= 1;
          HoursChanged();
        } else {
          NextDay();
        }
    }
  }

  public void LoadTiles() {
    TileLayer layer = dataManager.tileMap.layers[0];
    Tiles = new Tile[dataManager.tileMap.tileswide, dataManager.tileMap.tileshigh];

    foreach(Tile t in layer.tiles) {
      Tiles[t.x, Mathf.Abs(t.y)] = t;
    }
  }

  public void CreateRover() {
    Rover = new Rover(
      Mathf.FloorToInt(dataManager.tileMap.tileswide / 2),
      Mathf.Abs(Mathf.FloorToInt(dataManager.tileMap.tileshigh / 2))
    );
  }

  public void CreateHQ() {
    HQ = new HQ(this);
  }

  public void NextDay() {
    Days += 1;
    DaysChanged();

    HoursUntilDusk = InitialHoursUntilDusk;
    HoursChanged();
    Rover.Recharge();
  }
}
