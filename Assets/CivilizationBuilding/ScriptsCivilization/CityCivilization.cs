using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityCivilization : MonoBehaviour {

	GameObject manager;
	RaycastHit hit;
	float clickTime;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
				if (hit.collider.gameObject == gameObject) {
					clickTime = clickTime + Time.deltaTime;
					if(clickTime >= 1){
						manager.GetComponent<SaveLoadCivilizaton> ().Save ();
						SceneManager.LoadScene ("CityBuilding");
					}
				}
			}
		}
	}
}
