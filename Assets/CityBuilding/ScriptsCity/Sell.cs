using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour {

	public int Worth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100)){
				if(hit.collider.gameObject == gameObject){
				GameObject.Find ("Manager").GetComponent<ManagerCity> ().Gold += Worth;
				Destroy (gameObject);
				}
			}
		}
	}
}
