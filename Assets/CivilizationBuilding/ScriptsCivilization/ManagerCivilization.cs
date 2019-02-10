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
	public string gameName;
	List<string> files;
	public GameObject newText;
	GameObject textGameObject;
	public List<GameObject> texts;
	int loops;

	void Start (){
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
			foreach (GameObject name in texts) {
				Destroy (name);
			}
			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			GameObject.Find ("NextStage").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			offset = Random.Range (-1000f, 1000f);
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
								} else {
									Instantiate (mountain, new Vector3 (x, -0.5f, z), Quaternion.identity);
								}
							}
						}
					}
						z += 10;
					}
				z = -xAmount * 10;
				x += 10;
			}
			while (Cities != 10) {
				x = Random.Range (-xAmount, xAmount + 1) * 10;
				z = Random.Range (-zAmount, zAmount + 1) * 10;
				if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.825f && Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) > 0.5f) {
					Instantiate (city, new Vector3 (x, 2f, z), Quaternion.identity);
					Instantiate (settler, new Vector3 (x, 5f, z), Quaternion.identity);
					Instantiate (warrior, new Vector3 (x, 5f, z + 10), Quaternion.identity);
					Instantiate (warrior, new Vector3 (x, 5f, z - 10), Quaternion.identity);
					Instantiate (warrior, new Vector3 (x + 10, 5f, z), Quaternion.identity);
					Instantiate (warrior, new Vector3 (x - 10, 5f, z), Quaternion.identity);
					Cities++;
				}
			}
		}
	}

	void Update(){
		units = GameObject.FindGameObjectsWithTag("Unit");
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
			Camera.main.GetComponent<AudioSource>().Play(0);
		}
	}

	public void NextTurn (){
		turn++;
	}
		
	public void CallSettle (){
		foreach (GameObject Unit in units) {
			Unit.GetComponent<Unit> ().Settle ();
		}
	}
}