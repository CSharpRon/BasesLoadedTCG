using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable () {
		Debug.Log ("Here");
		StartCoroutine (Back());
	}

	IEnumerator Back() {
		yield return new WaitForSeconds (5);
		Debug.Log ("Loading Menu...");
		SceneManager.LoadScene ("MainMenu");
	}
}
