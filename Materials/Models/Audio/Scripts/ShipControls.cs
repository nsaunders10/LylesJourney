using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipControls : MonoBehaviour {

	Rigidbody ship;
	AudioSource sound;
	public AudioClip air;
	public GameObject shipHud;
	public GameObject[] thruster;
	public GameObject interior;
	public GameObject player;
	public GameObject shipCam;
	public GameObject pressE;
	public GameObject RCS;
	public Transform newTrans;
	public float trailSize;
	public bool controlRoom;
	public bool driving;
	public Slider shipPower;
	public Toggle Rcs;
	bool rcsToggle;
	float power;
	float x;
	float zStrafe;
	float y;
	float z;
	public int speed;
	public int strafeSpeed;
	public int yawSpeed;
	public int pitchSpeed;

	void Start () {
		ship = GetComponent<Rigidbody>();
		shipCam.SetActive (false);
		shipHud.SetActive (false);
		sound = GetComponent<AudioSource> ();
	}

	void Update () {

		shipPower.value = power;
		Rcs.isOn = rcsToggle;

		if (controlRoom && Input.GetKeyDown("e")) {
			driving = true;
			shipCam.SetActive (true);
			shipHud.SetActive (true);
			player.transform.position = newTrans.position;
		}

		if (driving) {
			
			isDriving ();
			StartCoroutine (Driving ());
		}
		if (!driving) {
		//	ship.velocity = new Vector3 (0, 0, 0);
			interior.SetActive (true);
			shipCam.SetActive (false);
			shipHud.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == ("Player")) {
			controlRoom = true;
			pressE.SetActive (true);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == ("Player")) {
			controlRoom = false;
			pressE.SetActive (false);
		}
	}

	IEnumerator Driving(){
		
		yield return new WaitForSeconds (.5f);
			
		if (driving && Input.GetKeyDown("e")) {
			driving = false;
			controlRoom = false;
		}
	}

	public void isDriving(){

		if (Input.GetKeyDown ("r")) {
			rcsToggle = !rcsToggle;
			ship.velocity = new Vector3 (0, 0, 0);
			transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
			Instantiate (RCS,transform.position,RCS.transform.rotation);
			sound.PlayOneShot(air);
		}

		EPower ();
		shipCam.SetActive (true);
		shipHud.SetActive (true);
		interior.SetActive (false);
		pressE.SetActive (false);
		x = Input.GetAxis ("Vertical") * speed * power;
		y = Input.GetAxis ("Mouse X") * yawSpeed;
		z = Input.GetAxis ("Mouse Y") * pitchSpeed;
		zStrafe = Input.GetAxis ("Horizontal") * strafeSpeed * power;
		thruster[0].transform.localScale = new Vector3 (x+power/trailSize, 1, 1);
		thruster[1].transform.localScale = new Vector3 (x+power/trailSize, 1, 1);

		if (Input.GetButton ("Fire2")) {
			transform.Rotate (0, y, -z);
		}
		else transform.Rotate (-y, 0, -z);

		if (rcsToggle) {
		transform.Translate (x, 0, -zStrafe, Space.Self);
		if (Input.GetKey ("space")) transform.Translate (0, strafeSpeed, 0);
		if (Input.GetKey (KeyCode.LeftControl))transform.Translate (0, -strafeSpeed, 0);
			ship.isKinematic = true;
		} else {
			ship.isKinematic = false;
			ship.AddRelativeForce (x * 50, 0, -zStrafe * 50);

			if (Input.GetKey ("space")) {
				ship.AddRelativeForce (0, x * 50, 0);
			}
			if (Input.GetKey (KeyCode.LeftControl)) {
				ship.AddRelativeForce (0, -x * 50, 0);
			}
		}

	}

	public void EPower(){
		float val = Input.GetAxis ("Mouse ScrollWheel");
			power += val;
		if (power < 0)
			power = .01f;
		if (power > 5)
			power = 5;
			
	}
}
