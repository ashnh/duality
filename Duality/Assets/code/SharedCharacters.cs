using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCharacters : MonoBehaviour {

	public GameObject red;
	public GameObject blue;

	public TextMesh x;

	public GameObject cameraObject;

	public float speed;

	bool isControllingBlue;

	void Start () {

		isControllingBlue = true;


	}
	
	void Update () {

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

		if (Input.GetKeyDown (KeyCode.Space))
			isControllingBlue = !isControllingBlue;

		currentlyUncontrolled.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		cameraObject.transform.position = new Vector3 (currentlyControlled.transform.position.x, currentlyControlled.transform.position.y, -10);

	}

	public void displayText (string text) {

	}

	public bool getIsControllingBlue () {

		return isControllingBlue;

	}
}
