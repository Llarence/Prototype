using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNameText : MonoBehaviour {

	public string[] Names;
	public string OverideName;

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = Names[Random.Range(0, Names.Length)];
		if(OverideName != ""){
			GetComponent<TextMesh> ().text = OverideName;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (2 * transform.position - GameObject.Find("Main Camera").transform.position);
	}
}
