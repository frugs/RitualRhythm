using UnityEngine;
using System.Collections.Generic;
using System;

public class BeatExecutor : MonoBehaviour {


	public delegate bool FMODCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters);

	private List<FMODCallback> queue = new List<FMODCallback>();
	private IList<FMODCallback> callbacks = new List<FMODCallback>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void queueCallback(FMODCallback callback) {
		queue.Add (callback);
	}

	public void addCallback(FMODCallback callback) {
		callbacks.Add (callback);
	}

	public FMOD.RESULT onBeat(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters) {
		foreach (FMODCallback function in callbacks) {
			function.Invoke(type, eventInstance, parameters);
		}

	    queue.RemoveAll(callback => callback.Invoke(type, eventInstance, parameters));
		return FMOD.RESULT.OK;
	}
}
