using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	Animator anim;
	AudioSource sound;
	public AudioClip door;
	public AudioClip dontGoIn;
	bool open;
	bool playfloatmusic;
	bool ohdear;
	public bool elevator;
	public bool unlock1;
	public bool unlock2;
	public bool unlock3;
	public bool unlock4;
	public bool unlock5;

	void Start () {
		anim = GetComponent<Animator> ();
		sound = GetComponent<AudioSource> ();
		playfloatmusic = false;
		ohdear = false;
	}
	

	void Update () {
		anim.SetBool ("Open", open);

		if (elevator && !ElevatorController.stop) {
			open = false;
		}
		if (elevator && !MissingObjects.elevatorFixed) {
			open = false;
		}
		if (unlock5 && Interaction.unlock5) {
			open = true;
		} if (unlock5 && !Interaction.unlock5) 
			open = false;
		if (unlock4 && GravityToggle.gravOn) {
			sound.mute = true;
			sound.loop = false;
		}
		if (unlock4 && Interaction.scene == 7 && MissingObjects.keyReturned)
			open = false;
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == ("Player")) {
		if ((unlock1 && !Interaction.unlock1) || (unlock2 && !Interaction.unlock2) || (unlock3 && !Interaction.unlock3) || (unlock4 && !Interaction.unlock4))
				return;
		else {
			if (!unlock5) {
					open = true;
					if(!unlock4)
					sound.PlayOneShot (door, 0.5f);
					if (unlock4 && GravityToggle.gravOn)
						sound.PlayOneShot (door, 0.5f);
			}
				if (unlock5 && Interaction.camerasOn && !ohdear) {
					if(Interaction.unlock5)
					sound.PlayOneShot (door, 0.5f);
					Interaction.unlock5 = false;
					sound.PlayOneShot (dontGoIn);
					ohdear = true;
				}
		}
	}
		if (unlock4 && Interaction.unlock4 && other.tag == ("Space Suit")) {
			open = true;
			if (!GravityToggle.gravOn && !playfloatmusic) {
				sound.Play ();
				playfloatmusic = true;
			}	
		}

	}

	void OnTriggerExit(Collider other){

		if (other.tag == ("Player")) {
			if ((unlock1 && !Interaction.unlock1) || (unlock2 && !Interaction.unlock2) || (unlock3 && !Interaction.unlock3) || (unlock4 && !Interaction.unlock4)) 
			return;
		else {
			if (!unlock5) {
				open = false;
				if(!unlock4)
				sound.PlayOneShot (door, 0.5f);
				if(unlock4 && GravityToggle.gravOn)
				sound.PlayOneShot (door, 0.5f);
			}
		}
	}
		if (unlock4 && Interaction.unlock4 && other.tag == ("Space Suit")) {
			open = false;
		}

	}
}
