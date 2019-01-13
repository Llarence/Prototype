using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCivilization : MonoBehaviour {

	public GameObject Mountain;
	public GameObject Grass;
	public GameObject Water;
	public GameObject Beach;
	public GameObject City;
	public GameObject Warrior;
	public GameObject Settler;
	int x;
	int z;
	public float offset;
	public float start;
	public int xAmount;
	public int zAmount;
	bool NoPlayerCity = true;
	public int Turn;
	GameObject[] Units;
	public GameObject Text;
	public GameObject Text2;
	public GameObject Text3;
	public string GameName;
	List<string> files;
	public GameObject newText;
	GameObject text;
	public List<GameObject> texts;
	int loops;

	void Start (){
		SpawnGameNames ();
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
			text = Instantiate (newText, Vector3.zero, Quaternion.identity);
			text.transform.SetParent (GameObject.Find("Canvas").transform);
			text.GetComponent<RectTransform>().localPosition = new Vector3 ((320 * ((loops) - (files.Count/2f))) - 160, -120, 0);
			text.GetComponent<Text>().text = file;
			texts.Add (text);
		}
	}

	public void GenerateMap () {
		GameName = Text.transform.GetChild (1).transform.GetChild (2).GetComponent<Text> ().text;
		if (GameName != "") {
			Destroy (Text);
			Destroy (Text2);
			Destroy (Text3);
			foreach (GameObject name in texts) {
				Destroy (name);
			}
			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			GameObject.Find ("NextTurn").GetComponent<RectTransform> ().Rotate (0, -90, 0);
			offset = Random.Range (-1000f, 1000f);
			x = -xAmount * 10;
			z = -zAmount * 10;
			while (x < (xAmount * 10) + 10) {
				while (z < (zAmount * 10) + 10) {
					if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.5f) {
						Instantiate (Water, new Vector3 (x, -1f, z), Quaternion.identity);
					} else {
						if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.5275f) {
							Instantiate (Beach, new Vector3 (x, -4.5f, z), Quaternion.identity);
						} else {
							if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.825f) {
								Instantiate (Grass, new Vector3 (x, -4.5f, z), Quaternion.identity);
							} else {
								Instantiate (Mountain, new Vector3 (x, -0.5f, z), Quaternion.identity);
							}
						}
					}
					z += 10;
				}
				z = -xAmount * 10;
				x += 10;
			}
			while (NoPlayerCity == true) {
				x = Random.Range (-xAmount, xAmount + 1) * 10;
				z = Random.Range (-zAmount, zAmount + 1) * 10;
				if (Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) < 0.825f && Mathf.PerlinNoise ((offset + ((x + (float)(-xAmount * 10)) / (5f * xAmount))), (offset + ((z + (float)(-zAmount * 10)) / (5f * zAmount)))) > 0.5f) {
					Instantiate (City, new Vector3 (x, 2f, z), Quaternion.identity);
					Instantiate (Settler, new Vector3 (x, 5f, z), Quaternion.identity);
					Instantiate (Warrior, new Vector3 (x, 5f, z + 10), Quaternion.identity);
					Instantiate (Warrior, new Vector3 (x, 5f, z - 10), Quaternion.identity);
					Instantiate (Warrior, new Vector3 (x + 10, 5f, z), Quaternion.identity);
					Instantiate (Warrior, new Vector3 (x - 10, 5f, z), Quaternion.identity);
					NoPlayerCity = false;
				}
			}
		}
	}

	void Update(){
		Units = GameObject.FindGameObjectsWithTag("Unit");
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
			Camera.main.GetComponent<AudioSource>().Play(0);
		}
	}

	public void NextTurn (){
		Turn++;
	}
		
	public void CallSettle (){
		foreach (GameObject Unit in Units) {
			Unit.GetComponent<Unit> ().Settle ();
		}
	}

	public void Start (){
		RectTransform.Translate (0, 100000, 0);
		GameObject.Find ("CameraRotator").GetComponent<CameraRotator> ().cameraToCenter = 1;
	}
}