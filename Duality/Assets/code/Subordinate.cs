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

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0) && (sc.getIsControllingBlue() == blue)) {

			GameObject controlObject = (blue) ? blueObject : redObject;

			if (Mathf.Sqrt (Mathf.Pow (controlObject.transform.position.x - transform.position.x, 2) + Mathf.Pow (controlObject.transform.position.y - transform.position.y, 2)) <= controlDistance)
				GetComponent <Rigidbody2D> ().velocity = getMovement ();
			else
				GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		} else {

			GetComponent <Rigidbody2D> ().velocity = new Vector2 (0, 0);

		}

	}

	Vector2 getMovement () {


		Vector3 mousePosInSpace = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f) );

		Vector2 rawDifference = new Vector2 (mousePosInSpace.x - transform.position.x, mousePosInSpace.y - transform.position.y);

		float speedModifier = Mathf.Sqrt (Mathf.Pow (mousePosInSpace.x - transform.position.x, 2) + Mathf.Pow (mousePosInSpace.y - transform.position.y, 2)) / slowDownDistance;

		if (speedModifier > 1f)
			speedModifier = 1f;

		float biggerFloat = (Mathf.Abs(rawDifference.x) > Mathf.Abs(rawDifference.y)) ? Mathf.Abs(rawDifference.x) : Mathf.Abs(rawDifference.y);

		return new Vector2 (rawDifference.x / biggerFloat * speed * speedModifier, rawDifference.y / biggerFloat * speed * speedModifier);

	}
}
