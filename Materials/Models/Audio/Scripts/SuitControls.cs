using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuitControls : MonoBehaviour {

	Rigidbody suit;
	AudioSource sound;
	//GUI for suit
	public Text velocityX;
	public Text velocityY;
	public Text velocityZ;
	public Text eulerX;
	public Text eulerY;
	public Text eulerZ;
	public Text RDB1;
	public Text RDB2;
	public Text RDB3;
	public Slider verThrust;
	public Slider horThrust;
	public Slider UDThrust;

	public AudioClip air;
	public GameObject suitCam;
	public GameObject player;
	public static bool driving;
	float mouseX;
	float mouseY;
	float vertical;
	float horizontal;
	public float speed;
	public float sensitivity;
	bool returnSuit;
	Vector3 suitOrg;
	Vector3 playerOrg;
	Quaternion suitRot;
	int count = 0;
	float updownT;
	int RDB;

	void Start () {
		suit = GetComponent<Rigidbody>();
		sound = GetComponent<AudioSource> ();
		suitOrg = this.transform.position;
		suitRot = this.transform.rotation;
	}

	void FixedUpdate () {
		
		velocityX.text = "X:" + suit.velocity.x;
		velocityY.text = "Y:" + suit.velocity.y;
		velocityZ.text = "Z:" + suit.velocity.z;
		eulerX.text = "X:" + transform.eulerAngles.x;
		eulerY.text = "Y:" + transform.eulerAngles.y;
		eulerZ.text = "Z:" + transform.eulerAngles.z;
		RDB1.text = "RDB1:" + RDB / 56;
		RDB2.text = "RDB2:" + RDB / 10;
		RDB3.text = "RDB3:" + RDB / 17;
		verThrust.value = Mathf.Abs(vertical);
		horThrust.value = Mathf.Abs(horizontal);
		UDThrust.value = updownT;

		RDB = Random.Range (0, 1000);
		
		if (driving) {
			Interaction.unlock4 = true;
			InUse ();
			suit.isKinematic = false;
		}
		if (!driving) {
			suit.isKinematic = true;
			this.transform.position = suitOrg;
			this.transform.rotation = suitRot;
			if (!GravityToggle.gravOn) {
				Interaction.unlock4 = false;
			}
		}
		if (returnSuit && driving && Input.GetButtonDown ("Use") && count == 1) {
			driving = false;
			player.transform.position = playerOrg;
			player.SetActive (true);
			suitCam.SetActive (false);
			count = 0;
		}
	}

	void OnTriggerStay(Collider other){
		
		if (other.tag == ("Player") && Input.GetButtonDown("Use")&& Interaction.unlock3) {
			driving = true;
			playerOrg = player.transform.position;
		}
		if (other.tag == ("Suit Hanger")) {
			returnSuit = true;
		}

	}
	void OnTriggerExit(Collider other){
		if (other.tag == ("Suit Hanger")) {
			returnSuit = false;
			count = 1;
		}

	}
	public void InUse(){
		suitCam.SetActive (true);
		player.SetActive (false);
		player.transform.position = this.transform.position;
		
		vertical = Input.GetAxis ("Vertical") * speed;
		horizontal = Input.GetAxis ("Horizontal") * sensitivity;
		mouseY = Input.GetAxis ("Mouse Y") *  sensitivity;;
		mouseX = Input.GetAxis ("Mouse X") *  sensitivity;

		transform.Rotate (-mouseY, mouseX, -horizontal);

		suit.AddRelativeForce (0, 0, vertical);

		if (Input.GetKey ("space")) {
		suit.AddRelativeForce (0, speed, 0);
			updownT = speed;
		}

		if (Input.GetKey (KeyCode.LeftControl)) {
			suit.AddRelativeForce (0, -speed, 0);
			updownT = speed;
		}
		if (Input.GetKeyUp (KeyCode.LeftControl) || Input.GetKeyUp ("space"))updownT = 0;

		if(vertical!=0 || horizontal!=0 || Input.GetKey ("space") || Input.GetKey (KeyCode.LeftControl)){
			
			sound.PlayOneShot(air,0.05f);
		}
	}
		
}
