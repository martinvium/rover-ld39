using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class World {

  DataManager dataManager;

  public Tile[,] Tiles;
  public Rover Rover;
  public HQ HQ;
  public int Days { get; private set; }

  public Action DaysChanged;

  public World(DataManager dataManager) {
    this.dataManager = dataManager;
    Days = 0;
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
  }
}
