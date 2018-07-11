using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Effects: MonoBehaviour {

	public GameObject[] light;
	public GameObject sparks;
	public GameObject alarms;
	public bool fire;
	public bool alarmLight;
	public bool flicker;
	public bool elevator;
	public bool blink;
	public int blinkSpeed;
	public float spinSpeed;
	public static int fireOut;
	int fireTime = 1;
	bool ready = true;
	int randy;


	void Start () {
		ready = true;
		if(blink)
		StartCoroutine(CarLightOn());
		fireOut = 0;
		if (fire)
		gameObject.SetActive (true);
	}

	void Update () {

		if (alarmLight && !Interaction.leefyOn) {
			transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
			alarms.SetActive (true);
		}
		if (alarmLight && Interaction.leefyOn) {
			alarms.SetActive (false);
		}

		randy = Random.Range (1, 60);

		if (flicker && !Interaction.leefyOn) {
			if (randy == 2) {
				LightsOn ();
			} else
				LightsOFF ();
		} else
			if(flicker && Interaction.leefyOn)LightsOn ();
		
	}
	void OnTriggerStay(Collider other){
		
		if (other.tag == ("Spray") && fire) {
			
			if (fireTime != 0 && ready) {
				StartCoroutine (PutOutFire ());
			}
		}
	}
	IEnumerator PutOutFire(){
		ready = false;
		yield return new WaitForSeconds (0.1f);
		fireTime -= 1;
		ready = true;
		fireOut += 1;
		if(fireTime == 0)
		gameObject.SetActive (false);
	}
	public void LightsOn(){
		
		light[0].SetActive (true);

		if(!Interaction.leefyOn)
		Instantiate (sparks, gameObject.transform.position, gameObject.transform.rotation);
		
		if(elevator && !MissingObjects.elevatorFixed && randy == 2)
			Instantiate (sparks, gameObject.transform.position, gameObject.transform.rotation);
	}
	public void LightsOFF(){
		light[0].SetActive (false);
	}

	IEnumerator CarLightOn(){
		light[0].SetActive (true);
		yield return new WaitForSeconds (blinkSpeed);
		StartCoroutine(CarLightOff());
	}
	IEnumerator CarLightOff(){
		light[0].SetActive (false);
		yield return new WaitForSeconds (blinkSpeed);
		StartCoroutine(CarLightOn());
	}
}
