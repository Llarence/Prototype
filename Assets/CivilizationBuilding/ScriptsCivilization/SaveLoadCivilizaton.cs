using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameState {
	public float x;
	public float y;
	public float z;
	public string name;
	public string cityName;
}

public class SaveLoadCivilizaton : MonoBehaviour {

	public GameObject mountain2;
	public GameObject grass2;
	public GameObject water2;
	public GameObject beach2;
	public GameObject city2;
	GameObject[] gameObjects;
	public float[] position;
	FileStream stream;
	GameState gameState = new GameState();
	string json_data;
	string data;
	int x;
	int z;
	public int xAmount2;
	public int zAmount2;
	float offset2;
	int i;
	GameObject justSpawned;
	public GameObject text;
	public GameObject text2;
	public GameObject text3;
	public bool isSaving;
	string saveName;
	string loadName;
	string deleteName;

	public void Save () {
		saveName = GetComponent<ManagerCivilization> ().GameName;
		json_data = "";
		gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject currentObject in gameObjects) {
			if(currentObject.name != "Grass(Clone)" && currentObject.name != "Water(Clone)" && currentObject.name != "Mountain(Clone)" && currentObject.name != "Beach(Clone)"  && currentObject.name != "EventSystem" && currentObject.name != "Manager" && currentObject.name != "Directional Light"  && currentObject.name != "Main Camera" && currentObject.layer != 5){
				gameState.x = currentObject.transform.position.x;
				gameState.y = currentObject.transform.position.y;
				gameState.z = currentObject.transform.position.z;
				gameState.name = currentObject.name;
				if(gameState.name == "City(Clone)"){
					gameState.cityName = currentObject.transform.GetChild (0).GetComponent<TextMesh>().text;
				}else{
					gameState.cityName = "";
				}
				json_data = json_data + "|" + JsonUtility.ToJson(gameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/~Civilization." + saveName, GetComponent<ManagerCivilization>().offset + "/" + json_data);
	}

	public void Load (){
		GetComponent<ManagerCivilization> ().GameName = text.transform.GetChild (1).transform.GetChild (2).GetComponent<Text> ().text;
		loadName = GetComponent<ManagerCivilization> ().GameName;
		if (File.Exists (Application.persistentDataPath + "/~Civilization." + loadName) == true) {
			Destroy (text);
			Destroy (text2);
			Destroy (text3);
			foreach (GameObject name in GetComponent<ManagerCivilization>().texts) {
				Destroy (name);
			}
			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			GameObject.Find ("NextTurn").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			data = File.ReadAllText (Application.persistentDataPath + "/~Civilization." + loadName);
			offset2 = float.Parse (data.Split ('/') [0]);
			x = -xAmount2 * 10;
			z = -zAmount2 * 10;
			while (x < (xAmount2 * 10) + 10) {
				while (z < (zAmount2 * 10) + 10) {
					if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.5f) {
						Instantiate (water2, new Vector3 (x, -1f, z), Quaternion.identity);
					} else {
						if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.5275f) {
							Instantiate (beach2, new Vector3 (x, -4.5f, z), Quaternion.identity);
						} else {
							if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.825f) {
								Instantiate (grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
							} else {
								if(Mathf.PerlinNoise (offset2 + (zAmount2 + xAmount2) * 10, 0) >= 0.5f * offset2 + (zAmount2 + xAmount2) * 10){
									Instantiate (mountain2, new Vector3 (x, -0.5f, z), Quaternion.identity);
								}else{
									Instantiate (grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
								}
							}
						}
					}
					z += 10;
				}
				z = -xAmount2 * 10;
				x += 10;
			}
			i = 1;
			while ((data.Split ('/') [1]).Split ('|') [i] != null) {
				if (((data.Split ('/') [1]).Split ('|') [i]).Split ('"') [13] != "") {
					justSpawned = Instantiate (city2, new Vector3 (float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [1].Split (',') [0]), float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [2].Split (',') [0]), float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [3].Split (',') [0])), Quaternion.identity);
					justSpawned.transform.GetChild (0).GetComponent<CityNameText> ().OverideName = ((data.Split ('/') [1]).Split ('|') [i]).Split ('"') [13];
				} else {
					Instantiate (Resources.Load (JsonUtility.FromJson<GameState> ((data.Split ('/') [1]).Split ('|') [i]).name.Split ('(') [0]), new Vector3 (float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [1].Split (',') [0]), float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [2].Split (',') [0]), float.Parse ((data.Split ('/') [1]).Split ('|') [i].Split (':') [3].Split (',') [0])), Quaternion.identity);
				}
				i++;
			}
		}
	}

	public void Delete (){
		deleteName = GetComponent<ManagerCivilization> ().Text.transform.GetChild (1).transform.GetChild(2).GetComponent<Text>().text;
		File.Delete (Application.persistentDataPath + "/~Civilization." + deleteName);
		GetComponent<ManagerCivilization>().SpawnGameNames ();
	}
}