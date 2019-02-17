using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityCivilization : MonoBehaviour {

	GameObject manager;
	RaycastHit hit;
	float clickTime;
	public GameObject border;
	public int turnIAmOn;
	public int Farms;
	public int Houses;
	public int GoldMines;
	public int Storages;
	public int People;
	public int Food;
	public int Population;
	public int Gold;

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
		if(manager.GetComponent<ManagerCivilization>().stage == "BuildCities"){
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
		}
		if(turnIAmOn < manager.GetComponent<ManagerCivilization>().turn){
			turnIAmOn++;
			Calculate ();
			AddBorder ();
		}	
	}

	void AddBorder (){
		//Instantiate (border, transform.position + new Vector3(10, -2.45f, -20), Quaternion.identity);
	}

	void Calculate (){
		Food += Mathf.Clamp(Mathf.Clamp (Population, 0, Houses * 1000000), 0, Farms * 2) * 2;
		Food -= Population;
		if(Food < 0){
			Population += Food;
			Food = 0;
		}
		Population += Mathf.FloorToInt(Mathf.Clamp (Food, 0, Population/4));
		Population = Mathf.Clamp (Population, 4, Houses * 3 + 4);
		Gold += Mathf.FloorToInt(Mathf.Clamp (Mathf.Clamp(Population, 0, Houses * 1000000) - Mathf.Clamp(Population, 0, Farms * 2), 0, GoldMines * 2));
		Food = Mathf.FloorToInt(Mathf.Clamp (Food, 0, Storages * 12));
		Gold = Mathf.FloorToInt(Mathf.Clamp (Gold, 0, (Storages * 12) + 10));
		GameObject.Find ("Resources").GetComponent<Text> ().text = "Gold:" + Gold;
		while(People < Population){
			Instantiate (Person, new Vector3(0, 0.5f, 0), Quaternion.identity);
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
		if (People > Population){
			Destroy (GameObject.FindGameObjectWithTag ("Person"));
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
	}
}