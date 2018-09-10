using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCharacters : MonoBehaviour {

	public GameObject red;
	public GameObject blue;

	public TextMesh blueTextMesh;
	public TextMesh redTextMesh;

	public GameObject cameraObject;

	public bool startSwitchOff;

	bool inText;

	// ugh I ain't feeling it today bois TODO change later
	bool convoTriggered;

	int currentBlueLine;
	int currentRedLine;

	ArrayList blueScript;
	ArrayList redScript;

	public float speed;

	bool isControllingBlue;

	bool canSwitch;

	void Start () {

		isControllingBlue = true;

		closeConvo ();

		canSwitch = !startSwitchOff;

		//Debug.Log (currentBlueLine);
		//Debug.Log (currentBlueLine +=1);
		//Debug.Log (currentBlueLine);
		//blueScript = new ArrayList ();
		//redScript = new ArrayList ();

	}
	
	void Update () {

		if (!inText) {

			blueTextMesh.text = "";
			redTextMesh.text = "";

		} else {

			blueTextMesh.text = (string) blueScript[currentBlueLine];
			redTextMesh.text = (string) redScript[currentRedLine];

		}
			

		GameObject currentlyControlled = (isControllingBlue) ? blue : red;
		GameObject currentlyUncontrolled = (!isControllingBlue) ? blue : red;

		if (Input.GetKey (KeyCode.W)) {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (currentlyControlled.GetComponent <Rigidbody2D> ().velocity.x, speed);
		} else if (Input.GetKey (KeyCode.S)) {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (currentlyControlled.GetComponent <Rigidbody2D> ().velocity.x, -speed);
		} else {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (currentlyControlled.GetComponent <Rigidbody2D> ().velocity.x, 0);
		}

		if (Input.GetKey (KeyCode.D)) {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (speed, currentlyControlled.GetComponent <Rigidbody2D> ().velocity.y);
		} else if (Input.GetKey (KeyCode.A)) {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (-speed, currentlyControlled.GetComponent <Rigidbody2D> ().velocity.y);
		} else {
			currentlyControlled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, currentlyControlled.GetComponent <Rigidbody2D> ().velocity.y);
		}

		if (Input.GetKeyDown (KeyCode.Space) && canSwitch) {

			isControllingBlue = !isControllingBlue;

			if (inText) {

				ArrayList controlledList = (isControllingBlue) ? blueScript : redScript;

				int correspondingInt;
				if (convoTriggered) {
					correspondingInt = 0;
					convoTriggered = false;
				} else {
					correspondingInt = (isControllingBlue) ? currentBlueLine += 1 : currentRedLine += 1;
				}

				if (controlledList.Count  <= correspondingInt)
					closeConvo ();
					


			}
				

		}

		currentlyUncontrolled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		cameraObject.transform.position = new Vector3 (currentlyControlled.transform.position.x, currentlyControlled.transform.position.y, -10);

	}

	public void turnOnConvo (ArrayList _redScript, ArrayList _blueScript) {

		redScript = _redScript;
		blueScript = _blueScript;

		convoTriggered = true;
		inText = true;

	}

	public void closeConvo () {

		inText = false;

		currentRedLine = 0;
		currentBlueLine = 0;

	}

	public bool getIsControllingBlue () {

		return isControllingBlue;

	}

	public void toggleCanSwitch (bool state) {

		canSwitch = state;

	}

	public bool getCanSwitch () {
		return canSwitch;
	}
}