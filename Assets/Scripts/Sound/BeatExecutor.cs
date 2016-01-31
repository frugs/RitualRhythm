using UnityEngine;
using System.Collections.Generic;
using System;

public class BeatExecutor : MonoBehaviour {


	public delegate Boolean FMODCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters);

	private IList<FMODCallback> callbacks;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public FMOD.RESULT onBeat(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters) {
		foreach (FMODCallback function in callbacks) {
			function.Invoke(type, eventInstance, parameters);
		}
		return FMOD.RESULT.OK;
	}
}
