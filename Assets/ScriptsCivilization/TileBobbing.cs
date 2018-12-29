using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBobbing : MonoBehaviour {

	float offset;
	float offset2;
	GameObject Manager;

	// Use this for initialization
	void Start () {
		Manager = GameObject.Find ("Manager");
		offset = transform.position.x/(Manager.GetComponent<ManagerCivilization>().xAmount * 2 + 1);
		offset2 = transform.position.z/(Manager.GetComponent<ManagerCivilization>().zAmount * 2 + 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, (2 * Mathf.PerlinNoise(Time.time + offset, Time.time + offset2)) - 2.5f, transform.position.z);
	}
}