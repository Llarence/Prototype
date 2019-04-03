using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Node {
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
	public List<Node> currentPath = new List<Node>();
	GameObject instantiated;
	public int AIStyle;
	
	// Use this for initialization
	void Start () {
		AIStyle = Random.Range (0, 3);
		GetComponent<MeshRenderer> ().material.color = notClicked;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Manager") != null){
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn != myTurn) {
				doneForTurn = false;
				myTurn = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn;
				if(currentPath.Count > 0){
					transform.position = new Vector3((currentPath[0].x * 10) - 500, 5f, (currentPath[0].z * 10) - 500);
					currentPath.RemoveAt (0);
					foreach(GameObject city in GameObject.FindGameObjectsWithTag("City")){
						if(city.transform.position.x == transform.position.x && city.transform.position.z == transform.position.z){
							city.GetComponent<CityCivilization> ().team = team;
						}
					}
				}
				if(team != "Player"){
					AI ();
				}
			}
		}
		if (team == "Player") {
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCivilization" || doneForTurn == false) {
				if (Input.mousePosition.x < Screen.width - 350) {
					if (Input.GetMouseButtonDown (0)) {
						if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
							if (hit.collider.gameObject == gameObject) {
								if (GetComponent<MeshRenderer> ().material.color == notClicked) {
									GetComponent<MeshRenderer> ().material.color = clicked;
									if (canSettle == true) {
										StartCoroutine (ShowSettleButton ());
									}
								} else {
									GetComponent<MeshRenderer> ().material.color = notClicked;
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
							} else {
								if (GetComponent<MeshRenderer> ().material.color == clicked) {
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
								GetComponent<MeshRenderer> ().material.color = notClicked;
							}
						}
					}
				}
				if (Input.GetMouseButtonDown (1) && GetComponent<MeshRenderer> ().material.color == clicked) {
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
						if (hit.collider.gameObject.CompareTag ("Grass")) {
							CreatePathGraph (Mathf.RoundToInt (hit.collider.transform.position.x/10 + 50), Mathf.RoundToInt (hit.collider.transform.position.z/10 + 50));
						}
					}
				}
			}
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCities" || doneForTurn == true) {
				GetComponent<MeshRenderer> ().material.color = notClicked;
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
				instantiated = Instantiate (city, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
				instantiated.GetComponent<CityCivilization> ().team = team;
				GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().createGraph();
				instantiated = Instantiate (warrior, new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
				instantiated.GetComponent<Unit> ().team = team;
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
		while(currentPath.Count == 0){
			int x3 = Random.Range (-3, 3);
			int z3 = Random.Range(-3, 3);
			if(GameObject.Find ("Manager").GetComponent<ManagerCivilization>().tiles2[Mathf.RoundToInt(transform.position.x/10 + 50 + x3), Mathf.RoundToInt(transform.position.z/10 + 50 + z3)] == 0){
				CreatePathGraph (Mathf.RoundToInt(transform.position.x/10 + 50 + x3), Mathf.RoundToInt(transform.position.z/10 + 50 + z3));
			}
		}
	}

	void CreatePathGraph (int x, int z) {
		tiles = GameObject.Find ("Manager").GetComponent<ManagerCivilization>().tiles2;
		graph = GameObject.Find ("Manager").GetComponent<ManagerCivilization>().graph2;
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
		List<Node> unvisited = new List<Node>();
		Node source = graph[Mathf.RoundToInt(transform.position.x/10 + 50), Mathf.RoundToInt(transform.position.z/10 + 50)];
		Node target = graph[x, z];
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
		currentPath = new List<Node>();
		Node curr = target;
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}
		currentPath.Reverse();
		currentPath.RemoveAt (0);
	}

	void TutorialMove(){
		if (Input.mousePosition.x < Screen.width - 350) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					if (hit.collider.gameObject == gameObject) {
						if (GetComponent<MeshRenderer> ().material.color == notClicked) {
							GetComponent<MeshRenderer> ().material.color = clicked;
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
}