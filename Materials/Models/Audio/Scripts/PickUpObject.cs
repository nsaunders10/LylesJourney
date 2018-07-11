using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

	public Collider player;
	public Transform hands;
	public Transform spaceHands;
	public GameObject rightClickText;
	public bool fireExtinguisher;
	public bool key;
	public bool kill;
	bool isHolding = false;
	bool held;
	int randy;
	Rigidbody rb;
	AudioSource sound;
	public AudioClip[] collide;

	void Start () {
	
		rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
		held = false;
	}

	void Update () {

		if (isHolding) {
			gameObject.transform.position = hands.position;
			gameObject.transform.rotation = hands.rotation;
			rb.velocity = new Vector3 (0, 0, 0);
			if(!fireExtinguisher)
			Physics.IgnoreCollision (this.GetComponent<Collider> (), player, true);
		} 
		if(!isHolding) {
			gameObject.transform.position = gameObject.transform.position;
			gameObject.transform.rotation = gameObject.transform.rotation;
			if(!fireExtinguisher)
			Physics.IgnoreCollision (this.GetComponent<Collider> (), player, false);
		}
		if (isHolding && fireExtinguisher) {
			StoryItems.fireExtinguisher = true;
			rightClickText.SetActive (true);
		}
		if (!isHolding && fireExtinguisher) {
			StoryItems.fireExtinguisher = false;
			rightClickText.SetActive (false);
		}
		if (MissingObjects.keyReturned && key) {
			isHolding = false;
		}
		if (MissingObjects.killReturned && kill) {
			isHolding = false;
		}
			
		if (SuitControls.driving) {
			if (isHolding) {
				gameObject.transform.position = spaceHands.position;
				gameObject.transform.rotation = spaceHands.rotation;
				rb.velocity = new Vector3 (0, 0, 0);
			} 
		}

		if (Input.GetButtonDown ("Use")) {
			isHolding = false;
	
		}
	}

	void OnTriggerStay(Collider other){
		
		if (other.tag == ("Hands") && Input.GetButtonUp("Use") && !held) {
			isHolding = true;
			held = true;
		}
	}
	void OnTriggerEnter(Collider other){

		if (other.tag == ("Hands")) {
			held = false;
		}
	}
	void OnTriggerExit(Collider other){

		if (other.tag == ("Hands")) {
			held = false;
		}
	}
		
	void OnCollisionEnter(Collision other){
		
		randy = Random.Range (0, 2);		
		if(randy ==0)
		sound.PlayOneShot (collide[0], 0.1f);
		if(randy ==1)
		sound.PlayOneShot (collide[1]);

	 }
		
	void OnCollisionStay(Collision other){
		
		if (other.gameObject.tag == ("Walls") && held)
			isHolding =true;
	}

}
