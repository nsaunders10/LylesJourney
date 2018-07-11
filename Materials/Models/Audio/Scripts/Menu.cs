using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject graphicsMenu;
	public bool inGame;
	public GameObject camera;
	public static bool pause;
	public Rigidbody suit;


	void Start () {
	mainMenu.SetActive (true);
	}

	void FixedUpdate () {
		/*if (Input.GetButtonDown ("Pause") && inGame) {
		//	pause = !pause;
		//	mainMenu.SetActive (true);
		}

		if (pause) {
			Time.timeScale = 4.0f;
		//	camera.SetActive (false);

		} else {
			Time.timeScale = 1.0f;
			if(!inGame)
			mainMenu.SetActive (false);
		//	camera.SetActive (true);
		}
		if (!inGame) {
			Time.timeScale = 1.0f;
		}*/
		if(!inGame)
		suit.AddRelativeForce (2, 0, 0);
	}
	public void StartGame(){
		SceneManager.LoadScene (1);
		inGame = true;
	}
	public void MainMenu(){
		SceneManager.LoadScene (0);
	}
	public void Graphics(){
		mainMenu.SetActive (false);
		graphicsMenu.SetActive (true);
	}
	public void Exit(){
		Application.Quit();
	}
	public void Back(){
		mainMenu.SetActive (true);
		graphicsMenu.SetActive (false);
		pause = false;
	}

	public void Fastest(){
		QualitySettings.SetQualityLevel(0,true);
	}
	public void Fast(){
		QualitySettings.SetQualityLevel(1,true);
	}
	public void Simple(){
		QualitySettings.SetQualityLevel(2,true);
	}
	public void Good(){
		QualitySettings.SetQualityLevel(3,true);
	}
	public void Beautiful(){
		QualitySettings.SetQualityLevel(4,true);
	}
	public void Fantastic(){
		QualitySettings.SetQualityLevel(5,true);
	}
}
