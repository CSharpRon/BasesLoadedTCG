using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inning : MonoBehaviour {
	public Text inningObj;
	public Team playerTeam;

	// Use this for initialization
	void Start () {
		inningObj = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		inningObj.text = "Inning: " + ((playerTeam.swaps / 2) + 1);
	}
}
