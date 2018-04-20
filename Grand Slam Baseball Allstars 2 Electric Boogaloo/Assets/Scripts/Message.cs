using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {
	public Text win;
	public Text lose;

	// Use this for initialization
	void Start () {
		lose = win;
		lose.text = "BETTER LUCK NEXT TIME...";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
