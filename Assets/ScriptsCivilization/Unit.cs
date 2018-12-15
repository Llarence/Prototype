﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	
	RaycastHit Hit;
	public Color NotClicked;
	public Color Clicked;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.color = NotClicked;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit)){
				if(Hit.collider.gameObject == gameObject){
					if (GetComponent<MeshRenderer>().material.color == NotClicked) {
						GetComponent<MeshRenderer>().material.color = Clicked;
					} else {
						GetComponent<MeshRenderer>().material.color = NotClicked;
					}
				}else{
					GetComponent<MeshRenderer>().material.color = NotClicked;
				}
			}
		}
		if(Input.GetMouseButtonDown(1)){
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit)){
				if (transform.position.x - Hit.point.x > 4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
					transform.Translate (-10, 0, 0);
				}
				if (transform.position.x - Hit.point.x < -4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
					transform.Translate (10, 0, 0);
				}
				if (transform.position.z - Hit.point.z > 4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
					transform.Translate (0, 0, -10);
				}
				if (transform.position.z - Hit.point.z < -4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
					transform.Translate (0, 0, 10);
				}
			}
		}
	}
}
