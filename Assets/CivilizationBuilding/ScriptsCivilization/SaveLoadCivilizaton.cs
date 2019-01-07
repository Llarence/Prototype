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
	public string Name;
}

public class SaveLoadCivilizaton : MonoBehaviour {

	public GameObject mountain2;
	public GameObject grass2;
	public GameObject water2;
	public GameObject beach2;
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
	public GameObject text;
	public GameObject text2;
	public bool isSaving;
	string saveName;
	string loadName;

	public void Save () {
		saveName = GetComponent<ManagerCivilization> ().GameName;
		json_data = "";
		gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in gameObjects) {
			if(CurrentObject.name != "Grass(Clone)" && CurrentObject.name != "Water(Clone)" && CurrentObject.name != "Mountain(Clone)" && CurrentObject.name != "Beach(Clone)"  && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light"  && CurrentObject.name != "Main Camera" && CurrentObject.layer != 5){
				gameState.x = CurrentObject.transform.position.x;
				gameState.y = CurrentObject.transform.position.y;
				gameState.z = CurrentObject.transform.position.z;
				gameState.Name = CurrentObject.name;
				json_data = json_data + "|" + JsonUtility.ToJson(gameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/~Civilization." + saveName, GetComponent<ManagerCivilization>().offset + "/" + json_data);
	}

	public void Load (){
		GetComponent<ManagerCivilization> ().GameName = text.transform.GetChild (1).transform.GetChild(2).GetComponent<Text>().text;
		loadName = GetComponent<ManagerCivilization> ().GameName;
		Destroy (text);
		Destroy (text2);
		foreach(GameObject name in GetComponent<ManagerCivilization>().texts){
			Destroy (name);
		}
		GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
		GameObject.Find ("NextTurn").GetComponent<RectTransform> ().Rotate(0, -90, 0);
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
							Instantiate (mountain2, new Vector3 (x, -0.5f, z), Quaternion.identity);
						}

					}
				}
				z += 10;
			}
			z = -xAmount2 * 10;
			x += 10;
		}
		i = 1;
		while ((data.Split ('/') [1]).Split('|')[i] != null){
			Instantiate(Resources.Load(JsonUtility.FromJson<GameState>((data.Split ('/') [1]).Split('|')[i]).Name.Split ('(') [0]), new Vector3(float.Parse ((data.Split ('/') [1]).Split('|')[i].Split(':')[1].Split(',')[0]), float.Parse ((data.Split ('/') [1]).Split('|')[i].Split(':')[2].Split(',')[0]), float.Parse ((data.Split ('/') [1]).Split('|')[i].Split(':')[3].Split(',')[0])), Quaternion.identity);
			i++;
		}
	}
}