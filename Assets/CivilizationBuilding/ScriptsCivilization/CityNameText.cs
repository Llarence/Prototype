using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNameText : MonoBehaviour {

	public string[] Names;

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = Names[Random.Range(0, Names.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (2 * transform.position - GameObject.Find("Main Camera").transform.position);
	}
}
