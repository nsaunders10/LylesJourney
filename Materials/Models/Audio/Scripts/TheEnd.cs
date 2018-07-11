using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour {

	public GameObject inside;
	public GameObject shipCam;
	public GameObject earth;
	public GameObject black;
	public GameObject thanks;
	public Camera cam;
	bool atEarth;
	public float t;
	public float c;
	AudioSource sound;
	public AudioClip warp;

	void Start () {
		sound = GetComponent<AudioSource> ();
		cam.fieldOfView = 60;
		StartCoroutine (TheTime ());
		atEarth = false;
		inside.SetActive(true);
		shipCam.SetActive(false);
		earth.SetActive(false);
		black.SetActive(false);
		thanks.SetActive(false);
	}

	IEnumerator TheTime(){
		yield return new WaitForSeconds (51f);
		t = Time.time;
	}

	void FixedUpdate () {
		

		if (Interaction.theEnd) {
			c = Time.time - t;
			inside.SetActive (false);
			shipCam.SetActive (true);
		
			if (cam.fieldOfView > 59 && cam.fieldOfView < 100)
				cam.fieldOfView = ((60 * c / 3) + 60);
			else if (cam.fieldOfView > 99.999f && cam.fieldOfView < 179) {
				cam.fieldOfView = ((60 * c) + 1);
			} else if (cam.fieldOfView > 178) {
				cam.fieldOfView = 179;
				StartCoroutine (FadeToBlack ());
			}
			if (atEarth) {
				gameObject.transform.Translate (Vector3.right * Time.time/3);
			}
		}
	}
	IEnumerator Warp(){
		yield return new WaitForSeconds (0f);
		sound.PlayOneShot (warp);
	}

	IEnumerator FadeToBlack(){
		yield return new WaitForSeconds (2f);
		cam.fieldOfView = 58;
		earth.SetActive (true);
		atEarth = true;
		black.SetActive (true);
		StartCoroutine (Thanks ());

	}
	IEnumerator Thanks(){
		yield return new WaitForSeconds (22f);
		thanks.SetActive (true);
		Cursor.visible = true;
	}
}
