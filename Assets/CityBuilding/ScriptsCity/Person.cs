using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent>().SetDestination(new Vector3(Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f)));
		StartCoroutine (ChooseDestination());
	}

	// Update is called once per frame
	IEnumerator ChooseDestination () {
		yield return new WaitForSeconds (Random.Range(3f, 10f));
		GetComponent<NavMeshAgent>().SetDestination(new Vector3(Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f)));
		StartCoroutine (ChooseDestination());
	}
}
