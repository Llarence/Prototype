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

	public GameObject Mountain2;
	public GameObject Grass2;
	public GameObject Water2;
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
	int I;

	public void Save (string SaveName) {
		GameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject CurrentObject in GameObjects) {
			if(CurrentObject.name != "Grass(Clone)" && CurrentObject.name != "Water(Clone)" && CurrentObject.name != "Mountain(Clone)" && CurrentObject.name != "EventSystem" && CurrentObject.name != "Manager" && CurrentObject.name != "Directional Light"  && CurrentObject.name != "Main Camera" && CurrentObject.tag != "Unit" && CurrentObject.layer != 5){
				GameState.x = CurrentObject.transform.position.x;
				GameState.y = CurrentObject.transform.position.y;
				GameState.z = CurrentObject.transform.position.z;
				GameState.Name = CurrentObject.name;
				json_data = json_data + JsonUtility.ToJson(GameState);
			}
		}
		File.WriteAllText (Application.persistentDataPath + "/Player.Save", GetComponent<ManagerCivilization>().offset + "/" + json_data);
	}

	public void Load (){
		Data = File.ReadAllText (Application.persistentDataPath + "/Player.Save");
		offset2 = float.Parse (Data.Split ('/') [0]);
		x = -xAmount2 * 10;
		z = -zAmount2 * 10;
		while (x < (xAmount2 * 10) + 10) {
			while (z < (zAmount2 * 10) + 10) {
				if (Mathf.PerlinNoise((offset2 + ((x + (float)(-xAmount2 * 10))/(5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10))/(5f * zAmount2)))) < 0.5f) {
					Instantiate (Water2, new Vector3 (x, -1f, z), Quaternion.identity);
				} else {
					if(Mathf.PerlinNoise((offset2 + ((x + (float)(-xAmount2 * 10))/(5f * xAmount2))), (offset2 + ((z + (float)(-zAmount2 * 10))/(5f * zAmount2)))) < 0.825f){
						Instantiate (Grass2, new Vector3 (x, -4.5f, z), Quaternion.identity);
					} else {
						Instantiate (Mountain2, new Vector3 (x, -0.5f, z), Quaternion.identity);
					}

				}
				z += 10;
			}
			z = -xAmount2 * 10;
			x += 10;
		}
		I = 1;
		while ((Data.Split ('/') [1]).Split('|')[I] != null){
				Instantiate(Resources.Load(JsonUtility.FromJson<gameState>((Data.Split ('/') [1]).Split('|')[I]).Name.Split ('(') [0]), new Vector3(float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[1].Split(',')[0]), float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[2].Split(',')[0]), float.Parse ((Data.Split ('/') [1]).Split('|')[I].Split(':')[3].Split(',')[0])), Quaternion.identity);
			I++;
		}
	}
}