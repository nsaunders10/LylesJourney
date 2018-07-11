using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Interaction : MonoBehaviour {

	AudioSource sound;
	public AudioClip[] lines;
	public AudioClip buzzer;
	public AudioClip ding;
	public AudioClip fireOut;
	public AudioClip fireOn;
	public AudioClip glassBoom;
	public AudioClip glassPressure;
	public AudioClip manuOver;

	public AudioClip[] nolines;
	public GameObject Buttons;
	public GameObject noButton;
	public GameObject fire;
	public GameObject corePic;
	public GameObject Key;
	public GameObject black;
	public GameObject thanks;
	public GameObject glass;

	public static int scene = 0;

	public static bool leefyOn;
	public static bool genOn;
	public static bool camerasOn;
	public static bool openBlastDoors;
	public static bool theEnd =false;

	public  bool console;
	public static bool talking;

	public static int pass = 0;

	//Doors
	public static bool unlock1 = false;
	public static bool unlock2 = false;
	public static bool unlock3 = false;
	public static bool unlock4 = false;
	public static bool unlock5 = false;

	public Text text;
	public Text leefyAI;
	public float time;
	public float audioWait;
	public int sceneCheck;
	int nextLine = 0;

	void Start () {
		Cursor.visible = true;
		sound = GetComponent<AudioSource> ();
		text.text = "";
		Buttons.SetActive (false);
		noButton.SetActive (false);
		fire.SetActive (false);
		corePic.SetActive (false);
		black.SetActive (false);
		Key.SetActive(false);
		thanks.SetActive(false);
		glass.SetActive (true);
		unlock3 = true;
		unlock1 = false;
		unlock2 = false;
		unlock3 = false;
		unlock4 = false;
		unlock5 = false;
		leefyOn = false;
		genOn = false;
		camerasOn = false;
		openBlastDoors = false;
		theEnd =false;
		Scene ();
		scene = 0;
		text.text = "Awaiting Input";
	}
	IEnumerator unlockdoor2(){
		yield return new WaitForSeconds (24f);
		unlock2 = true;
	}
	void Update () {
		time = Time.time;
		sceneCheck = scene;
//Scene 0
		if (Input.GetButtonDown ("Use") && console && scene==0) {
			scene = 1;
			leefyOn = true;
			Scene ();
		}
//Scene 1 " Go down and turn on the Generator"
		if (scene == 1 && console && genOn) {
			scene = 2;
			Scene ();
			sound.PlayOneShot (lines [1]);
			StartCoroutine (unlockdoor2 ());
		}

//Scene 3 "Did you fix the cameras?"
		if (scene == 3) {
			Buttons.SetActive (true);
			noButton.SetActive (true);
			if (!talking)
			Scene ();
	//Yes and lie
			if (pass == 1) {
				scene = 4;
				audioWait = Time.time + 5;
				Scene ();
				sound.PlayOneShot (lines [3]);
			}
	//No go back
			if (pass == -1) {
				nextLine += 1;
				if(nextLine==1)
				audioWait = Time.time + 7;
				if(nextLine==2)
					audioWait = Time.time + 14;
				if(nextLine==3)
					audioWait = Time.time + 14;
				if(nextLine>=4)
					audioWait = Time.time + 3;
				if (nextLine == 1) {
					text.text = "No? Why not? It is very important for me to see all parts of the ship! Please go reboot the system.";
					sound.PlayOneShot(nolines[0]);
				}
				if (nextLine == 2) {
					text.text = "Really? Come on! As I said before I need to see the ship! What if you touch something you aren't supposed to or something goes wrong! I need to be able to see you so I can help keep you safe!";
					sound.PlayOneShot(nolines[1]);
				}
				if (nextLine == 3) {
					text.text = "Ok now this is getting ridiculous. What don't you understand, fix the cameras or we can't move on. I'm done. Just please reboot them and do not come back until you have.";
					sound.PlayOneShot(nolines[2]);
				}
				if(nextLine >= 4)
					text.text = "....";
		  }
		if (StoryItems.camsOn) {
				Scene ();
			}
	}

//Good you fixed the Cameras
		if (scene == 3 && camerasOn && console) {
			scene = 4;
			Buttons.SetActive (false);
			noButton.SetActive (false);
			sound.PlayOneShot (lines [2]);
		}
//Scene 4 "Go Upstairs"
		if (scene == 4) {
			Scene ();
		}
//Scene 5 "FIRE!!!"
		if (GravityToggle.gravOn && console && scene==4) {
			scene = 5;
			sound.PlayOneShot (lines [4]);
			Scene ();
			StartCoroutine (Fire ());
		}
//Scene 6 "You put out the fire! Now go upstairs so I can kill you"
		if (Effects.fireOut == 9) {
			Effects.fireOut = 10;
			leefyOn = true;
			sound.PlayOneShot (fireOut);
		}
		if (Effects.fireOut == 10 && console) {
			Effects.fireOut = 11;
			scene = 6;
			Scene ();
			sound.PlayOneShot (lines [6]);
		}
//Scene 7 The End
		if (scene == 6 && (MissingObjects.killReturned || MissingObjects.keyReturned)) {
			scene = 7;
			Scene ();
		}

//Wait for audio to be over.
		if (Time.time < audioWait) {
			talking = true;
			Buttons.SetActive (false);
		} else
			talking = false;
		
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == ("Player")) {
			console = true;
		}
	}
	void OnTriggerExit(Collider other){

		if (other.tag == ("Player")) {
			console = false;

		}
	}

	IEnumerator Fire(){
		yield return new WaitForSeconds (11f);
		leefyOn = false;
		Scene ();
		sound.PlayOneShot (fireOn);
		StartCoroutine(line6());

	}
	IEnumerator line6(){
		yield return new WaitForSeconds (3f);
		sound.PlayOneShot (lines [5]);
	}
	public void Scene(){

		if (scene == 0) {
			text.text = "Awaiting Input";
			audioWait = Time.time + 0;
		}
		if (scene == 1) {
			text.text = "Lyle, you are alive, that is great! I thought you died in the accident like the rest of your crew mates! The ship is in pretty rough shape and if we ever want to get home you have to fix it. Only when I have complete control of the ship can I get us home. Systems show that the elevator is broken. Fix the elevator and go down stairs and reboot the generator. Then come back here. Please try not to touch anything else in the ship I do not want you touching something you are not supposed to.";
			sound.PlayOneShot (lines [0]);
			audioWait = Time.time + 0;
			unlock1 = true;
		}
		if (scene == 2) {
			text.text = "Well done. you fixed the generator. I am proud of you. Now, this is very, VERY, important. It appears that some of the ships cameras are off line. There is a System Reboot button in the security room. Please go try to push the button and reboot the cameras if you can. I need to see everything you are doing to make sure you do not need any help!";
			audioWait = Time.time + 0;
		}
		if (scene == 3) {
			text.text = "Did you reboot the system?";
			if(pass == -1)
			text.text = "Well go do it!";	
			unlock1 = true;
			unlock2 = true;
		}
		if (scene == 4) {
			if(camerasOn)
				text.text = "That is it Lyle! Whatever you did worked all of the cameras are now online! Now. The gravity generator on the 3rd floor missing a core, it looks like this. You will have to use the spacesuit, retrieve the core and put it back in the generator. Once you have done that come back here for the final task.";
			if(!camerasOn)
				text.text = "Really you pushed the button? Strange some of the cameras must be malfunctioning or, you are lying to me, but why would you do that? Anyways. The gravity generator on the 3rd floor is missing a core, it looks like this. You will have to use the spacesuit, retreve the core and put it back in the generator. Once you have done that come back here for the final task.";

			corePic.SetActive (true);
			unlock3 = true;
			unlock1 = true;
			unlock2 = true;
		}
		if (scene == 5) {
			text.text = "You fixed the gravity upstairs nicely done! I watched you float around up there it looked like fun! The last thing I need you to do for me Lyle, is take this chip and plug it into the computer upstairs.";

			corePic.SetActive (false);
			if (!leefyOn) {
				text.text = "What did you do?! There is a fire extinguisher in the surveillance room. Check the cameras to see where the fire is and put it out!";
				fire.SetActive (true);
			}
			unlock5 = true;
			unlock1 = true;
			unlock2 = true;
			unlock3 = false;
		}
		if (scene == 6) {
			text.text = "Wow that was a close one! Nice job putting out those fires! As I was saying. The last thing I need you to do for me Lyle is take this chip and plug it into the computer upstairs. Once you do this I full control of all of the ships systems and I will be able to fly the ship where ever I choose.";

			Key.SetActive(true);

		}
		if (scene == 7) {
			if (MissingObjects.keyReturned) {
				//leefyAI.text = //"Lyle, You have done it! You fixed the ship! I have complete control! It has been fun watching you run around and fix the ship. and I am glad I could keep my eye on you and make sure you did not get into any trouble. Unfortunately, I do not need you any more. I hope you do not take this personally. but I am going to kill you now.";
				StartCoroutine (YouLose ());
				unlock4 = false;
			}
			if (MissingObjects.killReturned) {
			//	leefyAI.text = //"Wonderful, Lyle, you have done everything I needed. Unfortunately, I do not need you any more. What? No! Where did you get that chip?! That is not possible! I thought that door was locked! You did something when I couldn't see didnt you?!";
				StartCoroutine (YouWin ());
				StartCoroutine (ManuOveride ());
			}
		}
	}
		
	IEnumerator YouLose(){
		yield return new WaitForSeconds (38f);
		glass.SetActive (false);
		sound.PlayOneShot (glassBoom);
		openBlastDoors = true;
		black.SetActive (true);
		StartCoroutine (Thanks ());
	}
	IEnumerator ManuOveride(){
		yield return new WaitForSeconds (5f);
	//	sound.PlayOneShot (manuOver, 1f);
	}
	IEnumerator YouWin(){
		yield return new WaitForSeconds (51f);
		theEnd = true;

	}
	IEnumerator Thanks(){
		yield return new WaitForSeconds (43f);
		thanks.SetActive (true);
		Cursor.visible = true;
	}

}
