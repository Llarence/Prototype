using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsProjector : MonoBehaviour {

	public float Gold;
	public float Food;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Gold = GameObject.Find ("GameStats").GetComponent<StatsManager> ().Gold;
	}
}
