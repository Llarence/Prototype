using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
			GetComponent<Text> ().text = "Gold:0";
			GetComponent<Text>().text = "Gold:" + (int.Parse(GetComponent<Text>().text.Split(':')[1]) + City.GetComponent<CityCivilization> ().Gold).ToString();
		}
	}
}
