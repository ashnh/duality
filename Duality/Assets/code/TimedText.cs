using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedText : MonoBehaviour {

	public TextAsset script;

	public float timePerLine;

	bool cycle;

	float timeStamp;

	string[] lines;

	int currentLineIndex;

	// Use this for initialization
	void Start () {

		endText ();

	}

	public void endText () {

		cycle = false;

		timeStamp = -timePerLine - 1f;

		currentLineIndex = -1;

		GetComponent <TextMesh> ().text = "";

	}

	/// <summary>
	/// starts timed text with given text asset
	/// </summary>
	/// <param name="characterLines"> textassest for the timed text </param> 
	public void startText (TextAsset characterLines) {

		lines = characterLines.text.Split ('"');

		startCycle ();

	}

	/// <summary>
	/// starts timed text with already assigned text asset
	/// </summary>
	public void startText () {

		lines = script.text.Split ('"');

		startCycle ();

	}

	void startCycle() {

		cycle = true;

		timeStamp = Time.timeSinceLevelLoad;

	}
	
	// Update is called once per frame
	void Update () {

		if (cycle &&timeStamp + timePerLine < Time.timeSinceLevelLoad) {

			if (++currentLineIndex < lines.Length) {
				GetComponent <TextMesh> ().text = lines [currentLineIndex];
				timeStamp = Time.timeSinceLevelLoad;
			} else
				endText ();

		}

	}
}
