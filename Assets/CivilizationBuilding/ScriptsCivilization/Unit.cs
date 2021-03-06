﻿using System.Collections;
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
	Color notClicked;
	public Color clicked;
	bool doneForTurn;
	int myTurn;
	public bool canSettle;
	public GameObject city;
	public GameObject warrior;
	public string team;
	int[,] tiles;
	Node[,] graph;
	public List<Node> currentPath = new List<Node>();
	GameObject instantiated;
	public int AIStyle;
	public int Health;
	public int Defense;
	public int Damage;
	public int Range;
	int ShouldMove;
	public int BoatLevel;
	public Mesh boat;
	Mesh me;
	float offset;
	float offset2;
	float offset3;
	GameObject manager;
	GameObject myAI;
	int Times;
	public AudioClip attack;
	public GameObject attackparticles;
	public int OverrideX = 10000;
	public int OverrideZ;
	public GameObject Light;
	GameObject lightnow;

	// Use this for initialization
	void Start () {
		notClicked = GetComponent<MeshRenderer> ().material.color;
		AIStyle = Random.Range (1, 3);
		foreach(GameObject AI in GameObject.FindGameObjectsWithTag("AI")){
			if(AI.GetComponent<AIManager>().team == team){
				myAI = AI;
				BoatLevel = 2;
				break;
			}
		}
		manager = GameObject.Find ("Manager");
		offset3 = manager.GetComponent<ManagerCivilization> ().offset;
		AIStyle = Random.Range (0, 3);
		lightnow = (Instantiate(Light, new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), Quaternion.identity) as GameObject);
		lightnow.transform.eulerAngles = new Vector3 (-90, 0, 0);
		lightnow.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
		me = GetComponent<MeshFilter> ().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.layer = 2;
		if (Physics.Raycast (transform.position + Vector3.up * 25, Vector3.down, out hit)) {
			if (hit.collider.gameObject.CompareTag("Water")) {
				GetComponent<MeshFilter> ().mesh = boat;
				transform.position = new Vector3(transform.position.x, (8 * Mathf.PerlinNoise(Time.time/30 + offset3, 0f) * Mathf.PerlinNoise(Time.time/3 + offset, Time.time/3 + offset2)) - 2.5f, transform.position.z);
			}else{
				if (hit.collider.gameObject.CompareTag ("DeepWater")) {
					GetComponent<MeshFilter> ().mesh = boat;
					transform.position = new Vector3(transform.position.x, (10 * Mathf.PerlinNoise(Time.time/30 + offset3, 0f) * Mathf.PerlinNoise(Time.time/3 + offset, Time.time/3 + offset2)) - 2.5f, transform.position.z);
				} else {
					GetComponent<MeshFilter> ().mesh = me;
				}
			}
		}
		gameObject.layer = 0;
		if(Health <= 0){
			Destroy (gameObject);
		}
		if(GameObject.Find("Manager") != null){
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn != myTurn) {
				if(OverrideX != 10000){
					CreatePathGraph (OverrideX/10 + 20, OverrideZ/10 + 20);
					OverrideX = 10000;
					OverrideZ = 0;
				}
				if(team != "Player"){
					AI ();
				}
				doneForTurn = false;
				myTurn = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn;
				if(currentPath.Count > 0 && doneForTurn == false){
					transform.position = new Vector3((currentPath[1].x * 10) - 200, 5f, (currentPath[1].z * 10) - 200);
					transform.eulerAngles = new Vector3 (0, (currentPath[1].x - currentPath[0].x) * 90 + ((currentPath[1].z - currentPath[0].z) * 90 + Mathf.Abs(currentPath[1].z - currentPath[0].z) * -90), 0);
					gameObject.layer = 2;
					if (Physics.Raycast (transform.position + Vector3.up * 25, Vector3.down, out hit)) {
						if (hit.collider.gameObject.tag != "Unit") {
							currentPath.Remove (currentPath [0]);
							if(currentPath.Count == 1){
								currentPath.Remove (currentPath [0]);
								if (Physics.Raycast (transform.position + Vector3.up * 25, Vector3.down, out hit)) {
									if (hit.collider.gameObject.tag == "City") {
										hit.collider.gameObject.GetComponent<CityCivilization> ().team = team;
									}
								}
							}
						}else{
							transform.position = new Vector3((currentPath[0].x * 10) - 200, 5f, (currentPath[0].z * 10) - 200);
						}
					}
					gameObject.layer = 0;
					offset = transform.position.x / (manager.GetComponent<ManagerCivilization>().xAmount * 2 + 1);
					offset2 = transform.position.z / (manager.GetComponent<ManagerCivilization>().zAmount * 2 + 1);
				}
				if(canSettle == false){
					lightnow.transform.position = new Vector3 (transform.position.x, transform.position.y + 6, transform.position.z);
				}else{
					lightnow.transform.position = new Vector3 (transform.position.x, transform.position.y + 8, transform.position.z);
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
									lightnow.transform.eulerAngles = new Vector3 (90, 0, 0);
									lightnow.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
									if (canSettle == true) {
										StartCoroutine (ShowSettleButton ());
									}
								} else {
									GetComponent<MeshRenderer> ().material.color = notClicked;
									lightnow.transform.eulerAngles = new Vector3 (-90, 0, 0);
									lightnow.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
							} else {
								if (GetComponent<MeshRenderer> ().material.color == clicked) {
									GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
								}
								GetComponent<MeshRenderer> ().material.color = notClicked;
								lightnow.transform.eulerAngles = new Vector3 (-90, 0, 0);
								lightnow.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
							}
						}
					}
				}
				if (Input.GetMouseButtonDown (1) && GetComponent<MeshRenderer> ().material.color == clicked) {
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
						if (hit.collider.gameObject.CompareTag ("Grass") || (BoatLevel == 1 && hit.collider.gameObject.CompareTag ("Water")) || hit.collider.gameObject.CompareTag ("City") || BoatLevel == 2) {
							CreatePathGraph (Mathf.RoundToInt (hit.collider.transform.position.x/10 + 20), Mathf.RoundToInt (hit.collider.transform.position.z/10 + 20));
						}
						if (hit.collider.gameObject.CompareTag ("Unit")) {
							if(Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < Range && hit.collider.gameObject.GetComponent<Unit>().team != team){
								Attack (hit.collider.gameObject);
								doneForTurn = true;
							}
						}
					}
				}
			}
			if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCities" || doneForTurn == true) {
				GetComponent<MeshRenderer> ().material.color = notClicked;
				lightnow.transform.eulerAngles = new Vector3 (-90, 0, 0);
				lightnow.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
				GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
			}
		}else{
			if(GameObject.Find("Manager") == null){
				
			}
		}
	}

	public void Settle (){
		if((canSettle == true && GetComponent<MeshRenderer> ().material.color == clicked) || (canSettle == true && team != "Player")){
			if(canSettleHere()){
				instantiated = Instantiate (city, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
				instantiated.GetComponent<CityCivilization> ().team = team;
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
			if((City.transform.position.x - transform.position.x <= 41 && City.transform.position.x - transform.position.x >= -41) && (City.transform.position.z - transform.position.z <= 41 && City.transform.position.z - transform.position.z >= -41)){
				return false;
			}
		}
		return true;
	}

	void AI(){
		GameObject Enemy;
		Enemy = FindClosestEnemy ();
		if (Vector3.Distance (Enemy.transform.position, transform.position) < Range && doneForTurn == false) {
			Attack (Enemy);
			doneForTurn = true;
		} else {
			if (canSettle == true && currentPath.Count == 0) {
				Settle ();
			}
			int x3;
			int z3;
			if (canSettle == false) {
				if (Vector3.Distance (Enemy.transform.position, transform.position) < 51) {
					x3 = Random.Range (-3, 3);
					z3 = Random.Range (-3, 3);
					if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3)] == 0) {
						CreatePathGraph (Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3));
					}
				}
				if (AIStyle == 1) {
					while (currentPath.Count == 0) {
						x3 = Random.Range (-3, 3);
						z3 = Random.Range (-3, 3);
						if (Vector3.Distance (Enemy.transform.position, transform.position) < 51) {
							if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3)] == 0) {
								CreatePathGraph (Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3));
							}
						} else {
							if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (transform.position.z / 10 + 20 + z3)] == 0) {
								CreatePathGraph (Mathf.RoundToInt (transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (transform.position.z / 10 + 20 + z3));
							}
						}
					}
				} else {
					while (currentPath.Count == 0) {
						Times++;
						x3 = Random.Range (-3, 3);
						z3 = Random.Range (-3, 3);
						if (Vector3.Distance (Enemy.transform.position, transform.position) < 51) {
							if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3)] == 0) {
								CreatePathGraph (Mathf.RoundToInt (Enemy.transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (Enemy.transform.position.z / 10 + 20 + z3));
							}
						} else {
							if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (myAI.GetComponent<AIManager> ().Target.x / 10 + 20 + x3), Mathf.RoundToInt (myAI.GetComponent<AIManager> ().Target.y / 10 + 20 + z3)] == 0) {
								CreatePathGraph (Mathf.RoundToInt (myAI.GetComponent<AIManager> ().Target.x / 10 + 20 + x3), Mathf.RoundToInt (myAI.GetComponent<AIManager> ().Target.y / 10 + 20 + z3));
							}
						}
						if (Times == 2) {
							AIStyle = 1;
							break;
						}
					}
				}
			} else {
				while (currentPath.Count == 0) {
					x3 = Random.Range (-8, 8);
					z3 = Random.Range (-8, 8);
					if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2 [Mathf.RoundToInt (transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (transform.position.z / 10 + 20 + z3)] == 0) {
						CreatePathGraph (Mathf.RoundToInt (transform.position.x / 10 + 20 + x3), Mathf.RoundToInt (transform.position.z / 10 + 20 + z3));
					}
				}
			}
		}
	}

	void CreatePathGraph (int x, int z) {
		if (BoatLevel == 0) {
			tiles = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles2;
			graph = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().graph2;
		}
		if(BoatLevel == 1){
			tiles = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles3;
			graph = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().graph3;
		}
		if(BoatLevel == 2){
			tiles = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().tiles4;
			graph = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().graph4;
		}
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
		List<Node> unvisited = new List<Node>();
		Node source = graph[Mathf.RoundToInt(transform.position.x/10 + 20), Mathf.RoundToInt(transform.position.z/10 + 20)];
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
	}

	void Attack (GameObject Target){
		GetComponent<AudioSource> ().clip = attack;
		Instantiate (attackparticles, Target.transform);
		GetComponent<AudioSource> ().Play ();
		Target.GetComponent<Unit> ().Health -= Mathf.Clamp (Damage - Target.GetComponent<Unit> ().Defense, 0, 10000);
	}

	public GameObject FindClosestEnemy()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Unit");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos){
			if (go.GetComponent<Unit>().team != team){
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		}
		return closest;
	}
}