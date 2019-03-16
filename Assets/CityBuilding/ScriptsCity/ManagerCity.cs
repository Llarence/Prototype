using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCity : MonoBehaviour {

	public GameObject[] Farms;
	public GameObject[] Houses;
	public GameObject[] GoldMines;
	public GameObject[] Storages;
	public GameObject[] People;
	public GameObject Person;
	public int Food;
	public int Population;
	public int Gold;
	public Text Text;
	float TickTime;

	// Use this for initialization
	void Start () {
		//StartCoroutine (Tick());
		GetComponent<SaveLoadCity> ().Load2();
		if(Population < 4){
			Population = 4;
		}
	}

	
	// Update is called once per frame
	void Update(){
		//if(Input.GetKeyDown("r") && TickTime != Mathf.Infinity){
		//	TickTime += 0.5f;
		//}
		//if(Input.GetKeyDown("f") && TickTime != Mathf.Infinity){
		//	TickTime -= 0.5f;
		//}
		TickTime = Mathf.Clamp (TickTime, 0.5f, Mathf.Infinity);
		Text.text = ("Population: " + Population + ", Food: " + Food + ", Gold: " + Gold);
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
			Camera.main.GetComponent<AudioSource>().Play(0);
		}
		Farms = GameObject.FindGameObjectsWithTag ("Farm");
		Houses = GameObject.FindGameObjectsWithTag ("House");
		GoldMines = GameObject.FindGameObjectsWithTag ("GoldMine");
		Storages = GameObject.FindGameObjectsWithTag ("Storage");
		People = GameObject.FindGameObjectsWithTag ("Person");
		while(People.Length < Population){
			Instantiate (Person, new Vector3(0, 0.5f, 0), Quaternion.identity);
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
		while(People.Length < Population){
			Instantiate (Person, new Vector3(0, 0.5f, 0), Quaternion.identity);
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
		if (People.Length > Population){
			Destroy (GameObject.FindGameObjectWithTag ("Person"));
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
	}

	IEnumerator Tick () {
		yield return new WaitForSeconds (TickTime);
		Food += Mathf.Clamp(Mathf.Clamp (Population, 0, Houses.Length * 1000000), 0, Farms.Length * 2) * 2;
		Food -= Population;
		if(Food < 0){
			Population += Food;
			Food = 0;
		}
		Population += Mathf.FloorToInt(Mathf.Clamp (Food, 0, Population/4));
		Population = Mathf.Clamp (Population, 4, Houses.Length * 3 + 4);
		Gold += Mathf.FloorToInt(Mathf.Clamp (Mathf.Clamp(Population, 0, Houses.Length * 1000000) - Mathf.Clamp(Population, 0, Farms.Length * 2), 0, GoldMines.Length * 2));
		Food = Mathf.FloorToInt(Mathf.Clamp (Food, 0, Storages.Length * 12));
		Gold = Mathf.FloorToInt(Mathf.Clamp (Gold, 0, (Storages.Length * 12) + 10));
		Text.text = ("Population: " + Population + ", Food: " + Food + ", Gold: " + Gold + ", TickTime: " + TickTime);
		while(People.Length < Population){
			Instantiate (Person, new Vector3(0, 0.5f, 0), Quaternion.identity);
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
		if (People.Length > Population){
			Destroy (GameObject.FindGameObjectWithTag ("Person"));
			People = GameObject.FindGameObjectsWithTag ("Person");
		}
		StartCoroutine (Tick());
	}

	public void MainMap (){
		GetComponent<SaveLoadCity> ().Save2();
		SceneManager.LoadScene ("Civilization");
	}
}	