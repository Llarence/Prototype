using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	public string team;
	public Vector2 Target;

	// Use this for initialization
	void Start () {
		Target.x = FindClosestEnemy ().transform.position.x;
		Target.y = FindClosestEnemy ().transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
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