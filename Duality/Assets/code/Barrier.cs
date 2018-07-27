using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

	public bool destroyed;

	public Collider2D wall;

	// Use this for initialization
	void Start () {

		if (destroyed)
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

	public void setWall (bool turnOn) {

		wall.isTrigger = !turnOn;

	}
}
