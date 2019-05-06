using System.Collections;
using System.Linq;
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
	public string team;
	public int AI;
	public int PathEndx;
	public int PathEndz;
}

public class SaveLoadCivilizaton : MonoBehaviour {

	public GameObject mountain2;
	public GameObject grass2;
	public GameObject water2;
	public GameObject deepWater2;
	public GameObject beach2;
	public GameObject copper2;
	public GameObject iron2;
	public GameObject unobtainium2;
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
	public GameObject text3;
	public GameObject text4;
	public bool isSaving;
	string saveName;
	public string loadName;
	string deleteName;
	GameObject instantiated;

	public void Save () {
		saveName = GetComponent<ManagerCivilization> ().gameName;
		json_data = "";
		gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in gameObjects) {
			if(CurrentObject.name != "Spot Light(Clone)" && CurrentObject.name != "Cylinder" && CurrentObject.name != "Grass(Clone)" && CurrentObject.name != "Water(Clone)" && CurrentObject.name != "Quad" && CurrentObject.name != "Border(Clone)" && CurrentObject.name != "Mountain(Clone)" && CurrentObject.name != "Deep Water(Clone)" && CurrentObject.name != "Beach(Clone)"  && CurrentObject.name != "Unobtainium(Clone)" && CurrentObject.name != "Copper(Clone)" && CurrentObject.name != "Iron(Clone)" && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light"  && CurrentObject.name != "Main Camera"  && CurrentObject.name != "CameraRotator" && CurrentObject.name != "InfoStorage" && CurrentObject.layer != 5 && CurrentObject.layer != 9){
				gameState.x = CurrentObject.transform.position.x;
				gameState.y = CurrentObject.transform.position.y;
				gameState.z = CurrentObject.transform.position.z;
				gameState.name = CurrentObject.name;
				if (CurrentObject.CompareTag ("City")) {
					gameState.cityName = CurrentObject.transform.GetChild (0).gameObject.GetComponent<TextMesh> ().text;
				}
				if(CurrentObject.CompareTag("City")){
					gameState.team = CurrentObject.GetComponent<CityCivilization>().team;
				}
				if(CurrentObject.CompareTag("Unit")){
					gameState.team = CurrentObject.GetComponent<Unit> ().team;
					if(CurrentObject.GetComponent<Unit> ().currentPath.Count > 0){
						gameState.PathEndx = (CurrentObject.GetComponent<Unit> ().currentPath.Last().x * 10) - 500;
						gameState.PathEndz = (CurrentObject.GetComponent<Unit> ().currentPath.Last().z * 10) - 500;
					}
					gameState.AI = CurrentObject.GetComponent<Unit> ().AIStyle;
				}
				if(CurrentObject.CompareTag("AI")){
					gameState.team = CurrentObject.GetComponent<AIManager> ().team;
				}
				if(CurrentObject.tag != "Unit" && CurrentObject.tag != "City" && CurrentObject.tag != "AI"){
					gameState.team = "";
				}
				if (CurrentObject.tag != "City") {
					gameState.cityName = "";
				}
				if(CurrentObject.tag == "Unit" && CurrentObject.tag == "AI"){
					gameState.AI = CurrentObject.GetComponent<Unit>().AIStyle;
				}
				json_data = json_data + "|" + JsonUtility.ToJson(gameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/~Civilization." + saveName, GetComponent<ManagerCivilization>().offset + "/" + GameObject.Find("Resources").GetComponent<ResourceCounter>().gold.ToString() + "/" + json_data);
	}

	public void Load (){
		if (GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().inGameName == "") {
			GetComponent<ManagerCivilization> ().gameName = text.transform.GetChild (1).transform.GetChild (2).GetComponent<Text> ().text;
		} else {
			GetComponent<ManagerCivilization> ().gameName = GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().inGameName;
		}
		loadName = GetComponent<ManagerCivilization> ().gameName;
		GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().inGameName = loadName;
		if (File.Exists (Application.persistentDataPath + "/~Civilization." + loadName) == true) {
			Destroy (text);
			Destroy (text2);
			Destroy (text3);
			Destroy (text4);
			foreach (GameObject name in GetComponent<ManagerCivilization>().texts) {
				Destroy (name);
			}
			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			GameObject.Find ("NextStage").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			GameObject.Find ("CurrentStage").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			GameObject.Find ("Mini Map").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			GameObject.Find ("Resources").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			data = File.ReadAllText (Application.persistentDataPath + "/~Civilization." + loadName);
			offset2 = float.Parse (data.Split ('/') [0]);
			GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold = int.Parse (data.Split ('/') [1]);
			GameObject.Find ("Resources").GetComponent<Text>().text = ("Gold:" + int.Parse (data.Split ('/') [1]) + ", Iron:" + 0 + ", Copper:" + 0 + ", Unobtainium:" + 0);
			GetComponent<ManagerCivilization>().offset = offset2;
			Random.InitState (Mathf.CeilToInt(offset2));
			x = -xAmount2 * 10;
			z = -zAmount2 * 10;
			while (x < (xAmount2 * 10) + 10) {
				while (z < (zAmount2 * 10) + 10) {
					if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.4575f) {
						Instantiate (deepWater2, new Vector3 (x, -1f, z), Quaternion.identity);
					} else {
						if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.5f) {
							Instantiate (water2, new Vector3 (x, -1f, z), Quaternion.identity);
						} else {
							if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.5275f) {
								Instantiate (beach2, new Vector3 (x, -4.5f, z), Quaternion.identity);
							} else {
								if (Mathf.PerlinNoise ((offset2 + ((x + (float)(-xAmount2 * 10)) / (5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10)) / (5f * zAmount2)))) < 0.825f) {
									Instantiate (grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
									if (Random.Range (1, 301) == 1) {
										Instantiate (unobtainium2, new Vector3 (x, 11, z), Quaternion.identity);
									} else {
										if(Random.Range(1, 151) == 1){
											Instantiate (copper2, new Vector3 (x, -0.75f, z), Quaternion.identity);
										} else {
											if(Random.Range(1, 76) == 1){
												Instantiate (iron2, new Vector3 (x, -0.75f, z), Quaternion.identity);
											}
										}
									}
								} else {
									if(Random.Range(0f, 1f) < 0.2f){
										Instantiate (mountain2, new Vector3 (x, -0.51f, z), Quaternion.identity);
										Instantiate (grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
									}else{
										Instantiate (grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
									}
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
			while ((data.Split ('/') [2]).Split ('|').Length > i) {
				instantiated = Instantiate (Resources.Load (JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).name.Split ('(') [0]), new Vector3 (float.Parse ((data.Split ('/') [2]).Split ('|') [i].Split (':') [1].Split (',') [0]), float.Parse ((data.Split ('/') [2]).Split ('|') [i].Split (':') [2].Split (',') [0]), float.Parse ((data.Split ('/') [2]).Split ('|') [i].Split (':') [3].Split (',') [0])), Quaternion.identity) as GameObject;
				if (instantiated.name == "City(Clone)") {
					instantiated.GetComponent<CityCivilization>().Name = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).cityName;
					instantiated.transform.GetChild (0).gameObject.GetComponent<CityNameText> ().overideName = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).cityName;
					instantiated.GetComponent<CityCivilization> ().team = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).team;
				}
				if (instantiated.tag == "Unit") {
					instantiated.GetComponent<Unit> ().team = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).team;
					instantiated.GetComponent<Unit> ().currentPath = new List<Node>();
					instantiated.GetComponent<Unit> ().OverrideX = (JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).PathEndx);
					instantiated.GetComponent<Unit> ().OverrideZ = (JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).PathEndz);
					instantiated.GetComponent<Unit> ().AIStyle = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).AI;
				}
				if (instantiated.tag == "AI") {
					instantiated.GetComponent<AIManager> ().team = JsonUtility.FromJson<GameState> ((data.Split ('/') [2]).Split ('|') [i]).team;
				}
				i++;
			}
			GetComponent<ManagerCivilization> ().createGraph();
		}
		foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
			if(City.GetComponent<CityCivilization>().team == "Player"){
				Camera.main.transform.position = new Vector3 (City.transform.position.x - 75, 100, City.transform.position.z);
				break;
			}
		}
		GameObject.Find ("Resources").GetComponent<ResourceCounter> ().Check ();
	}

	public void Delete (){
		deleteName = GetComponent<ManagerCivilization> ().text.transform.GetChild (1).transform.GetChild(2).GetComponent<Text>().text;
		File.Delete (Application.persistentDataPath + "/~Civilization." + deleteName);
		foreach(string filePath in System.IO.Directory.GetFiles(Application.persistentDataPath)){
			if(filePath.Split ('.')[filePath.Split ('.').Length - 2] == deleteName){
				File.Delete (filePath);
			}
		}
		GetComponent<ManagerCivilization>().SpawnGameNames ();
	}
}