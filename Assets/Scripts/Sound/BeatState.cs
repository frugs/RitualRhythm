using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Text;

public class BeatState : MonoBehaviour {

	private bool inBeat = false;

	// Use this for initialization
	void Start () {
		BeatExecutor executor = GetComponent<BeatExecutor> ();
		executor.addCallback (handle);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool isInBeat() {
		return inBeat;
	}

	private bool handle(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters) {
		if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER) {
			FMOD.Studio.TIMELINE_MARKER_PROPERTIES marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
			IntPtr namePtr = marker.name;
			int nameLen = 0;
			while (Marshal.ReadByte(namePtr, nameLen) != 0) ++nameLen;
			byte[] buffer = new byte[nameLen];
			Marshal.Copy(namePtr, buffer, 0, buffer.Length);
			string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
			if (name == "BeatIn") {
				StartCoroutine(setInBeat(true));
//				Debug.Log("In");
			} else if (name == "BeatOut") {
				StartCoroutine(setInBeat(false));
//				Debug.Log("Out");
			}
		}
		return true;
	}

	IEnumerator setInBeat(bool inBeat) {
		yield return new WaitForSeconds (0.05f);
		this.inBeat = inBeat;
	}
}
