using UnityEngine;
using System.Collections;

public class BlackFade : MonoBehaviour {

	Color lerper;
	Renderer rend;
	public float duration;
	float t = 0;

	void Start () {
		rend = GetComponent<Renderer> ();
	//	rend.material.shader = Shader.Find("Standard");
	}

	void Update () {
		
		if (t < 1){ 
			t += Time.deltaTime/duration;
		}
		//lerper = Color.Lerp (new Color(0,0,0,0), Color.green, t);

		rend.material.color = Color.Lerp (new Color(0,0,0,0), new Color(0,0,0,1), t);
		//rend.material.SetColor ("_SpecColor",lerper);

	}
}
