using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	public float destroyTime;

	void Start () {
		StartCoroutine (DestroyByTime ());
	}
	

	void Update () {
	
	}

	IEnumerator DestroyByTime(){
		
		yield return new WaitForSeconds(destroyTime);
			
			Destroy(this.gameObject);
	}
}
