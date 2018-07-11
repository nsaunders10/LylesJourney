using UnityEngine;
using System.Collections;

public class MuteAudioScources : MonoBehaviour {

	AudioSource sound;
	void Start () {
		sound = GetComponent<AudioSource> ();
	}
	

	void Update () {
		
		if (SuitControls.driving && !GravityToggle.gravOn) {
			sound.mute = true;
		} else
			sound.mute = false;
	}
}
