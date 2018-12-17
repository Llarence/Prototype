using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	
	RaycastHit Hit;
	GameObject Collider;
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
		if(Input.GetMouseButtonDown(1) && GetComponent<MeshRenderer>().material.color == Clicked){
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit)){
				if (transform.position.x - Hit.point.x > 4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
					transform.Translate (-10, 0, 0);
					gameObject.layer = 2;
					if(Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out Hit)){
						if(Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit"){
							transform.Translate (10, 0, 0);
						}
					}
					transform.eulerAngles = new Vector3 (0, 0, 0);
					gameObject.layer = 0;
				}
				if (transform.position.x - Hit.point.x < -4 && transform.position.z - Hit.point.z > -4 && transform.position.z - Hit.point.z < 4) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
					transform.Translate (10, 0, 0);
					gameObject.layer = 2;
					if(Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out Hit)){
						print (Hit.collider.gameObject.tag);
						if(Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit"){
							transform.Translate (-10, 0, 0);
						}
					}
					transform.eulerAngles = new Vector3 (0, 180, 0);
					gameObject.layer = 0;
				}
				if (transform.position.z - Hit.point.z > 4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
					transform.Translate (0, 0, -10);
					gameObject.layer = 2;
					if(Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out Hit)){
						print (Hit.collider.gameObject.tag);
						if(Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit"){
							transform.Translate (0, 0, 10);
						}
					}
					transform.eulerAngles = new Vector3 (0, 270, 0);
					gameObject.layer = 0;
				}
				if (transform.position.z - Hit.point.z < -4 && transform.position.x - Hit.point.x > -4 && transform.position.x - Hit.point.x < 4) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
					transform.Translate (0, 0, 10);
					gameObject.layer = 2;
					if(Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out Hit)){
						print (Hit.collider.gameObject.tag);
						if(Hit.collider.gameObject.tag != "Grass" && Hit.collider.gameObject.tag != "City" || Hit.collider.gameObject.tag == "Unit"){
							transform.Translate (0, 0, -10);
						}
					}
					transform.eulerAngles = new Vector3 (0, 90, 0);
					gameObject.layer = 0;
				}
			}
		}
	}
}
