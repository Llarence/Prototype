using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCivilization : MonoBehaviour {

	public GameObject mountain;
	public GameObject grass;
	public GameObject water;
	public GameObject deepWater;
	public GameObject beach;
	public GameObject city;
	public GameObject warrior;
	public GameObject settler;
	public GameObject copper;
	public GameObject iron;
	public GameObject unobtainium;
	int x;
	int z;
	public float offset;
	public float start;
	public int xAmount;
	public int zAmount;
	int Cities;
	public int turn;
	GameObject[] units;
	public GameObject text;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;
	public string gameName;
	List<string> files;
	public GameObject newText;
	GameObject textGameObject;
	public List<GameObject> texts;
	int loops;
	public string stage;
	List<Vector3> CityPositions = new List<Vector3>();
	int tries;
	GameObject NewCity;
	public int [,] tiles2;
	public Node [,] graph2;
	int UnitsDone;

	void Start (){
		stage = "BuildCities";
		SpawnGameNames ();
 		if (GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().inGameName != "") {
			GetComponent<SaveLoadCivilizaton> ().Load ();
		}
	}

	public void SpawnGameNames() {
		loops = 0;
		foreach(GameObject name in texts){
			Destroy (name);
		}
		texts = new List<GameObject>();
		files = new List<string> ();
		foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath)){
			if (file.Split ('~').Length > 1) {
				if ((file.Split ('~') [1]).Split ('.') [0] == "Civilization") {
					files.Add((file.Split ('~') [1]).Split ('.') [1]);
				}
			}
		}
		foreach (string file in files){
			loops++;
			textGameObject = Instantiate (newText, Vector3.zero, Quaternion.identity);
			textGameObject.transform.SetParent (GameObject.Find("Canvas").transform);
			textGameObject.GetComponent<RectTransform>().localPosition = new Vector3 ((320 * ((loops) - (files.Count/2f))) - 160, -120, 0);
			textGameObject.GetComponent<Text>().text = file;
			texts.Add (textGameObject);
		}
	}

	public void GenerateMap () {
		gameName = text.transform.GetChild (1).transform.GetChild (2).GetComponent<Text> ().text;
		GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().inGameName = gameName;
		if (gameName != "") {
			Destroy (text);
			Destroy (text2);
			Destroy (text3);
			Destroy (text4);
			foreach (GameObject name in texts) {
				Destroy (name);
			}
			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			GameObject.Find ("NextStage").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			GameObject.Find ("CurrentStage").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			GameObject.Find ("Mini Map").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			offset = Random.Range (-1000f, 1000f);
			Random.InitState (Mathf.CeilToInt(offset));
			x = -xAmount * 10;
			z = -zAmount * 10;
			while (x < (xAmount * 10) + 10) {
				while (z < (zAmount * 10) + 10) {
					if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.4575f) {
						Instantiate (deepWater, new Vector3 (x, -1f, z), Quaternion.identity);
					} else {
						if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.5f) {
							Instantiate (water, new Vector3 (x, -1f, z), Quaternion.identity);
						} else {
							if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.5275f) {
								Instantiate (beach, new Vector3 (x, -4.5f, z), Quaternion.identity);
							} else {
								if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.825f) {
									Instantiate (grass, new Vector3 (x, -4.5f, z), Quaternion.identity);
									if (Random.Range (1, 301) == 1) {
										Instantiate (unobtainium, new Vector3 (x, 11, z), Quaternion.identity);
									} else {
										if(Random.Range(1, 151) == 1){
											Instantiate (copper, new Vector3 (x, 0, z), Quaternion.identity);
										} else {
											if(Random.Range(1, 76) == 1){
												Instantiate (iron, new Vector3 (x, 0, z), Quaternion.identity);
											}
										}
									}
								} else {
									if(Random.Range(0f, 1f) < 0.2f){
										Instantiate (mountain, new Vector3 (x, -0.5f, z), Quaternion.identity);
									}else{
										Instantiate (grass, new Vector3 (x, -4.5f, z), Quaternion.identity);
									}
								}
							}
						}
					}
					z += 10;
				}
				z = -xAmount * 10;
				x += 10;
			}
			while (Cities != 4) {
				x = Random.Range (-xAmount + 2, xAmount - 1) * 10;
				z = Random.Range (-zAmount + 2, zAmount - 1) * 10;
				if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.825f && Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) > 0.5f) {
					if(isLegalSpawn()){
						CityPositions.Add (new Vector3(x, 2, z));
						Cities++;
					}
					tries++;
					if(tries == 5000){
						break;
					}
				}
			}
			Cities = 0;
			while (Cities != CityPositions.Count) {
				NewCity = (Instantiate (city, CityPositions[Cities], Quaternion.identity) as GameObject);
				if (Cities == 0) {
					NewCity.GetComponent<CityCivilization> ().team = "Player";
				} else {
					NewCity.GetComponent<CityCivilization> ().team = Cities.ToString();
				}
				NewCity.GetComponent<CityCivilization> ().capital = true;
				if(Cities == 0){
					(Instantiate (warrior, new Vector3 (CityPositions[Cities].x, 5f, CityPositions[Cities].z), Quaternion.identity) as GameObject).GetComponent<Unit>().team = "Player";
				}else{
					Instantiate (warrior, new Vector3 (CityPositions[Cities].x, 5f, CityPositions[Cities].z), Quaternion.identity);
				}
				Cities++;
			}
		}
		createGraph ();
		foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
			if(City.GetComponent<CityCivilization>().team == "Player"){
				Camera.main.transform.position = new Vector3 (City.transform.position.x - 150, 100, City.transform.position.z);
				break;
			}
		}
	}

	void Update(){
		units = GameObject.FindGameObjectsWithTag("Unit");
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
			Camera.main.GetComponent<AudioSource>().Play(0);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene ("Menu");
		}
	}

	public void NextTurn (){
		stage = "BuildCivilization";
		GameObject.Find ("CurrentStage").GetComponent<Text> ().text = "Build Civilization";
		turn++;
	}

	public void NextStage (){
		if(stage == "BuildCities"){
			NextTurn();
			return;
		}
		GetComponent<SaveLoadCivilizaton> ().Save();
		stage = "BuildCities";
		GameObject.Find ("CurrentStage").GetComponent<Text> ().text = "Build Cities";
	}
		
	public void CallSettle (){
		foreach (GameObject Unit in units) {
			Unit.GetComponent<Unit> ().Settle ();
		}
	}

	public void Spawn (string Unit){
		foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
			if(City.GetComponent<CityCivilization>().Selected){
				City.GetComponent<CityCivilization>().SpawnCity (Unit);
				break;
			}
		}
	}

	bool isLegalSpawn (){
		if(CityPositions.Count != 0){
			foreach(Vector3 pos in CityPositions){
				if((pos.x - x <= 95 && pos.x - x >= -95) && (pos.z - z <= 95 && pos.z - z >= -95)){
					return false;
				}
			}
		}
		return true;
	}
		
	public void Back (){
		SceneManager.LoadScene ("Menu");
	}

	public void CountDone(){
		UnitsDone++;
		if(UnitsDone == GameObject.FindGameObjectsWithTag("Unit").Length){
			UnitsDone = 0;
			StartCoroutine ("i");
		}
	}

	IEnumerator i (){
		yield return new WaitForSeconds (5);
		foreach(GameObject Unit in GameObject.FindGameObjectsWithTag("Unit")){
			Unit.GetComponent<Unit> ().CheckPos ();
		}
	}

	public void createGraph(){
		tiles2 = new int [101, 101];
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("Grass")){
			tiles2 [Mathf.RoundToInt(grass.transform.position.x/10) + 50, Mathf.RoundToInt(grass.transform.position.z/10) + 50] = 0;
		}
		foreach(GameObject water in GameObject.FindGameObjectsWithTag("Water")){
			tiles2 [Mathf.RoundToInt(water.transform.position.x/10) + 50, Mathf.RoundToInt(water.transform.position.z/10) + 50] = 9999;
		}
		foreach(GameObject deepwater in GameObject.FindGameObjectsWithTag("DeepWater")){
			tiles2 [Mathf.RoundToInt(deepwater.transform.position.x/10) + 50, Mathf.RoundToInt(deepwater.transform.position.z/10) + 50] = 9999;
		}
		foreach(GameObject mountain in GameObject.FindGameObjectsWithTag("Mountain")){
			tiles2 [Mathf.RoundToInt(mountain.transform.position.x/10) + 50, Mathf.RoundToInt(mountain.transform.position.z/10) + 50] = 9999;
		}
		graph2 = new Node[101, 101];
		for(int x2 = 0; x2 < 101; x2++){
			for(int z2 = 0; z2 < 101; z2++){
				graph2[x2, z2] = new Node();
				graph2[x2, z2].x = x2;
				graph2[x2, z2].z = z2;
			}
		}
		for (int x2 = 0; x2 < 101; x2++) {
			for (int z2 = 0; z2 < 101; z2++) {
				if (tiles2 [x2, z2] != 9999) {
					if (x2 > 0 && tiles2 [x2 - 1, z2] != 9999) {
						graph2 [x2, z2].neighbours.Add (graph2 [x2 - 1, z2]);
					}
					if (x2 < 100 && tiles2 [x2 + 1, z2] != 9999) {
						graph2 [x2, z2].neighbours.Add (graph2 [x2 + 1, z2]);
					}
					if (z2 > 0 && tiles2 [x2, z2 - 1] != 9999) {
						graph2 [x2, z2].neighbours.Add (graph2 [x2, z2 - 1]);
					}
					if (z2 < 100 && tiles2 [x2, z2 + 1] != 9999) {
						graph2 [x2, z2].neighbours.Add (graph2 [x2, z2 + 1]);
					}
				}
			}
		}
	}
}