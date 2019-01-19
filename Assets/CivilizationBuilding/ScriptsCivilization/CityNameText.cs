using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNameText : MonoBehaviour {

	public string[] names;
	public string overideName;

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = names[Random.Range(0, names.Length)];
		if(overideName != ""){
			GetComponent<TextMesh> ().text = overideName;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (2 * transform.position - GameObject.Find("Main Camera").transform.position);
	}
}
