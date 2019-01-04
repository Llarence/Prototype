﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHologram : MonoBehaviour {

	public GameObject SpawnObject;
	RaycastHit hit;
	public LayerMask HitLayers;
	int Colliding;
	public float SpawnHeight;
	public float Rotation;
	public Color Red;
	public Color Green;

	// Use this for initialization
	void Start () {
		if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, HitLayers)){
			transform.position = new Vector3(hit.point.x, SpawnHeight, hit.point.z);
		}
	}

	// Update is called once per frame
	void Update () {
		if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, HitLayers)){
			transform.position = new Vector3(hit.point.x, SpawnHeight, hit.point.z);
		}
		if (Input.GetMouseButtonDown (1) && Colliding == 0) {
			Instantiate (SpawnObject, transform.position, Quaternion.identity);
			GameObject.Find ("MainCamera").GetComponent<Place> ().SpawningObject = false;
			Camera.main.GetComponent<AudioSource>().Play(0);
			Destroy (gameObject);
		}

		if(Colliding == 0){
			GetComponent<MeshRenderer>().material.color = Green;
		}
		if(Colliding > 0){
			GetComponent<MeshRenderer>().material.color = Red;
		}
	}

	void OnTriggerEnter (Collider Col){
		if(Col.gameObject.layer != 2){
			Colliding++;
		}
	}

	void OnTriggerExit (Collider Col){
		if (Col.gameObject.layer != 2) {
			Colliding--;
		}
	}
}
