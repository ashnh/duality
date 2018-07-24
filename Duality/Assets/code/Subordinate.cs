using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordinate : MonoBehaviour {

	public bool blue;

	public float speed;
	public float slowDownDistance;
	public float controlDistance;

	public SharedCharacters sc;

	public GameObject redObject;
	public GameObject blueObject;

	public Camera cameraObject;

	bool awakened;
	bool massMovement;

	bool fightOrdered;

	// Use this for initialization
	void Start () {
		
		awakened = false;
		massMovement = false;
		fightOrdered = false;

	}

	bool playerInCommandPosition (GameObject controlObject) {
		return Mathf.Sqrt (Mathf.Pow (controlObject.transform.position.x - transform.position.x, 2)
		+ Mathf.Pow (controlObject.transform.position.y - transform.position.y, 2)) <= controlDistance;
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "subordinate")
			massMovement = false;
	}

	// Update is called once per frame
	void Update () {

		GameObject controlObject = (blue) ? blueObject : redObject;

		if (Input.GetKey (KeyCode.Mouse0) && (sc.getIsControllingBlue() == blue)) {

			//////////shift to be fight active key
			if (Input.GetKey (KeyCode.Q) && awakened) {
				massMovement = true;

				//////////idea: make blue slow down but not red
				if (blue) {
					Vector2 movement = evenDirectionalMovement (transform.position, controlObject.transform.position);
					float speedModifier = getSpeedModifier (transform.position, controlObject.transform.position);

					GetComponent <Rigidbody2D> ().velocity = new Vector2 (movement.x * speedModifier * speed,
						movement.y * speedModifier * speed);

				} else {

					Vector3 mousePosInSpace = Camera.main.ScreenToWorldPoint(
						new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f) );

					Vector2 movement = evenDirectionalMovement (controlObject.transform.position, mousePosInSpace);

					GetComponent <Rigidbody2D> ().velocity = new Vector2 (movement.x * speed, movement.y * speed);

				}

				awakened = false;

			} else if (playerInCommandPosition (controlObject)) {
				awakened = true;
				massMovement = false;
				GetComponent <Rigidbody2D> ().velocity = getMovement ();
			}

		} else if (!massMovement) {

			GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		}

	}

	Vector2 getMovement () {


		Vector3 mousePosInSpace = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f) );

		Vector2 moveVector = evenDirectionalMovement (transform.position, mousePosInSpace);

		float speedModifier = getSpeedModifier (transform.position, mousePosInSpace);

		return new Vector2 (moveVector.x * speed * speedModifier, moveVector.y * speed * speedModifier);

	}

	float getSpeedModifier (Vector3 placeFrom, Vector3 placeTo) {

		float speedModifier = Mathf.Sqrt (Mathf.Pow (placeTo.x - placeFrom.x, 2)
			+ Mathf.Pow (placeTo.y - placeFrom.y, 2)) / slowDownDistance;

		if (speedModifier > 1f)
			speedModifier = 1f;

		return speedModifier;

	}

	Vector2 evenDirectionalMovement (Vector3 placeFrom, Vector3 placeTo) {

		Vector2 rawDifference = new Vector2 (placeTo.x - placeFrom.x, placeTo.y - placeFrom.y);

		float biggerFloat = (Mathf.Abs(rawDifference.x) > Mathf.Abs(rawDifference.y))
			? Mathf.Abs(rawDifference.x) : Mathf.Abs(rawDifference.y);

		return new Vector2 (rawDifference.x / biggerFloat, rawDifference.y / biggerFloat);

	}


}
