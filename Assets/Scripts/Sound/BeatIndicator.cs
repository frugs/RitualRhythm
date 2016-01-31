using UnityEngine;
using System.Collections;

public class BeatIndicator : MonoBehaviour {

	public GameObject mark;
	public Sound soundManager;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			createMarker(i);
//			createMarker(-i);
		}
	}

	void createMarker (int markerIndex)
	{
		GameObject marker = (GameObject) Instantiate (mark, new Vector3 (10, -4), Quaternion.identity);
		BeatMarker markerScript = marker.GetComponent<BeatMarker> ();
		markerScript.index = markerIndex;
		markerScript.soundManager = soundManager;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
