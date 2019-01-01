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
}

public class SaveLoadCivilizaton : MonoBehaviour {

	public GameObject Mountain;
	public GameObject Grass;
	public GameObject Water;
	string Path;
	GameObject[] GameObjects;
	public float[] Position;
	FileStream Stream;
	gameState GameState = new gameState();
	string json_data = "";
	string Data;
	int x;
	int z;
	public int xAmount2;
	public int zAmount2;
	float offset2;

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
		File.WriteAllText (Path, GetComponent<ManagerCivilization>().offset + json_data);
		print ("done write");
	}

	public void Load (){
		Data = File.ReadAllText (Path);
		x = -xAmount2 * 10;
		z = -zAmount2 * 10;
		while (x < (xAmount2 * 10) + 10) {
			while (z < (zAmount2 * 10) + 10) {
				if (Mathf.PerlinNoise((offset2 + ((x + (float)(-xAmount2 * 10))/(5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10))/(5f * zAmount2)))) < 0.5f) {
					Instantiate (Water, new Vector3 (x, -1f, z), Quaternion.identity);
				} else {
					if(Mathf.PerlinNoise((offset2 + ((x + (float)(-xAmount2 * 10))/(5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10))/(5f * zAmount2)))) < 0.825f){
						Instantiate (Grass, new Vector3 (x, -4.5f, z), Quaternion.identity);
					} else {
						Instantiate (Mountain, new Vector3 (x, -0.5f, z), Quaternion.identity);
					}

				}
				z += 10;
			}
			z = -xAmount2 * 10;
			x += 10;
		}
		print (Data);
	}
}
