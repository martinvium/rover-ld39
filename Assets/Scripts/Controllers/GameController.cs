using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  SpriteManager spriteManager;
  DataManager dataManager;
  GameObject mapTilesGo;

	// Use this for initialization
	void Start () {
    mapTilesGo = new GameObject();
    mapTilesGo.name = "MapTiles";

    spriteManager = new SpriteManager();
    spriteManager.LoadAll();

    dataManager = DataManager.Instance();

    renderTileMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void renderTileMap() {
    TileLayer layer = dataManager.tileMap.layers[0];

    foreach(Tile tile in layer.tiles) {
      GameObject go = new GameObject();
      go.name = tile.Type() + "_" + tile.x + "_" + tile.y;
      go.transform.SetParent(mapTilesGo.transform);
      go.transform.position = new Vector3(tile.x * 2, tile.y * 2 * -1, 0);

      SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
      renderer.sortingLayerName = "Tiles";
      renderer.sprite = spriteManager.Get("Tiles", tile.Type());
    }
  }
}
