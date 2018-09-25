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

	public bool blueIsGone;
	public bool redIsGone;

	public int nextLevel;

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

			if(!blueIsGone)
				blueTextMesh.text = "";
			if(!redIsGone)
				redTextMesh.text = "";

		} else {

			if(!blueIsGone)
				blueTextMesh.text = (string) blueScript[currentBlueLine];
			if(!redIsGone)
				redTextMesh.text = (string) redScript[currentRedLine];

		}
			

		GameObject currentlyControlled = (isControllingBlue) ? blue : red;
		GameObject currentlyUncontrolled = (!isControllingBlue) ? blue : red;

		if (redIsGone) {

			if (blueIsGone) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (nextLevel);
				return;
			} else {
				currentlyControlled = blue;
				currentlyUncontrolled = red;
			}

		} else if (blueIsGone) {
			currentlyControlled = red;
			currentlyUncontrolled = blue;
		}
			

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

		if (!(blueIsGone || redIsGone))
			currentlyUncontrolled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		cameraObject.transform.position = new Vector3 (currentlyControlled.transform.position.x, currentlyControlled.transform.position.y, -10);

	}

	/// <summary>
	/// Turns the on convo. the red and blue lines should be of about equal length.
	/// </summary>
	/// <param name="_redScript"> all red lines of type string </param>
	/// <param name="_blueScript">all blue lines of type string</param>
	public void turnOnConvo (ArrayList _redScript, ArrayList _blueScript) {

		redScript = _redScript;
		blueScript = _blueScript;

		convoTriggered = true;
		inText = true;

	}

	/// <summary>
	/// Closes the convo.
	/// </summary>
	public void closeConvo () {

		inText = false;

		currentRedLine = 0;
		currentBlueLine = 0;

	}

	/// <summary>
	/// Gets the is controlling blue.
	/// </summary>
	/// <returns><c>true</c>, if is controlling blue was gotten, <c>false</c> otherwise.</returns>
	public bool getIsControllingBlue () {

		return isControllingBlue;

	}

	/// <summary>
	/// Toggles the whether or not the player can switch between characters. (if neither character is gone)
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void toggleCanSwitch (bool state) {

		if (!(redIsGone || blueIsGone))
			canSwitch = state;

	}

	/// <summary>
	/// Gets the can switch.
	/// </summary>
	/// <returns><c>true</c>, if can switch was gotten, <c>false</c> otherwise.</returns>
	public bool getCanSwitch () {
		return canSwitch;
	}

	/// <summary>
	/// Removes the blue character and turns canSwitch false.
	/// </summary>
	public void removeBlue () {

		blueIsGone = true;
		canSwitch = false;
		isControllingBlue = false;
		Destroy (blue);

	}

	/// <summary>
	/// Removes the red character and turns canSwitch false.
	/// </summary>
	public void removeRed () {

		redIsGone = true;
		canSwitch = false;
		isControllingBlue = true;
		Destroy (red);

	}
}