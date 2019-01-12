using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNameText : MonoBehaviour {

	public string[] Names;

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = Names[Random.Range(0, Names.Length + 1)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
