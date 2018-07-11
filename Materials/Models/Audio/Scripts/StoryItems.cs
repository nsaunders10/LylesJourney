using UnityEngine;
using System.Collections;

public class StoryItems : MonoBehaviour {

	public static bool fireExtinguisher;
	public static bool camsOn;
	public AudioSource Extinguisher;
	public AudioSource alarm;
	public GameObject spray;
	public GameObject survCams;
	public GameObject thirdFloor;
	public GameObject secondFloor;
	public GameObject firstFloor;
	bool playonce;

	float randy;

	void Start () {
		spray.SetActive (false);
		thirdFloor.SetActive (false);
		firstFloor.SetActive (false);
		playonce = true;
		camsOn = false;
	}

	void Update () {
		if (Input.GetButtonDown ("Fire2")) {
			Extinguisher.Play ();
		}

		if (fireExtinguisher && Input.GetButton ("Fire2")) {
			spray.SetActive (true);
		} 

		else {
			Extinguisher.Stop ();
			spray.SetActive (false);
		}
		if (ElevatorController.floorCheck == 3) {
			thirdFloor.SetActive (true);
		}else thirdFloor.SetActive (false);

		if (ElevatorController.floorCheck == 2) {
			secondFloor.SetActive (true);

		}else secondFloor.SetActive (false);

		if (ElevatorController.floorCheck == 1) {
			firstFloor.SetActive (true);
		}
		if (Interaction.camerasOn && camsOn) {
			survCams.SetActive (true);
			firstFloor.SetActive (true);
		if (Interaction.camerasOn)
			return;
	   }
		if (ElevatorController.floorCheck != 1 && !camsOn) {
			survCams.SetActive (false);
			firstFloor.SetActive (false);
		}
		if (!Interaction.leefyOn && playonce) {
			alarm.Play ();
			playonce = false;
		} else if(Effects.fireOut==9 || Interaction.leefyOn){
			alarm.Stop ();
			playonce = true;
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == ("Player")&& this.tag == ("Survey Room")) {
			camsOn = true;
		}
	}
	void OnTriggerExit(Collider other){
		
		if (other.tag == ("Player")&& this.tag == ("Survey Room")) {
			camsOn = false;
			if (Interaction.scene == 2) {
				Interaction.scene = 3;
			}
		}
	}
}
