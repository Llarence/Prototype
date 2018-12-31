using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class gameState {
	public float x;
	public float y;
	public float z;
}


public class SaveCivilizaton : MonoBehaviour {

	string Path;
	GameObject[] GameObjects;
	public float[] Position;
	FileStream Stream;
	gameState GameState = new gameState();
	string json_data = "";

	public void Save (string SaveName) {
		Path = Application.persistentDataPath + "/Player.Save";
		GameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in GameObjects) {
			//GameState.x = CurrentObject.transform.position.x;
			//GameState.y = CurrentObject.transform.position.y;
			//GameState.z = CurrentObject.transform.position.z;
			json_data = json_data + JsonUtility.ToJson(GameState);
			json_data = json_data + JsonUtility.ToJson(CurrentObject);
		}
		print ("starting write");
		File.WriteAllText (Path, json_data);
		print ("done write");
	}

}
