using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElevatorController : MonoBehaviour {

	public GameObject elevator;
	public Text floorNumber;
	public AudioClip ding;
	float speed;
	public static bool stop;
	public bool left;
	public bool right;
	public static bool down;
	public static bool up;
	public static int floorCheck = 2;
	AudioSource sounds;


	void Start () {
		sounds = GetComponent<AudioSource> ();
		sounds.Play ();
	}

	void Update(){
		floorNumber.text = "" + floorCheck;
		if (up)
			speed = .02f;
		if (down)
			speed = -.02f;
	}

	void FixedUpdate () {
		
		if (!stop) {
			elevator.transform.Translate (0, speed, 0);
			sounds.mute = false;
		} else
			sounds.mute = true;

	}
	void OnTriggerEnter(Collider other){

		if (other.tag == ("Floor 1")||other.tag == ("Floor")||other.tag == ("Floor 3")) {
			stop = true;
			up = false;
			down = false;
		//	sounds.PlayOneShot (ding);
		}
		if (other.tag == ("Floor 1"))
			floorCheck = 1;
		if (other.tag == ("Floor"))
			floorCheck = 2;
		if (other.tag == ("Floor 3"))
			floorCheck = 3;
	}

	void OnTriggerStay(Collider other){
		
		if (floorCheck !=1 && left && other.tag == ("Player") && Input.GetButtonDown("Use")) {
			stop = false;
			up = false;
			down = true;
		}
		if (floorCheck !=3 && right && other.tag == ("Player") && Input.GetButtonDown("Use")) {
			stop = false;
			down = false;
			up = true;
		}

	}
}
