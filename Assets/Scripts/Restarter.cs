﻿using UnityEngine;
using System.Collections;
using RitualRhythm.Actor.Player;

public class Restarter : MonoBehaviour {

	public Sound soundManager;
	public PlayerBehaviour player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.Equals(null)) {
			StartCoroutine(playEnding());
		}
	}

	IEnumerator playEnding() {
		
		soundManager.setState(4);
		yield return new WaitForSeconds(10);
		Application.LoadLevel(Application.loadedLevel);
	}
}
