using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceCounter : MonoBehaviour {

	int turnOn;
	int TotalStorage;
	int Sea;
	public int gold;
	int PlayerCities;
	public int Iron;
	public int Copper;
	public int Unobtainium;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOn != GameObject.Find("Manager").GetComponent<ManagerCivilization>().turn){
			turnOn++;
			TotalStorage = 0;
			Iron = 0;
			Copper = 0;
			Unobtainium = 0;
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if (City.GetComponent<CityCivilization> ().team == "Player") {
					TotalStorage += 15 + (12 * City.GetComponent<CityCivilization> ().Storages);
					if(Sea < City.GetComponent<CityCivilization> ().Sea){
						Sea = City.GetComponent<CityCivilization> ().Sea;
						foreach(GameObject unit in GameObject.FindGameObjectsWithTag("Unit")){
							unit.GetComponent<Unit> ().BoatLevel = Sea;
						}
					}
					Iron += City.GetComponent<CityCivilization> ().Iron.Count;
					Copper += City.GetComponent<CityCivilization> ().Copper.Count;
					Unobtainium += City.GetComponent<CityCivilization> ().Unobtainium.Count;
				}
			}
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if(City.GetComponent<CityCivilization>().team == "Player"){
					GetComponent<Text> ().text = ("Gold:" + ((Mathf.Clamp (int.Parse (GetComponent<Text> ().text.Split (':') [1].Split (',') [0]) + City.GetComponent<CityCivilization> ().GoldProduced, 0, TotalStorage)).ToString ()) + ", Iron:" + Iron + ", Copper:" + Copper + ", Unobtainium:" + Unobtainium);
				}
			}
			gold = int.Parse (GetComponent<Text> ().text.Split (':') [1].Split (',') [0]);
			PlayerCities = 0;
			if(Input.GetKey(KeyCode.F)){
				gold = 1000;
				GetComponent<Text> ().text = ("Gold:" + "1000" + ", Iron:" + Iron + ", Copper:" + Copper + ", Unobtainium:" + Unobtainium);
			}
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if(City.GetComponent<CityCivilization>().team == "Player"){
					PlayerCities++;
				}
			}
			if(gold > 999 || PlayerCities == GameObject.FindGameObjectsWithTag("City").Length){
				SceneManager.LoadScene ("Win");
			}
		}
	}
}
