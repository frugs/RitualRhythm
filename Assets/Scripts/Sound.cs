using UnityEngine;
using System.Collections;
using System;

public class Sound : MonoBehaviour {

	private const string music = "event:/Music/Beat";
	private const string enemyCount = "Enemy Count";
	private const string punch = "event:/SFX/Vox/A/Strike";

	public const int BEATS_PER_MINUTE = 130;
	public const int BEAT_MILLIS = 60000 / BEATS_PER_MINUTE;
	private const int ALLOWED_OFFSET_MILLIS = 20;
	private const int USER_LAG_MILLIS = 50;

	private FMOD.Studio.EventInstance musicEv;
	private FMOD.Studio.EventInstance punchEv;
	private FMOD.Studio.ParameterInstance enemyCountPa;
	private FMOD.Studio.EVENT_CALLBACK cb;

	private float songTime;
	private float previousFrameTime;
	private float lastReportedPlayPosition;

	// Use this for initialization
	void Start () {

		musicEv = FMODUnity.RuntimeManager.CreateInstance (music);
		punchEv = FMODUnity.RuntimeManager.CreateInstance (punch);
		musicEv.getParameter (enemyCount, out enemyCountPa);
		cb = new FMOD.Studio.EVENT_CALLBACK(StudioEventCallback);
		musicEv.setCallback(cb, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

		previousFrameTime = Time.time;
		lastReportedPlayPosition = 0;
		musicEv.start ();
		enemyCountChange (5);

	}

	public FMOD.RESULT StudioEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters) {
		Debug.Log ("It works!!!");
		playPunch ();
		return FMOD.RESULT.OK;
	}
	
	void Update() {
		songTime += Time.time - previousFrameTime;
		previousFrameTime = Time.time;
		float newReportedPosition = (float)getFMODTime () / (float)1000;
		if (newReportedPosition > lastReportedPlayPosition) {
			songTime = (songTime + newReportedPosition) / 2;
			lastReportedPlayPosition = newReportedPosition;
		} else if (newReportedPosition < lastReportedPlayPosition) {

		}
//		Debug.Log (songTime);
	}

	void enemyCountChange(int count)
	{
		enemyCountPa.setValue (count);
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space) {
			int offset;
			if (isOnBeat(out offset)) {
				Debug.Log ("Pressed on beat. Offset: " + offset);
			} else {
				Debug.Log ("Pressed off beat. Offset: " + offset);
			}
		}
	}

	public void playPunch() {
		punchEv.start();
	}

	private int getFMODTime() {
		int val;
		musicEv.getTimelinePosition (out val);
		return val;
	}

	public int getSongTime() {
		return (int)(songTime * 1000);
	}

	public bool isOnBeat (out int offset)
	{
		int currMillis;
		musicEv.getTimelinePosition (out currMillis);

		int mod = (currMillis - USER_LAG_MILLIS) % BEAT_MILLIS;
		int absOffset = Mathf.Min (mod, BEAT_MILLIS - mod);
		offset = absOffset == mod ? mod : -(BEAT_MILLIS - mod);
		if (mod < ALLOWED_OFFSET_MILLIS || mod > BEAT_MILLIS - ALLOWED_OFFSET_MILLIS) {
			return true;
		}

		return false;
	}
}
