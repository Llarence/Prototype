using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public Text GoldText;
	public Text FoodText;
	public float Gold;
	public float Food;

	// Use this for initialization
	void Start () {
		moveStatsUp ();
	}
		
	public void moveStatsUp (){
		GameObject.Find ("Gold").GetComponent<RectTransform> ().transform.Translate (0, 100000, 0);
		GameObject.Find ("Food").GetComponent<RectTransform> ().transform.Translate (0, 100000, 0);
	}
	public void moveStatsDown (){
		GameObject.Find ("Gold").GetComponent<RectTransform> ().transform.Translate (0, -100000, 0);
		GameObject.Find ("Food").GetComponent<RectTransform> ().transform.Translate (0, -100000, 0);
	}
}
