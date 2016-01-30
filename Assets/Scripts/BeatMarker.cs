using UnityEngine;
using System.Collections;

public class BeatMarker : MonoBehaviour {

	public int index;
	public Sound soundManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int songTime = soundManager.getSongTime ();
		float pos = (float)(songTime % (Sound.BEAT_MILLIS * 5)) / (Sound.BEAT_MILLIS * 5);

		transform.position = new Vector3(10 - ((float)pos + (float)index), transform.position.y, transform.position.z);
	}
}
//    renderNoteFallingDownScreen(id:int) {
//	      note[id].y = strumBar.y - (songTime - note[id].strumTime) + visualLatencySetting;
//    }