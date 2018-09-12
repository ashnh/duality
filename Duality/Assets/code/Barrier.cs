using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

	public bool startDestroyed;

	public Collider2D wall;

	// Use this for initialization
	void Start () {

		if (startDestroyed)
			wall.isTrigger = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "subordinate") {

			Subordinate touching = other.gameObject.GetComponent <Subordinate> ();

			if (touching.getBarrierOrdered ()) {

				if (touching.blue)
					setWall (true);
				else
					setWall (false);

			}

		}

	}

	/// <summary>
	/// Sets the wall.
	/// </summary>
	/// <param name="turnOn">If set to <c>true</c> turn on.</param>
	public void setWall (bool turnOn) {

		wall.isTrigger = !turnOn;

	}

	/// <summary>
	/// returns whether the wall is activated
	/// </summary>
	/// <returns><c>true</c>, if is activated, <c>false</c> otherwise.</returns>
	public bool wallIsActivated () {
		return !wall.isTrigger;
	}
}
