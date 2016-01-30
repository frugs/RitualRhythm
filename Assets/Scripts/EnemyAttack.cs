using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public Sound soundManager;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		 sprite = gameObject.GetComponent (SpriteRenderer);
	}
	
	// Update is called once per frame
	void Update () {
		int offset;
		if (soundManager.isOnBeat (offset)) {
			sprite.color.r = 1.0;
		} else {
			sprite.color.r = 0.0;
		}
	}
}
