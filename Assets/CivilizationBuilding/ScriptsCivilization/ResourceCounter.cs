using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceCounter : MonoBehaviour {

	int turnOn;
	int TotalStorage;
	public int gold;

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "Gold:0";
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOn != GameObject.Find("Manager").GetComponent<ManagerCivilization>().turn){
			turnOn++;
			TotalStorage = 0;
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if (City.GetComponent<CityCivilization> ().team == "Player") {
					TotalStorage += 10 + (12 * City.GetComponent<CityCivilization> ().Storages);
				}
			}
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				if(City.GetComponent<CityCivilization>().team == "Player"){
					GetComponent<Text>().text = "Gold:" + (Mathf.Clamp(int.Parse(GetComponent<Text>().text.Split(':')[1]) + City.GetComponent<CityCivilization> ().GoldProduced, 0 , TotalStorage)).ToString();
				}
			}
			gold = int.Parse (GetComponent<Text> ().text.Split (':') [1]);
			if(gold > 999){
				SceneManager.LoadScene ("Win");
			}
		}
	}
}
