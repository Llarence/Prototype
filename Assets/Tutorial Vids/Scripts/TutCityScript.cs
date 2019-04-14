using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutCityScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnTutorialVids()
    {
        GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, -90, 0);
        GameObject.Find("Skip Tutorial").GetComponent<RectTransform>().transform.Rotate(0, -90, 0);
        GameObject.Find("Play Tutorial").GetComponent<RectTransform>().transform.Rotate(0, 90, 0);
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Play();
    }

    public void OffTutorialVids()
    {
        GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, 90, 0);
        GameObject.Find("Skip Tutorial").GetComponent<RectTransform>().transform.Rotate(0, 90, 0);
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Pause();
    }
}
