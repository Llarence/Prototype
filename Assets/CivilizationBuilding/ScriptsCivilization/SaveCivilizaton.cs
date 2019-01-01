using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class gameState {
	public float x;
	public float y;
	public float z;
	public string Name;
	public float Offset;
}

public class SaveCivilizaton : MonoBehaviour {

	string Path;
	GameObject[] GameObjects;
	public float[] Position;
	FileStream Stream;
	gameState GameState = new gameState();
	string json_data = "";
	string Data;

	public void Save (string SaveName) {
		Path = Application.persistentDataPath + "/Player.Save";
		GameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in GameObjects) {
			if(CurrentObject.name != "Grass(Clone)" && CurrentObject.name != "Water(Clone)" && CurrentObject.name != "Mountain(Clone)" && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light" && CurrentObject.tag != "Unit" && CurrentObject.layer != 5){
				GameState.x = CurrentObject.transform.position.x;
				GameState.y = CurrentObject.transform.position.y;
				GameState.z = CurrentObject.transform.position.z;
				GameState.Name = CurrentObject.name;
				json_data = json_data + JsonUtility.ToJson(GameState);
			}
		}
		print ("starting write a");
		File.WriteAllText (Path, JsonUtility.ToJson(GameState.Offset) + json_data);
		print ("done write");
		Load ();
	}

	public void Load (){
		Data = File.ReadAllText (Path);
		print (Data);
	}
}
