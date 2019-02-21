using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour {

	int turnOn;

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "Gold:0";
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOn != GameObject.Find("Manager").GetComponent<ManagerCivilization>().turn){
			turnOn++;
			foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
				print (int.Parse(GetComponent<Text>().text.Split(':')[1]));
				GetComponent<Text>().text = "Gold:" + (int.Parse(GetComponent<Text>().text.Split(':')[1]) + City.GetComponent<CityCivilization> ().Gold).ToString();
			}
		}
	}
}
