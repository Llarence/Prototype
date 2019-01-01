using System.Collections;
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
	string json_data = "";
	string Data;
	int x;
	int z;
	float Gold;
	int I;

	public void Save2 (string SaveName) {
		GameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in GameObjects) {
			if(CurrentObject.name != "Ground" && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light"  && CurrentObject.name != "Main Camera" && CurrentObject.tag != "UI"){
				GameState.x2 = CurrentObject.transform.position.x;
				GameState.y2 = CurrentObject.transform.position.y;
				GameState.z2 = CurrentObject.transform.position.z;
				GameState.Name2 = CurrentObject.name;
				json_data = json_data + "|" + JsonUtility.ToJson(GameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/Player.SaveCity", GetComponent<ManagerCity>().Gold + "/" + GetComponent<ManagerCity>().Gold + "/" + GetComponent<ManagerCity>().Gold + "/"  + json_data);
	}

	public void Load2 (){
		Data = File.ReadAllText (Application.persistentDataPath + "/Player.SaveCity");
		GetComponent<ManagerCity>().Gold = int.Parse (Data.Split ('/') [0]);
		GetComponent<ManagerCity>().Gold = int.Parse (Data.Split ('/') [1]);
		GetComponent<ManagerCity>().Gold = int.Parse (Data.Split ('/') [2]);
		I = 1;
		while ((Data.Split ('/') [1]).Split('|')[I] != null){
			Instantiate(Resources.Load(JsonUtility.FromJson<gameState2>((Data.Split ('/') [1]).Split('|')[I]).Name2.Split ('(') [0]), new Vector3(float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[1].Split(',')[0]), float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[2].Split(',')[0]), float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[3].Split(',')[0])), Quaternion.identity);
			I++;
		}
	}
}