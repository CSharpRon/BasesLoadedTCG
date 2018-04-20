using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public Text scoreObj;
	public Team playerTeam;

	// Use this for initialization
	void Start () {
		scoreObj = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.name == "Score")
			scoreObj.text = "Your Score: " + playerTeam.score;
		else
			scoreObj.text = "Enemy Score: " + playerTeam.score;
	}
}
