﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.color = notClicked;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn != myTurn) {
			doneForTurn = false;
			myTurn = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn;
		}
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCivilization") {
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
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().stage == "BuildCities") {
			GetComponent<MeshRenderer> ().material.color = notClicked;
			GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
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
}