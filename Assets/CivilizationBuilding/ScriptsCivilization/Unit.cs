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
	Node p;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.color = notClicked;
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
				CreatePathGraph (50, 50);
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
		public int x;
		public int z;

		public Node (){
			neighbours = new List<Node>();
		}

		public float DistanceTo(Node n) {
			return Vector2.Distance(new Vector2(x, z), new Vector2(n.x, n.z));
		}
	}

	void CreatePathGraph (int x, int y) {
		tiles = new int [101, 101];
		foreach(GameObject grass in GameObject.FindGameObjectsWithTag("Grass")){
			tiles [Mathf.RoundToInt(grass.transform.position.x/10) + 50, Mathf.RoundToInt(grass.transform.position.z/10) + 50] = 0;
		}
		foreach(GameObject water in GameObject.FindGameObjectsWithTag("Water")){
			tiles [Mathf.RoundToInt(water.transform.position.x/10) + 50, Mathf.RoundToInt(water.transform.position.z/10) + 50] = 9999;
		}
		foreach(GameObject deepwater in GameObject.FindGameObjectsWithTag("DeepWater")){
			tiles [Mathf.RoundToInt(deepwater.transform.position.x/10) + 50, Mathf.RoundToInt(deepwater.transform.position.z/10) + 50] = 9999;
		}
		foreach(GameObject mountain in GameObject.FindGameObjectsWithTag("Mountain")){
			tiles [Mathf.RoundToInt(mountain.transform.position.x/10) + 50, Mathf.RoundToInt(mountain.transform.position.z/10) + 50] = 9999;
		}
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
		List<Node> unvisited = new List<Node>();
		graph = new Node[101, 101];
		for(int x2 = 0; x2 < 101; x2++){
			for(int z2 = 0; z2 < 101; z2++){
				graph[x2, z2] = new Node();
				graph[x2, z2].x = x2;
				graph[x2, z2].z = z2;
			}
		}
		for (int x2 = 0; x2 < 101; x2++) {
			for (int z2 = 0; z2 < 101; z2++) {
				if (tiles [x2, z2] != 9999) {
					if (x2 > 0 && tiles [x2 - 1, z2] != 9999) {
						graph [x2, z2].neighbours.Add (graph [x2 - 1, z2]);
					}
					if (x2 < 100 && tiles [x2 + 1, z2] != 9999) {
						graph [x2, z2].neighbours.Add (graph [x2 + 1, z2]);
					}
					if (z2 > 0 && tiles [x2, z2 - 1] != 9999) {
						graph [x2, z2].neighbours.Add (graph [x2, z2 - 1]);
					}
					if (z2 < 100 && tiles [x2, z2 + 1] != 9999) {
						graph [x2, z2].neighbours.Add (graph [x2, z2 + 1]);
					}
				}
			}
		}
		Node source = graph[Mathf.RoundToInt(transform.position.x/10 + 51), Mathf.RoundToInt(transform.position.z/10 + 51)];
		Node target = graph[x, y];
		dist[source] = 0;
		prev[source] = null;
		foreach(Node v in graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			unvisited.Add(v);
		}
		while(unvisited.Count > 0) {
			Node u = null;
			foreach(Node possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}
			if(u == target) {
				break;
			}
			unvisited.Remove(u);
			foreach(Node v in u.neighbours) {
				float alt = dist[u] + u.DistanceTo(v) + tiles[v.x, v.z];
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}
		if(prev[target] == null) {
			return;
		}
		List<Node> currentPath = new List<Node>();
		Node curr = target;
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}
		currentPath.Reverse();
		p = source;
		foreach(Node node in currentPath){
			Debug.DrawLine(new Vector3((p.x * 10) - 500, 10, (p.z * 10) - 500),new Vector3((node.x * 10) - 500, 10, (node.z * 10) - 500), Color.black, 100);
			p = node;
		}
	}
}