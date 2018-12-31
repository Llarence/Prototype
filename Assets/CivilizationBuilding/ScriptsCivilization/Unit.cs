﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
	
	RaycastHit Hit;
	GameObject Collider;
	public Color NotClicked;
	public Color Clicked;
	bool DoneForTurn;
	int MyTurn;
	public bool CanSettle;
	public GameObject City;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.color = NotClicked;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().Turn != MyTurn) {
			DoneForTurn = false;
			MyTurn = GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().Turn;
		}
		if (Input.mousePosition.x < Screen.width - 350) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out Hit)) {
					if (Hit.collider.gameObject == gameObject) {
						if (GetComponent<MeshRenderer> ().material.color == NotClicked) {
							GetComponent<MeshRenderer> ().material.color = Clicked;
							if (CanSettle == true) {
								StartCoroutine (ShowSettleButton());
							}
						} else {
							GetComponent<MeshRenderer> ().material.color = NotClicked;
							GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
						}
					} else {
						if(GetComponent<MeshRenderer> ().material.color == Clicked){
							GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 90, 0);
						}
						GetComponent<MeshRenderer> ().material.color = NotClicked;
					}
				}
			}
		}
		if (Input.mousePosition.x < Screen.width - 350) {
			if (Input.GetMouseButtonDown (1) && GetComponent<MeshRenderer> ().material.color == Clicked && DoneForTurn == false) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out Hit)) {
					if (transform.position.x - Hit.point.x > 4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (-10, 0, 0);
						gameObject.layer = 2;
						DoneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out Hit)) {
							if (Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit") {
								transform.Translate (10, 0, 0);
								DoneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 0, 0);
						gameObject.layer = 0;
					}
					if (transform.position.x - Hit.point.x < -4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (10, 0, 0);
						gameObject.layer = 2;
						DoneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out Hit)) {
							if (Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit") {
								transform.Translate (-10, 0, 0);
								DoneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 180, 0);
						gameObject.layer = 0;
					}
					if (transform.position.z - Hit.point.z > 4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (0, 0, -10);
						gameObject.layer = 2;
						DoneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out Hit)) {
							if (Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit") {
								transform.Translate (0, 0, 10);
								DoneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 270, 0);
						gameObject.layer = 0;
					}
					if (transform.position.z - Hit.point.z < -4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
						transform.eulerAngles = new Vector3 (0, 0, 0);
						transform.Translate (0, 0, 10);
						gameObject.layer = 2;
						DoneForTurn = true;
						if (Physics.Raycast (transform.position + Vector3.up * 5, Vector3.down, out Hit)) {
							if (Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit") {
								transform.Translate (0, 0, -10);
								DoneForTurn = false;
							}
						}
						transform.eulerAngles = new Vector3 (0, 90, 0);
						gameObject.layer = 0;
					}
				}
			}
		}
	}
	public void Settle (){
		if(CanSettle == true && GetComponent<MeshRenderer> ().material.color == Clicked && Hit.collider.gameObject.tag == "Grass" && Hit.collider.gameObject.tag != "City"){
			Instantiate (City, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
			Destroy (gameObject);
		}
	}

	public IEnumerator ShowSettleButton (){
		yield return new WaitForSeconds (Time.deltaTime * 2 + 0.1f);
		GameObject.Find ("Settle").GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 0, 0);
	}
}