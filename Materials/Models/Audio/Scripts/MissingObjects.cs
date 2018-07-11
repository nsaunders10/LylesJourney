using UnityEngine;
using System.Collections;

public class MissingObjects : MonoBehaviour {

	public GameObject correctTransform;
	public GameObject ship;
	public static bool coreReturned;
	public static bool keyReturned;
	public static bool killReturned;
	public static bool elevatorFixed;
	public bool gravGen;
	public bool key;
	public bool killKey;
	public bool elevatorThing;
	AudioSource sound;
	public AudioClip confirm;

	void Start () {
		sound = GetComponent<AudioSource> ();
		ship.SetActive (false);
		keyReturned = false;
		killReturned = false;
		sound.mute = false;
	}

	void Update () {
		
		if (coreReturned && gravGen) {
			GravityToggle.gravOn = true;
			transform.position = correctTransform.transform.position;
			transform.rotation = correctTransform.transform.rotation;
			StartCoroutine (Mute ());
		}
		if (keyReturned && key) {
			transform.position = correctTransform.transform.position;
			transform.rotation = correctTransform.transform.rotation;
			ship.SetActive (true);
		}
		if (killReturned && killKey) {
			transform.position = correctTransform.transform.position;
			transform.rotation = correctTransform.transform.rotation;
			ship.SetActive (true);
		}
		if (elevatorThing && elevatorFixed) {
			transform.position = correctTransform.transform.position;
			transform.rotation = correctTransform.transform.rotation;
		}
	}

	IEnumerator Mute(){
		yield return new WaitForSeconds (2f);
		if (gravGen)
			sound.mute = true;
	}
	void OnTriggerEnter(Collider other){
		
		if (other.tag == ("Missing Core")&& gravGen) {
			coreReturned = true;
			sound.PlayOneShot (confirm);
		}
		//Bad end key
		if (other.tag == ("Missing Key") && key && !killReturned && !keyReturned && Interaction.scene == 6) {
		//	sound.PlayOneShot (confirm);
			keyReturned = true;
			sound.PlayOneShot (confirm, 0.5f);
		}
		//Good end key
		if (other.tag == ("Missing Key") && killKey && !keyReturned && !killReturned && Interaction.scene == 6) {
			sound.PlayOneShot (confirm,0.5f);
			killReturned = true;
		}
		if (other.tag == ("Elevator Hole") && elevatorThing && !elevatorFixed) {
			sound.PlayOneShot (confirm);
			elevatorFixed = true;
		}
	}
}
