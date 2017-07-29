using UnityEngine;
using System.Collections.Generic;

public class SpriteManager {

	Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	public void LoadAll() {
		LoadSpriteCategory("Tiles");
		LoadSpriteCategory("Units");
	}

	public Sprite Get(string category, string name) {
		string key = category + "/" + name;

		if (sprites.ContainsKey(key) == false) {
			Debug.LogError("Could not find sprite: " + key);
			return null;
		}

		return sprites[key];
	}

	void LoadSpriteCategory(string category) {
		Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/" + category);

		Debug.Log("loadedSprites(" + category + "): " + loadedSprites.Length);

		foreach (Sprite sprite in loadedSprites) {
			string key = category + "/" + sprite.name;
			sprites.Add(key, sprite);
			Debug.Log("loaded: " + key);
		}
	}
}
