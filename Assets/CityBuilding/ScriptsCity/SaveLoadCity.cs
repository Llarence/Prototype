﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class gameState2 {
	public float x2;
	public float y2;
	public float z2;
	public string Name2;
}

public class SaveLoadCity : MonoBehaviour {

	GameObject[] GameObjects;
	public float[] Position;
	FileStream Stream;
	gameState2 GameState = new gameState2();
	string json_data;
	string Data;
	int x;
	int z;
	float Gold;
	int I = 1;

	public void Save2 () {
		json_data = "";
		GameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in GameObjects) {
			if(CurrentObject.name != "Ground" && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light" && CurrentObject.name != "Quad" && CurrentObject.name != "MainCamera" && CurrentObject.name != "InfoStorage" && CurrentObject.tag != "Person" && CurrentObject.layer!= 5 && CurrentObject.layer != 2){
				GameState.x2 = CurrentObject.transform.position.x;
				GameState.y2 = CurrentObject.transform.position.y;
				GameState.z2 = CurrentObject.transform.position.z;
				GameState.Name2 = CurrentObject.name;
				json_data = json_data + "|" + JsonUtility.ToJson(GameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/~Player." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().inGameName + "." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().cityName, GetComponent<ManagerCity>().Gold + "/" + GetComponent<ManagerCity>().Food + "/" + GetComponent<ManagerCity>().Population + "/" + GetComponent<ManagerCity>().Farms.Length + "/" + GetComponent<ManagerCity>().Houses.Length + "/" + GetComponent<ManagerCity>().GoldMines.Length + "/" + GetComponent<ManagerCity>().Storages.Length + "/" + GetComponent<ManagerCity>().Seas.Length + json_data);
	}

	public void Load2 (){
		if(File.Exists (Application.persistentDataPath + "/~Player." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().inGameName + "." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().cityName)){
			Data = File.ReadAllText (Application.persistentDataPath + "/~Player." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().inGameName + "." + GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().cityName);
			GetComponent<ManagerCity>().Gold = int.Parse (Data.Split ('/') [0]);
			GetComponent<ManagerCity>().Food = int.Parse (Data.Split ('/') [1]);
			GetComponent<ManagerCity>().Population = int.Parse (Data.Split ('/') [2]);
			while ((Data.Split ('/') [7]).Split ('|').Length > I){
				Instantiate((Resources.Load(JsonUtility.FromJson<gameState2> ((Data.Split ('/') [7]).Split ('|') [I]).Name2.Split ('(') [0])), new Vector3(float.Parse ((Data.Split ('/') [7]).Split ('|') [I].Split (':') [1].Split (',') [0]), float.Parse ((Data.Split ('/') [7]).Split ('|') [I].Split (':') [2].Split (',') [0]), float.Parse ((Data.Split ('/') [7]).Split ('|') [I].Split (':') [3].Split (',') [0])), Quaternion.identity);
				I++;
			}
		}else{
			GetComponent<ManagerCity>().Gold = 15;
		}
	}
}