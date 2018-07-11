using UnityEngine;
using System.Collections;

public class YesOrNo : MonoBehaviour {

	public bool yesButton;
	public bool genButton;
	public bool fixCameras;
	public GameObject genLight;
	AudioSource sound;
	public AudioClip buzzer;
	public AudioClip ding;
	public AudioClip SecurityRebooted;

	void Start () {
		if(genLight)
		genLight.SetActive (false);
		sound = GetComponent<AudioSource> ();
	}

	void Update () {
		Interaction.pass = 0;
	}
	void OnTriggerStay(Collider other){

		if (!Interaction.talking && yesButton && other.tag == ("Player") && Input.GetButtonDown("Use")) {
			sound.PlayOneShot (ding);
			Interaction.pass = 1;
		}
		if (!Interaction.talking && !yesButton && other.tag == ("Player") && Input.GetButtonDown("Use") && !fixCameras) {
			sound.PlayOneShot (buzzer);
			Interaction.pass = -1;
		}

		if (genButton && other.tag == ("Player") && Input.GetButtonDown("Use")) {
			genLight.SetActive (true);
			Interaction.genOn = true;
			sound.PlayOneShot (buzzer);
			sound.Play ();
			if (ElevatorController.floorCheck != 1)
			sound.Stop ();
		}
	
		if (fixCameras && other.tag == ("Player") && Input.GetButtonDown("Use")) {
			Interaction.camerasOn = true;
			sound.PlayOneShot (SecurityRebooted);
			genLight.SetActive (true);
		}
	}
}
