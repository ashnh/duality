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






		-, interntionally nothing
	*/

	public bool oneTime;

	// name of the specified tag (if specified)
	public string tagName;

	public string type;

	public TextAsset script;

	public GameObject destroyObject;

	public SharedCharacters sc;

	bool destroyed;

	// ": separates lines
	// BBB: blue line
	// RRR: red line

	private string[] lines;

	private ArrayList blueLines; 
	private ArrayList redLines;

	void Start () {
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
		if (other.tag.Equals (tagName) || tagName.Equals("")) {
			switch (type) {
			case "convo":
				sc.turnOnConvo (redLines, blueLines);
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
				break;
			case "restartLevel":
				UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
				break;
			}
		}
	}

	private void checkOff() {

		if (oneTime)
			type = "";

	}

}
