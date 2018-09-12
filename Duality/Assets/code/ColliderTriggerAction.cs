using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerAction : MonoBehaviour {

	/*	trigger action types
		-convo, triggers a conversation
		-toggleCanSwitch, <--
		-employ, turn on subordinate action
		-destroy, destroy an object
		-restartLevel,  ▒▒▒▒▒▒▒▒▄▄▄▄▄▄▄▄▒▒▒▒▒▒▒▒ 
						▒▒▒▒▒▄█▀▀░░░░░░▀▀█▄▒▒▒▒▒ 
						▒▒▒▄█▀▄██▄░░░░░░░░▀█▄▒▒▒ 
						▒▒█▀░▀░░▄▀░░░░▄▀▀▀▀░▀█▒▒ 
						▒█▀░░░░███░░░░▄█▄░░░░▀█▒ 
						▒█░░░░░░▀░░░░░▀█▀░░░░░█▒ 
						▒█░░░░░░░░░░░░░░░░░░░░█▒ 
						▒█░░██▄░░▀▀▀▀▄▄░░░░░░░█▒ 
						▒▀█░█░█░░░▄▄▄▄▄░░░░░░█▀▒ 
						▒▒▀█▀░▀▀▀▀░▄▄▄▀░░░░▄█▀▒▒ 
						▒▒▒█░░░░░░▀█░░░░░▄█▀▒▒▒▒ 
						▒▒▒█▄░░░░░▀█▄▄▄█▀▀▒▒▒▒▒▒ 
						▒▒▒▒▀▀▀▀▀▀▀▒▒▒▒▒▒▒▒▒▒▒▒▒
		-startTimedText, npc text start
		-changeToLevel, insert snark
		-toggleBarrierState, turns on and off walls




		-, interntionally nothing
	*/

	public bool oneTime;

	public bool timedTextWithArgument;

	// name of the specified tags (if specified), split by "
	public string tagNames;

	public string type;

	public TimedText timedText;

	public TextAsset script;

	public GameObject destroyObject;

	public SharedCharacters sc;

	public Barrier barrier;

	bool destroyed;

	// ": separates lines
	// BBB: blue line
	// RRR: red line

	private string[] lines;
	private string[] tagNameList;
	private bool empty;

	private ArrayList blueLines; 
	private ArrayList redLines;

	void Start () {

		if (tagNames.Length > 0)
			tagNameList = tagNames.Split ('"');
		else
			empty = true;
		switch (type) {
		case "convo":
			blueLines = new ArrayList ();

			redLines = new ArrayList ();



			lines = script.text.Split ('"');

			foreach (string x in lines) {

				if (x.Substring (0, 3).Equals ("BBB"))
					blueLines.Add (x.Substring (3));
				else
					redLines.Add (x.Substring (3));
				
			}
			break;
		case "destroy":
			destroyed = false;
			break;
		}

	}

	void OnTriggerEnter2D (Collider2D other) {

		bool acceptedString = false;
		if (!empty) {
			foreach (string x in tagNameList) {
				if (x.Equals (other.tag))
					acceptedString = true;
			}
		}

		if ((empty || acceptedString) && !other.isTrigger) {
			switch (type) {
			case "convo":
				sc.turnOnConvo (redLines, blueLines);
				checkOff ();
				break;
			case "startTimedText":
				if (timedTextWithArgument)
					timedText.startText (script);
				else
					timedText.startText ();
				checkOff ();
				break;
			case "toggleCanSwitch":
				sc.toggleCanSwitch (!sc.getCanSwitch ());
				checkOff ();
				break;
			case "employ":
				other.gameObject.GetComponent<Subordinate> ().setEmployed (true);
				checkOff ();
				break;
			case "destroy":
				if (!destroyed) {
					Destroy (destroyObject);
					destroyed = true;
				}
				checkOff ();
				break;
			case "restartLevel":
				UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
				checkOff ();
				break;
			case "toggleBarrierState":
				barrier.setWall (!barrier.wallIsActivated());
				checkOff ();
				break;
			}
		}
	}

	private void checkOff() {

		if (oneTime)
			type = "";

	}

}
