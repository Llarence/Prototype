using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutCivScript : MonoBehaviour {

    public bool playTutorial;

    // Use this for initialization
    void Start() {
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Pause();
        playTutorial = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
    public void SpawnTutorialVid()
    {
        GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, -90, 0);
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Play();
        GameObject.Find("Skip Tutorial").GetComponent<RectTransform>().transform.Translate(10000, 0, 0);
        playTutorial = true;
    }
    public void OffTutorialVids()
    {
        GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, 90, 0);
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Pause();
        GameObject.Find("Skip Tutorial").GetComponent<RectTransform>().transform.Translate(-10000, 0, 0);
        playTutorial = false;
    }
}
