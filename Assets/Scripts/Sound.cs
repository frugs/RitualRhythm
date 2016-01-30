using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public string music = "event:/Music/Beat";

	public string enemyCount = "Enemy Count";

	FMOD.Studio.EventInstance musicEv;
	FMOD.Studio.ParameterInstance enemyCountPa;

	// Use this for initialization
	void Start () {

		musicEv = FMODUnity.RuntimeManager.CreateInstance (music);

		musicEv.getParameter (enemyCount, out enemyCountPa);

		// timelinePos = musicEv.getTimelinePosition;

		musicEv.start ();

	}

	void enemyCountChange()
	{
		enemyCountPa.setValue ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
