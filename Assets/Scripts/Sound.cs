using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	private const string music = "event:/Music/Beat";
	private const string enemyCount = "Enemy Count";

	private const int BEATS_PER_MINUTE = 260;
	private const int BEAT_MILLIS = 60000 / BEATS_PER_MINUTE;
	private const int ALLOWED_OFFSET_MILLIS = 50;
	private const int USER_LAG_MILLIS = 50;

	private FMOD.Studio.EventInstance musicEv;
	private FMOD.Studio.ParameterInstance enemyCountPa;

	int count;

	// Use this for initialization
	void Start () {

		musicEv = FMODUnity.RuntimeManager.CreateInstance (music);
		musicEv.getParameter (enemyCount, out enemyCountPa);

		musicEv.start ();

	}

	void enemyCountChange(int count)
	{
		enemyCountPa.setValue (count);
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		if (count % 300 == 0) {
			float thing;
			enemyCountPa.getValue(out thing);
			enemyCountChange((int)thing + 1);
		}
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

	bool isOnBeat (out int offset)
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
