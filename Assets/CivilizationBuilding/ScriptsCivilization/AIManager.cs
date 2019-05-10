using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	public string team;
	public Vector2 Target;
	int turnOn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOn != GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().turn){
			turnOn++;
			Target.x = FindClosestEnemy ().transform.position.x;
			Target.y = FindClosestEnemy ().transform.position.z;
		}
	}

	public GameObject FindClosestEnemy(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("City");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			if (go.GetComponent<CityCivilization> ().team != team) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
				
			}
		}
		return closest;
	}
}