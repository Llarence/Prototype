using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityNameText : MonoBehaviour {

	public string[] names;
	public string overideName;

	// Use this for initialization
	void Start () {
		GetName ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (2 * transform.position - GameObject.Find("Main Camera").transform.position);
	}

	void GetName(){
		if(overideName != ""){
			GetComponent<TextMesh> ().text = overideName;
			foreach (string name in names){
				if(GetComponent<TextMesh> ().text == name){
					transform.parent.gameObject.GetComponent<CityCivilization> ().capital = true;
				}
			}
			return;
		}
		if(transform.parent.gameObject.GetComponent<CityCivilization>().capital == true){
			GetComponent<TextMesh> ().text = names[Random.Range(0, names.Length)];
		}else{
			GetComponent<TextMesh> ().text = ('A' + Random.Range (0, 26)).ToString();
		}
		foreach(GameObject City in GameObject.FindGameObjectsWithTag("City")){
			if(City.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == GetComponent<TextMesh> ().text && City != transform.parent.gameObject){
				GetName ();
				return;
			}
		}
	}
}
