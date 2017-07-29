using UnityEngine;
using System.IO;

public class DataManager {

	static DataManager instance;

  public TileMap tileMap;

	public static DataManager Instance() {
		if(instance == null) {
			instance = new DataManager();
		}

		return instance;
	}

	public DataManager() {
		LoadAll();
	}

	public void LoadAll() {
		LoadTileMap();
	}

	void LoadTileMap() {
    var filename = Path.Combine(Application.streamingAssetsPath, "TileMap.json");
    var text = File.ReadAllText(filename);
    tileMap = JsonUtility.FromJson<TileMap>(text);
	}
}
