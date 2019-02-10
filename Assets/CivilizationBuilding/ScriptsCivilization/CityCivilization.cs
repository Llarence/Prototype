using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityCivilization : MonoBehaviour {

	GameObject manager;
	RaycastHit hit;
	float clickTime;
	public GameObject border;
	public int turnIAmOn;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		Instantiate (border, transform.position + new Vector3(10, -2.45f, 0), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(10, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(0, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, 10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, 0), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(-10, -2.45f, -10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(0, -2.45f, -10), Quaternion.identity);
		Instantiate (border, transform.position + new Vector3(10, -2.45f, -10), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
				if (hit.collider.gameObject == gameObject) {
					clickTime = clickTime + Time.deltaTime;
					if(clickTime >= 1){
						manager.GetComponent<SaveLoadCivilizaton> ().Save ();
						GameObject.Find ("InfoStorage").GetComponent<InfoStorage>().cityName = transform.GetChild(0).gameObject.GetComponent<TextMesh>().text;
						SceneManager.LoadScene ("CityBuilding");
					}
				}
			}
		}
		if(turnIAmOn < manager.GetComponent<ManagerCivilization>().turn){
			turnIAmOn++;
			AddBorder ();
		}	
	}

	void AddBorder (){
		//Instantiate (border, transform.position + new Vector3(10, -2.45f, -20), Quaternion.identity);
	}
}
