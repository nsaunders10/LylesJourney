using UnityEngine;
using System.Collections;

public class GravityToggle : MonoBehaviour {

	Rigidbody rb;
	public static bool gravOn = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		gravOn = false;
	}

	void Update () {

		if (gravOn) {
			rb.useGravity = true;
			Interaction.unlock4 = true;
		} else 
			rb.useGravity = false;
		
		if (Interaction.openBlastDoors) {
			Physics.gravity = new Vector3 (5f, .5f, 0);
		}

	}
	void FixedUpdate(){


	}

	void OnTriggerStay(Collider other){
		
		if (other.tag == ("Spray")) {
			rb.AddRelativeForce (Vector3.right);
		}
	}
}
