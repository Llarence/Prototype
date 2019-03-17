using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour {
	
	RaycastHit hit;
	GameObject colliderGameObject;
	public Color notClicked;
	public Color clicked;
	bool doneForTurn;
	int myTurn;
	public bool canSettle;
	public GameObject city;
	public GameObject warrior;
	public string team;
	public GameObject UnitClickArea;
	public List<GameObject> UnitClickAreas = new List<GameObject>();
	int[,] tiles;
	Node[,] graph;


	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.color = notClicked;
		CreatePathGraph ();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Manager") != null){
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn != myTurn) {
				doneForTurn = false;
				myTurn = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn;
				if(team != "Player"){
					AI ();
				}
			}
		}
		if (team == "Player") {
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCivilization" || doneForTurn == true) {
				if (Input.mousePosition.x < Screen.width - 350) {
					if (Input.GetMouseButtonDown (0)) {
						if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
							if (hit.collider.gameObject == gameObject) {
								if (GetComponent<MeshRenderer> ().material.color == notClicked) {
									GetComponent<MeshRenderer> ().material.color = clicked;
									UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3 (0 + transform.position.x, 0, 10 + transform.position.z), Quaternion.identity) as GameObject);
									UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3 (0 + transform.position.x, 0, -10 + transform.position.z), Quaternion.identity) as GameObject);
									UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3 (-10 + transform.position.x, 0, 0 + transform.position.z), Quaternion.identity) as GameObject);
									UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3 (10 + transform.position.x, 0, 0 + transform.position.z), Quaternion.identity) as GameObject);
									if (canSettle == true) {
										StartCoroutine (ShowSettleButton ());
									}
								} else {
									GetComponent<MeshRenderer> ().material.color = notClicked;
									foreach (GameObject ClickableTile in UnitClickAreas) {
										Destroy (ClickableTile);
									}
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
							} else {
								if (GetComponent<MeshRenderer> ().material.color == clicked) {
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
								GetComponent<MeshRenderer> ().material.color = notClicked;
								foreach (GameObject ClickableTile in UnitClickAreas) {
									Destroy (ClickableTile);
								}
							}
						}
					}
				}
				if (Input.mousePosition.x < Screen.width - 350) {
					if (Input.GetMouseButtonDown (1) && GetComponent<MeshRenderer> ().material.color == clicked && doneForTurn == false) {
						if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
							if (transform.position.x - hit.point.x > 4 && transform.position.z - hit.point.z > -4 && transform.position.z - hit.point.z < 4) {
								transform.eulerAngles = new Vector3 (0, 0, 0);
								transform.Translate (-10, 0, 0);
								gameObject.layer = 2;
								doneForTurn = true;
								if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
									if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
										transform.Translate (10, 0, 0);
										doneForTurn = false;
									}
								}
								transform.eulerAngles = new Vector3 (0, 0, 0);
								gameObject.layer = 0;
							}
							if (transform.position.x - hit.point.x < -4 && transform.position.z - hit.point.z > -4 && transform.position.z - hit.point.z < 4) {
								transform.eulerAngles = new Vector3 (0, 0, 0);
								transform.Translate (10, 0, 0);
								gameObject.layer = 2;
								doneForTurn = true;
								if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
									if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
										transform.Translate (-10, 0, 0);
										doneForTurn = false;
									}
								}
								transform.eulerAngles = new Vector3 (0, 180, 0);
								gameObject.layer = 0;
							}
							if (transform.position.z - hit.point.z > 4 && transform.position.x - hit.point.x > -4 && transform.position.x - hit.point.x < 4) {
								transform.eulerAngles = new Vector3 (0, 0, 0);
								transform.Translate (0, 0, -10);
								gameObject.layer = 2;
								doneForTurn = true;
								if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
									if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
										transform.Translate (0, 0, 10);
										doneForTurn = false;
									}
								}
								transform.eulerAngles = new Vector3 (0, 270, 0);
								gameObject.layer = 0;
							}
							if (transform.position.z - hit.point.z < -4 && transform.position.x - hit.point.x > -4 && transform.position.x - hit.point.x < 4) {
								transform.eulerAngles = new Vector3 (0, 0, 0);
								transform.Translate (0, 0, 10);
								gameObject.layer = 2;
								doneForTurn = true;
								if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
									if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
										transform.Translate (0, 0, -10);
										doneForTurn = false;
									}
								}
								transform.eulerAngles = new Vector3 (0, 90, 0);
								gameObject.layer = 0;
							}
						}
					}
				}
			}
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCities" || doneForTurn == true) {
				GetComponent<MeshRenderer> ().material.color = notClicked;
				foreach (GameObject ClickableTile in UnitClickAreas) {
					Destroy (ClickableTile);
				}
				GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
			}
		}else{
			if(GameObject.Find("Manager") == null){
				TutorialMove ();
			}
		}
	}

	public void Settle (){
		if(canSettle == true && GetComponent<MeshRenderer> ().material.color == clicked){
			if(canSettleHere()){
				Instantiate (city, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
				Instantiate (warrior, new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}

	public IEnumerator ShowSettleButton (){
		yield return new WaitForSeconds (Time.deltaTime * 2 + 0.1f);
		GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 0, 0);
	}

	bool canSettleHere (){
		foreach (GameObject City in GameObject.FindGameObjectsWithTag("City")) {
			if((City.transform.position.x - transform.position.x <= 60 && City.transform.position.x - transform.position.x >= -60) && (City.transform.position.z - transform.position.z <= 60 && City.transform.position.z - transform.position.z >= -60)){
				return false;
			}
		}
		return true;
	}

	void AI(){
		if (doneForTurn == false) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
			transform.Translate (0, 0, 10);
			gameObject.layer = 2;
			doneForTurn = true;
			if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
				if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
					transform.Translate (0, 0, -10);
					doneForTurn = false;
				}
			}else{
				transform.Translate (0, 0, -10);
				doneForTurn = false;
			}
			transform.eulerAngles = new Vector3 (0, 90, 0);
			gameObject.layer = 0;
		}
		if (doneForTurn == false) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
			transform.Translate (-10, 0, 0);
			gameObject.layer = 2;
			doneForTurn = true;
			if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
				if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
					transform.Translate (10, 0, 0);
					doneForTurn = false;
				}
			}else{
				transform.Translate (10, 0, 0);
				doneForTurn = false;
			}
			transform.eulerAngles = new Vector3 (0, 0, 0);
			gameObject.layer = 0;
		}
		if (doneForTurn == false) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
			transform.Translate (10, 0, 0);
			gameObject.layer = 2;
			doneForTurn = true;
			if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
				if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
					transform.Translate (-10, 0, 0);
					doneForTurn = false;
				}
			}else{
				transform.Translate (-10, 0, 0);
				doneForTurn = false;
			}
			transform.eulerAngles = new Vector3 (0, 180, 0);
			gameObject.layer = 0;
		}
		if (doneForTurn == false) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
			transform.Translate (0, 0, -10);
			gameObject.layer = 2;
			doneForTurn = true;
			if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
				if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
					transform.Translate (0, 0, 10);
					doneForTurn = false;
				}
			}else{
				transform.Translate (0, 0, 10);
				doneForTurn = false;
			}
			transform.eulerAngles = new Vector3 (0, 270, 0);
			gameObject.layer = 0;
		}
	}

	void TutorialMove(){
		if (Input.mousePosition.x < Screen.width - 350) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					if (hit.collider.gameObject == gameObject) {
						if (GetComponent<MeshRenderer> ().material.color == notClicked) {
							GetComponent<MeshRenderer> ().material.color = clicked;
							UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3(0 + transform.position.x, 0, 10 + transform.position.z), Quaternion.identity) as GameObject);
							UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3(0 + transform.position.x, 0, -10 + transform.position.z), Quaternion.identity) as GameObject);
							UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3(-10 + transform.position.x, 0, 0 + transform.position.z), Quaternion.identity) as GameObject);
							UnitClickAreas.Add (Instantiate (UnitClickArea, new Vector3(10 + transform.position.x, 0, 0 + transform.position.z), Quaternion.identity) as GameObject);
							if (canSettle == true) {
								StartCoroutine (ShowSettleButton ());
							}
						} else {
							GetComponent<MeshRenderer> ().material.color = notClicked;
							foreach(GameObject ClickableTile in UnitClickAreas){
								Destroy (ClickableTile);
							}
							GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
						}
					} else {
						if (GetComponent<MeshRenderer> ().material.color == clicked) {
							GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
						}
						GetComponent<MeshRenderer> ().material.color = notClicked;
						foreach(GameObject ClickableTile in UnitClickAreas){
							Destroy (ClickableTile);
						}
					}
				}
			}
		}
		if (Input.mousePosition.x < Screen.width - 350) {
			if (Input.GetMouseButtonDown (1) && GetComponent<MeshRenderer> ().material.color == clicked && doneForTurn == false) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					if (transform.position.x - hit.point.x > 4 && transform.position.z - hit.point.z > -4 && transform.position.z - hit.point.z < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (-10, 0, 0);
						gameObject.layer = 2;
						doneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
							if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
								transform.Translate (10, 0, 0);
								doneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 0, 0);
						gameObject.layer = 0;
					}
					if (transform.position.x - hit.point.x < -4 && transform.position.z - hit.point.z > -4 && transform.position.z - hit.point.z < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (10, 0, 0);
						gameObject.layer = 2;
						doneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
							if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
								transform.Translate (-10, 0, 0);
								doneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 180, 0);
						gameObject.layer = 0;
					}
					if (transform.position.z - hit.point.z > 4 && transform.position.x - hit.point.x > -4 && transform.position.x - hit.point.x < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (0, 0, -10);
						gameObject.layer = 2;
						doneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
							if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
								transform.Translate (0, 0, 10);
								doneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 270, 0);
						gameObject.layer = 0;
					}
					if (transform.position.z - hit.point.z < -4 && transform.position.x - hit.point.x > -4 && transform.position.x - hit.point.x < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (0, 0, 10);
						gameObject.layer = 2;
						doneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out hit)) {
							if (hit.collider.gameObject.tag != "Grass" && hit.collider.gameObject.tag != "City" || hit.collider.gameObject.tag == "Unit") {
								transform.Translate (0, 0, -10);
								doneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 90, 0);
						gameObject.layer = 0;
					}
				}
			}
		}
	}

	class Node {
		public List<Node> neighbours;

		public Node (){
			neighbours = new List<Node>();
		}
	}

	void CreatePathGraph () {
		tiles = new int [101, 101];
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("Grass")){
			tiles [Mathf.CeilToInt(transform.position.x/10 + 50), Mathf.CeilToInt(transform.position.z/10 + 50)] = 0;
		}
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("Water")){
			tiles [Mathf.CeilToInt(transform.position.x/10 + 50), Mathf.CeilToInt(transform.position.z/10 + 50)] = 1;
		}
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("DeepWater")){
			tiles [Mathf.CeilToInt(transform.position.x/10 + 50), Mathf.CeilToInt(transform.position.z/10 + 50)] = 2;
		}
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("Mountain")){
			tiles [Mathf.CeilToInt(transform.position.x/10 + 50), Mathf.CeilToInt(transform.position.z/10 + 50)] = 3;
		}
		graph = new Node[101, 101];
		for(int xpos = 0; xpos < 101; xpos++){
			for(int zpos = 0; zpos < 101; zpos++){
				graph [xpos, zpos] = new Node();
				if (xpos > 0){
					graph [xpos, zpos].neighbours.Add (graph [xpos - 1, zpos]);
				}
				if (xpos < 100) {
					graph [xpos, zpos].neighbours.Add (graph [xpos + 1, zpos]);
				}
				if (zpos > 0) {
					graph [xpos, zpos].neighbours.Add (graph [xpos, zpos - 1]);
				}
				if (zpos < 100) {
					graph [xpos, zpos].neighbours.Add (graph [xpos, zpos + 1]);
				}
			}
		}

	}
}