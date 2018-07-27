using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communication : MonoBehaviour {

	public TextAsset script;
	public SharedCharacters sc;

	// ": separates lines
	// BBB: blue line
	// RRR: red line

	private string[] lines;

	private ArrayList blueLines; 
	private ArrayList redLines;

	void Start () {

		lines = script.text.Split ('"');

		foreach (string x in lines) {

			if (x.Substring (0, 3).Equals ("BBB"))
				blueLines.Add (x);
			else
				redLines.Add (x);
			

		}

	}

	void Update () {
		
	}
}
