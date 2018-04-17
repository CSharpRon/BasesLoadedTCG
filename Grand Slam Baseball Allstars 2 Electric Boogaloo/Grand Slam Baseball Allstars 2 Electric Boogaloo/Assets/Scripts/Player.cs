using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour {
	public int strength;
	public int stamina;
	public int speed;
	public int style;
	public int accuracy;
	public Ball ball;
	private Sprite sprite;
	public Vector3 oldPosition;
	public int targetBase;
	public bool running;
	public bool safe;

	// Use this for initialization
	void Start () {
		oldPosition = this.transform.position;
		targetBase = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Pitch the ball based on the pitcher's power
	public void Pitch(){
		Vector3 power = new Vector3(0.0f, (float)(-strength/4) + (float)(Random.Range(-4, -1) * 2), 0.0f);
		ball.velocity = power;
		ball.airTime = .5f;
		ball.pitched = true;
		ball.missed = false;
	}

	// Hit the ball. Velocity based on ball velocity and strength,
	// chance to hit and ball direction based on accuracy
	public void Bat(){
		float ballSpeed = -ball.velocity.y;
		if (ball.pitched) {
			ball.pitched = false;
			if (ballSpeed < accuracy * 2 + (Random.Range (0, 3) * 2)) {
				ball.velocity.y = ballSpeed + (strength / 2);
				float xRange = (Random.Range (10f, 15f) - accuracy) / 2;
				ball.velocity.x = Random.Range (-xRange, xRange);
				ball.airTime = 2f;
				ball.pitched = false;
				ball.hit = true;
				targetBase = 1;
				running = true;
				safe = false;
			} else {
				ball.missed = true;
				Debug.Log ("Missed");
			}
		}
	}

	// Catch the ball. Chance to catch based on ball velocity and accuracy
	public void Catch(){
		if (Vector3.Distance (ball.transform.position, this.transform.position) <= 1.0) {
			if (ball.airTime <= 0.5) {
				ball.airTime = 0;
				ball.velocity = Vector3.zero;
				ball.caught = true;
				ball.holding = this;
			}
		}
	}

	//Take a base. Chance to keep taking based on style, speed based on speed, stamina based on stamina
	public void Run(Vector3 position){
		if (Vector3.Distance(transform.position, position) <= .5) {
			safe = true;
			targetBase += 1;
		}
		else
			transform.position = Vector3.MoveTowards (transform.position, position, (speed * Time.deltaTime) / 2);
	}

	//Pass the ball, based on accuracy and strength
	public void Pass(Vector3 position){
		ball.transform.position = Vector3.MoveTowards (ball.transform.position, position, strength / 5);
	}

	//Move in the way of the ball, based on speed and stamina
	public void Intercept(){
		transform.position = Vector3.MoveTowards (this.transform.position, ball.transform.position, (speed * Time.deltaTime)/4);
		Catch ();
	}
}
