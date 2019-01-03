﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {

	public float rotation;
	public bool SpawningObject = false;
	public GameObject HouseHologram;
	public GameObject FarmHologram;
	public GameObject GoldMineHologram;
	public GameObject StorageHologram;
	GameObject ObjectSpawning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && SpawningObject == true) {
			SpawningObject = false;
			Destroy(ObjectSpawning);
			return;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			ObjectSpawning.transform.Rotate (0, 90, 0);
			rotation = rotation + 90;
		}
	}

	public void SpawnHouseHologram(){
		Destroy (ObjectSpawning);
		SpawningObject = true;
		ObjectSpawning = Instantiate (HouseHologram, new Vector3(0, -1000, 0), Quaternion.identity);
		Camera.main.GetComponent<AudioSource>().Play(0);
		return;
	}
	public void SpawnFarmHologram(){
		Destroy (ObjectSpawning);
		SpawningObject = true;
		ObjectSpawning = Instantiate (FarmHologram, new Vector3(0, -1000, 0), Quaternion.identity);
		Camera.main.GetComponent<AudioSource>().Play(0);
		return;
	}
	public void SpawnGoldMineHologram(){
		Destroy (ObjectSpawning);
		SpawningObject = true;
		ObjectSpawning = Instantiate (GoldMineHologram, new Vector3(0, -1000, 0), Quaternion.identity);
		Camera.main.GetComponent<AudioSource>().Play(0);
		return;
	}
	public void SpawnStorageHologram(){
		Destroy (ObjectSpawning);
		SpawningObject = true;
		ObjectSpawning = Instantiate (StorageHologram, new Vector3(0, -1000, 0), Quaternion.identity);
		Camera.main.GetComponent<AudioSource>().Play(0);
		return;
	}
}