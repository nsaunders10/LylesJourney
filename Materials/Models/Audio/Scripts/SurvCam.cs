using UnityEngine;
using System.Collections;

public class SurvCam : MonoBehaviour {

	public Transform player;
	public GameObject light;
	public GameObject sparks;
	public bool fixableCamera;
	bool on;
	int randy;

	void Start () {
		if (!Interaction.leefyOn)
			on = false;
	}
	void Update () {

		if (Interaction.leefyOn)
			on = true;
		
		if (on && !fixableCamera) {
			transform.LookAt (player);
			light.SetActive (true);
		}
		randy = Random.Range (1, 60);

		if (fixableCamera && !Interaction.camerasOn) {
			if (randy == 2)
				Instantiate (sparks, gameObject.transform.position, gameObject.transform.rotation);	
		} else 
			if (fixableCamera && Interaction.camerasOn) {
			transform.LookAt (player);
			light.SetActive (true);
		}
		
	}
}
