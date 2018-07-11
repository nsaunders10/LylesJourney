using UnityEngine;
using System.Collections;

public class UnlockDoors : MonoBehaviour {

	AudioSource sound;
	public AudioClip soundAndColor;
	bool theEnd;
	float audioWait;

	void Start () {
		sound = GetComponent<AudioSource> ();
	}
	

	void Update () {

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == ("Player")) {
			theEnd = true;
			//sound.Play ();
		//	sound.PlayOneShot (soundAndColor);
			StartCoroutine (LylesEnd ());
		}

	}
	IEnumerator LylesEnd(){
		
		yield return new WaitForSeconds (4f);
		StartCoroutine (OpenDoor ());
		sound.Play ();
	}
	IEnumerator OpenDoor(){

		yield return new WaitForSeconds (28f);
	}
}