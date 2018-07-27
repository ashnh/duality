using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordinate : MonoBehaviour {

	public bool blue;

	public float speed;
	public float slowDownDistance;
	public float controlDistance;
	public float fightStateDuration;

	public SharedCharacters sc;

	public GameObject redObject;
	public GameObject blueObject;

	public Camera cameraObject;


	private bool awakened;
	private bool massMovement;

	private bool fightOrdered;
	private bool barrierOrdered;

	private float activeTime;

	//red exclusive
	private Vector2 returnVector;

	public bool getFightOrdered () {
		return fightOrdered;
	}

	public bool getBarrierOrdered () {
		return barrierOrdered;
	}

	void Start () {
		
		awakened = false;
		massMovement = false;
		fightOrdered = false;
		barrierOrdered = false;

		//returnVector = new Vector2 (0, 0);

		activeTime = Time.timeSinceLevelLoad;

	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "subordinate") {
			massMovement = false;
		} else {

			Subordinate otherThing = other.gameObject.GetComponent <Subordinate> ();

			if (otherThing.blue != this.blue && fightOrdered) {

				if (!otherThing.getFightOrdered ()) {
					Destroy (other.gameObject);
				}

			}

		}
	}
		
	void Update () {

		GameObject controlObject = (blue) ? blueObject : redObject;

		//-----------continual check section
		barrierOrdered = Input.GetKey (KeyCode.E) && playerInCommandPosition (controlObject);

		//-----------time section
		if (fightOrdered && Time.timeSinceLevelLoad > activeTime + fightStateDuration) {

			fightOrdered = false;

			if (!blue) {

				moveToPosition (transform.position, returnVector);

				massMovement = true;

			}

		}

		//------------control section
		if (Input.GetKey (KeyCode.Mouse0) && sc.getIsControllingBlue() == blue) {

			//////////shift to be fight active key
			if (Input.GetKey (KeyCode.LeftShift) && playerInCommandPosition (controlObject)) {

				fightOrdered = true;

				activeTime = Time.timeSinceLevelLoad;

				if (!blue) {

					returnVector = transform.position;

					moveTowardsDirection (controlObject.transform.position, dMousePos ());

				} else {

					moveToPosition (dMousePos ());

				}

			} else if (Input.GetKey (KeyCode.Q) && awakened) {
				massMovement = true;

				//////////idea: make blue slow down but not red
				if (blue) {
					moveToPosition (transform.position, controlObject.transform.position);

				} else {

					moveTowardsDirection (controlObject.transform.position, dMousePos ());

				}

				awakened = false;

			} else if (playerInCommandPosition (controlObject)) {
				awakened = true;
				massMovement = false;
				moveToPosition (dMousePos ());
			}

		} else if (!massMovement && (blue || !fightOrdered) ) {

			GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		}

	}

	//returns mouse position in world space (kinda unneccesary)
	Vector3 dMousePos () {
		return Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f) );
	}

	// moves the subordinate in a direction defined by to different vector3 points
	void moveTowardsDirection (Vector3 placeFrom, Vector3 placeTo) {

		Vector2 movement = evenDirectionalMovement (placeFrom, placeTo);

		GetComponent <Rigidbody2D> ().velocity = new Vector2 (movement.x * speed, movement.y * speed);

	}

	// moves subordinate to a vector3 point defined by the direction and length between two vector3 points
	void moveToPosition (Vector3 placeFrom, Vector3 placeTo) {

		Vector2 movement = evenDirectionalMovement (placeFrom, placeTo);

		float speedModifier = getSpeedModifier (placeFrom, placeTo);

		GetComponent <Rigidbody2D> ().velocity =
			new Vector2 (movement.x * speed * speedModifier, movement.y * speed * speedModifier);

	}

	// previous moveToPosition but place from is defined by position in world space
	void moveToPosition (Vector3 placeTo) {

		moveToPosition (transform.position, placeTo);

	}

	// gets proportional slow down from to vector3 points
	float getSpeedModifier (Vector3 placeFrom, Vector3 placeTo) {

		float speedModifier = Mathf.Sqrt (Mathf.Pow (placeTo.x - placeFrom.x, 2)
			+ Mathf.Pow (placeTo.y - placeFrom.y, 2)) / slowDownDistance;

		if (speedModifier > 1f)
			speedModifier = 1f;

		return speedModifier;

	}

	// returns the base direction (max 1) from two vector3 points
	Vector2 evenDirectionalMovement (Vector3 placeFrom, Vector3 placeTo) {

		Vector2 rawDifference = new Vector2 (placeTo.x - placeFrom.x, placeTo.y - placeFrom.y);

		float biggerFloat = (Mathf.Abs(rawDifference.x) > Mathf.Abs(rawDifference.y))
			? Mathf.Abs(rawDifference.x) : Mathf.Abs(rawDifference.y);

		return new Vector2 (rawDifference.x / biggerFloat, rawDifference.y / biggerFloat);

	}
		
	// returns whether or not the subordinate is in command position
	bool playerInCommandPosition (GameObject controlObject) {
		return Mathf.Sqrt (Mathf.Pow (controlObject.transform.position.x - transform.position.x, 2)
			+ Mathf.Pow (controlObject.transform.position.y - transform.position.y, 2)) <= controlDistance;
	}


}
