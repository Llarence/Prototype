using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityCivilization : MonoBehaviour {

	GameObject Manager;
	RaycastHit Hit;
	float ClickTime;

	// Use this for initialization
	void Start () {
		Manager = GameObject.Find ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out Hit)) {
				if (Hit.collider.gameObject == gameObject) {
					ClickTime = ClickTime + Time.deltaTime;
					if(ClickTime >= 1){
						Manager.GetComponent<SaveLoadCivilizaton> ().Save ();
						SceneManager.LoadScene ("CityBuilding");
					}
				}
			}
		}
	}
}
