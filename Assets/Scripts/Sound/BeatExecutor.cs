using UnityEngine;
using System.Collections;
using System;

public class BeatExecutor : MonoBehaviour {

	public Sound soundManager;

	// Use this for initialization
	void Start () {
//		soundManager.addBeatCallback (onBeat);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	FMOD.RESULT onBeat(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters) {
		Debug.Log ("Still works");
		return FMOD.RESULT.OK;
	}
}
