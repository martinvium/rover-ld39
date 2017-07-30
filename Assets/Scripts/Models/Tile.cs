using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile {
  public const int RUBLE = 0;
  public const int ROCKS = 1;
  public const int EMPTY = 2;
  public const int EXPOSED = 4;

  public int y;
  public int tile;
  public int rot;
  public int index;
  public int flipX;
  public int x;

  public int Samples;

  public int SpriteIndex() {
    return tile;
  }

  public string Type() {
      return "Tiles_" + SpriteIndex();
  }

  public void GenerateSoil() {
    switch(tile) {
      case RUBLE:
        Samples = Random.Range(0, 50);
        break;
      case ROCKS:
        Samples = Random.Range(30, 80);
        break;
      case EXPOSED:
        Samples = Random.Range(60, 100);
        break;
    }
  }
}
