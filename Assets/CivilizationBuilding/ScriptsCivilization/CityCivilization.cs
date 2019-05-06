using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityCivilization : MonoBehaviour {

	GameObject manager;
	RaycastHit hit;
	float clickTime;
	public GameObject border;
	public int turnIAmOn;
	public int Gold;
	public int Food;
	public int Population;
	public int Farms;
	public int Houses;
	public int GoldMines;
	public int Storages;
	public int People;
	public int Sea;
	string filePath;
	bool expanded;
	public string Name;
	public bool Selected;
	public int GoldProduced;
	public GameObject Settler;
	public GameObject Warrior;
	public GameObject AxeMan;
	public GameObject Archer;
	public bool capital;
	public string team;
	public Material Player;
	public Material Old;
	public List<GameObject> Copper = new List<GameObject>();
	public List<GameObject> Iron = new List<GameObject>();
	public List<GameObject> Unobtainium = new List<GameObject>();

	// Use this for initialization
	void Start () {
		Old = GetComponent<MeshRenderer> ().material;
		manager = GameObject.Find ("Manager");
		Instantiate (border, transform.position + new Vector3(10, -2.45f, 0), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(10, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(0, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, 0), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, -10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(0, -2.45f, -10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(10, -2.45f, -10), Quaternion.identity);
		if(File.Exists (Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton>().loadName + "." + Name)){
			filePath = Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton> ().loadName + "." + Name;
			Population = int.Parse(File.ReadAllText (filePath).Split ('/') [2]);
		}
		if(Population > 40){
			expanded = true;
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 0), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(0, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 0), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(0, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(10, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-10, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-10, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(10, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, -10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, -10), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (team != "Player") {
			GetComponent<MeshRenderer> ().material = Old;
		}
		if (team == "Player") {
			GetComponent<MeshRenderer> ().material = Player;
			if (Input.GetMouseButton (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					if (hit.collider.gameObject == gameObject) {
						clickTime = clickTime + Time.deltaTime;
						if (clickTime >= 1) {
							if (manager.GetComponent<ManagerCivilization> ().stage == "BuildCities") {
								manager.GetComponent<SaveLoadCivilizaton> ().Save ();
								GameObject.Find ("InfoStorage").GetComponent<InfoStorage> ().cityName = transform.GetChild (0).gameObject.GetComponent<TextMesh> ().text;
								SceneManager.LoadScene ("CityBuilding");
							}
						}
					} else {
						clickTime = 0;
					}
				}
			}
			if (Input.GetMouseButtonDown (1)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					if (hit.collider.gameObject == gameObject && Selected == false) {
						GameObject.Find ("Spawn Settler").transform.Rotate (0, -90, 0);
						GameObject.Find ("Spawn Warrior").transform.Rotate (0, -90, 0);
						GameObject.Find ("Spawn AxeMan").transform.Rotate (0, -90, 0);
						GameObject.Find ("Spawn Archer").transform.Rotate (0, -90, 0);
						Selected = true;
					} else {
						if (Selected == true) {
							GameObject.Find ("Spawn Settler").transform.Rotate (0, 90, 0);
							GameObject.Find ("Spawn Warrior").transform.Rotate (0, 90, 0);
							GameObject.Find ("Spawn AxeMan").transform.Rotate (0, 90, 0);
							GameObject.Find ("Spawn Archer").transform.Rotate (0, -90, 0);
							Selected = false;
						}
					}
				} else {
					if (Selected == true) {
						GameObject.Find ("Spawn Settler").transform.Rotate (0, 90, 0);
						GameObject.Find ("Spawn Warrior").transform.Rotate (0, 90, 0);
						GameObject.Find ("Spawn AxeMan").transform.Rotate (0, 90, 0);
						GameObject.Find ("Spawn Archer").transform.Rotate (0, -90, 0);
						Selected = false;
					}
				}
			}
			if (manager.GetComponent<ManagerCivilization> ().stage == "BuildCities" && GameObject.Find ("Spawn Settler").transform.eulerAngles.y == 0) {
				GameObject.Find ("Spawn Settler").transform.Rotate (0, 90, 0);
				GameObject.Find ("Spawn Warrior").transform.Rotate (0, 90, 0);
				GameObject.Find ("Spawn AxeMan").transform.Rotate (0, 90, 0);
				GameObject.Find ("Spawn Archer").transform.Rotate (0, -90, 0);
				Selected = false;
			}
		}
		if (turnIAmOn < manager.GetComponent<ManagerCivilization> ().turn) {
			UpdateTurn ();
		}
	}

	void Calculate (){
		Food += Mathf.Clamp(Mathf.Clamp (Population, 0, Houses * 1000000), 0, Farms * 2) * 2;
		Food -= Population;
		if(Food < 0){
			Population += Food;
			Food = 0;
		}
		Population += Mathf.FloorToInt(Mathf.Clamp (Food, 0, Population/4));
		Population = Mathf.Clamp (Population, 4, Houses * 3 + 4);
		Gold += Mathf.FloorToInt(Mathf.Clamp (Mathf.Clamp(Population, 0, Houses * 1000000) - Mathf.Clamp(Population, 0, Farms * 2), 0, GoldMines * 2 - (Farms + Houses + Storages)));
		Food = Mathf.FloorToInt(Mathf.Clamp (Food, 0, Storages * 12));
		Gold = Mathf.FloorToInt(Mathf.Clamp (Gold, -Mathf.Infinity, (Storages * 12) + 10));
		GoldProduced = Mathf.FloorToInt(Mathf.Clamp (Mathf.Clamp(Population, 0, Houses * 1000000) - Mathf.Clamp(Population, 0, Farms * 2), 0, GoldMines * 2));
	}

	void UpdateTurn (){
		if(File.Exists (Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton>().loadName + "." + transform.GetChild(0).GetComponent<TextMesh>().text)){
			filePath = Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton> ().loadName + "." + transform.GetChild (0).GetComponent<TextMesh> ().text;
			Gold = int.Parse(File.ReadAllText (filePath).Split ('/') [0]);
			Food = int.Parse(File.ReadAllText (filePath).Split ('/') [1]);
			Population = int.Parse(File.ReadAllText (filePath).Split ('/') [2]);
			Farms = int.Parse(File.ReadAllText (filePath).Split ('/') [3]);
			Houses = int.Parse(File.ReadAllText (filePath).Split ('/') [4]);
			GoldMines = int.Parse(File.ReadAllText (filePath).Split ('/') [5]);
			Storages = int.Parse(File.ReadAllText (filePath).Split ('/') [6]);
			Sea = int.Parse(File.ReadAllText (filePath).Split ('/') [7].Split('|') [0]);
		}
		if(Population > 40 && expanded == false){
			expanded = true;
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 0), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(0, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 0), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(0, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(10, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-10, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-10, -2.45f, 20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(10, -2.45f, -20), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, 10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, -10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(-20, -2.45f, 10), Quaternion.identity);
			Instantiate (border, transform.position + new Vector3(20, -2.45f, -10), Quaternion.identity);
		}
		turnIAmOn++;
		Calculate ();
		if(File.Exists (Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton>().loadName + "." + transform.GetChild(0).GetComponent<TextMesh>().text)){
			filePath = Application.persistentDataPath + "/~Player." + manager.GetComponent<SaveLoadCivilizaton> ().loadName + "." + transform.GetChild (0).GetComponent<TextMesh> ().text;
			File.WriteAllText (filePath, Gold + "/" + Food + "/" + Population + "/" + File.ReadAllText(filePath).Split ('/') [3] + "/" + File.ReadAllText(filePath).Split ('/') [4] + "/" + File.ReadAllText(filePath).Split ('/') [5] + "/" + File.ReadAllText(filePath).Split ('/') [6] + "/" + File.ReadAllText(filePath).Split ('/') [7]);
		}
		AI ();
	}

	public void SpawnCity (string UnitName){
		if (Physics.Raycast (transform.position + Vector3.up * 10, Vector3.down, out hit)) {
			if (hit.collider.gameObject.tag != "Unit") {
				if (team == "Player") {
					if (manager.GetComponent<ManagerCivilization> ().stage != "BuildCities") {
						if (UnitName == "Warrior" && GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold >= 10) {
							(Instantiate (Warrior, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = "Player";
							GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold -= 10;
						}
						if (UnitName == "Settler" && GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold >= 15) {
							(Instantiate (Settler, new Vector3 (transform.position.x, 0.3f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = "Player";
							GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold -= 15;
						}
						if (UnitName == "AxeMan" && GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold >= 15) {
							(Instantiate (AxeMan, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = "Player";
							GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold -= 15;
						}
						if (UnitName == "Archer" && GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold >= 15) {
							(Instantiate (Archer, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = "Player";
							GameObject.Find ("Resources").GetComponent<ResourceCounter> ().gold -= 15;
						}
					}
				}
				if (team != "Player") {
					if (UnitName == "Warrior") {
						(Instantiate (Warrior, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = team;
					}
					if (UnitName == "Settler") {
						(Instantiate (Settler, new Vector3 (transform.position.x, 0.3f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = team;
					}
					if (UnitName == "AxeMan") {
						(Instantiate (AxeMan, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = team;
					}
					if (UnitName == "Archer") {
						(Instantiate (Archer, new Vector3 (transform.position.x, 5f, transform.position.z), Quaternion.identity) as GameObject).GetComponent<Unit> ().team = team;
					}
				}
			}
		}
	}

	void AI (){
		if (team != "Player" && 1 == Random.Range (1, 17)) {
			SpawnCity ("Warrior");
		}else if (team != "Player" && 1 == Random.Range (1, 20)) {
			SpawnCity ("AxeMan");
		}else if (team != "Player" && 1 == Random.Range (1, 30)) {
			SpawnCity ("Archer");
		}else if (team != "Player" && 1 == Random.Range (1, 20)) {
			SpawnCity ("Archer");
		}
	}

	public void CheckResources(){
		foreach(GameObject iron in GameObject.FindGameObjectsWithTag ("Iron")){
			if (expanded == false) {
				if ((iron.transform.position.x - transform.position.x <= 10 && iron.transform.position.x - transform.position.x >= -10) && (iron.transform.position.z - transform.position.z <= 10 && iron.transform.position.z - transform.position.z >= -10)) {
					Iron.Add (iron);
				}
			} else {
				if ((iron.transform.position.x - transform.position.x <= 20 && iron.transform.position.x - transform.position.x >= -20) && (iron.transform.position.z - transform.position.z <= 20 && iron.transform.position.z - transform.position.z >= -20)) {
					Iron.Add (iron);
				}
			}
		}
		foreach(GameObject copper in GameObject.FindGameObjectsWithTag ("Copper")){
			if (expanded == false) {
				if ((copper.transform.position.x - transform.position.x <= 10 && copper.transform.position.x - transform.position.x >= -10) && (copper.transform.position.z - transform.position.z <= 10 && copper.transform.position.z - transform.position.z >= -10)) {
					Copper.Add (copper);
				}
			} else {
				if ((copper.transform.position.x - transform.position.x <= 20 && copper.transform.position.x - transform.position.x >= -20) && (copper.transform.position.z - transform.position.z <= 20 && copper.transform.position.z - transform.position.z >= -20)) {
					Copper.Add (copper);
				}
			}
		}
		foreach(GameObject unobtainium in GameObject.FindGameObjectsWithTag ("Unobtainium")){
			if (expanded == false) {
				if ((unobtainium.transform.position.x - transform.position.x <= 10 && unobtainium.transform.position.x - transform.position.x >= -10) && (unobtainium.transform.position.z - transform.position.z <= 10 && unobtainium.transform.position.z - transform.position.z >= -10)) {
					Unobtainium.Add (unobtainium);
				}
			} else {
				if ((unobtainium.transform.position.x - transform.position.x <= 20 && unobtainium.transform.position.x - transform.position.x >= -20) && (unobtainium.transform.position.z - transform.position.z <= 20 && unobtainium.transform.position.z - transform.position.z >= -20)) {
					Unobtainium.Add (unobtainium);
				}
			}
		}
	}
}