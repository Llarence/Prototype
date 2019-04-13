using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceCounter : MonoBehaviour {

	int turnOn;
	int TotalStorage;
	public int gold;
	int PlayerCities;
	int Iron;
	int Copper;
	int Unobtainium;

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "Gold:" + "0" + " Iron:" + Iron + " Copper:" + Copper + " Unobtainium:" + Unobtainium;
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
					TotalStorage += 10 + (12 * City.GetComponent<CityCivilization> ().Storages);
					Iron += City.GetComponent<CityCivilization> ().Iron.Count;
					Copper += City.GetComponent<CityCivilization> ().Copper.Count;
					Unobtainium += City.GetComponent<CityCivilization> ().Unobtainium.Count;
				}
			}
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if(City.GetComponent<CityCivilization>().team == "Player"){
					GetComponent<Text>().text = "Gold:" + (Mathf.Clamp(int.Parse(GetComponent<Text>().text.Split(':')[1]) + City.GetComponent<CityCivilization> ().GoldProduced, 0 , TotalStorage)).ToString() + " Iron:" + Iron + " Copper:" + Copper + " Unobtainium:" + Unobtainium;
				}
			}
			gold = int.Parse (GetComponent<Text> ().text.Split (':') [1]);
			PlayerCities = 0;
			if(Input.GetKey(KeyCode.F)){
				gold += 1000;
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
