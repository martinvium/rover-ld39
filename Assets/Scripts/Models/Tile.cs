using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile {
  public int y;
  public int tile;
  public int rot;
  public int index;
  public int flipX;
  public int x;

  public int Soil = 1;

  public int SpriteIndex() {
    return tile;
  }

  public string Type() {
      return "Tiles_" + SpriteIndex();
  }
}
