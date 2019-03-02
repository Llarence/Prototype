using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour {

	int turnOn;
	int TotalStorage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOn != GameObject.Find("Manager").GetComponent<ManagerCivilization>().turn){
			turnOn++;
			GetComponent<Text> ().text = "Gold:0";
			TotalStorage = 0;
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				TotalStorage += 10 + (12 * City.GetComponent<CityCivilization> ().Storages);
			}
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				GetComponent<Text>().text = "Gold:" + (int.Parse(GetComponent<Text>().text.Split(':')[1]) + Mathf.Clamp(int.Parse(GetComponent<Text>().text.Split(':')[2]) + City.GetComponent<CityCivilization> ().GoldProduced, 0 , TotalStorage)).ToString();
			}
		}
	}
}
