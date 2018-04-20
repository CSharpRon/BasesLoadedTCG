using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Vector3 velocity;
	public bool pitched;
	public bool hit;
	public float airTime;
	public bool caught;
	public bool passed;
	public bool missed;
	public Vector3 oldPosition;
	public Player holding;

	// Use this for initialization
	void Start () {
		oldPosition = this.transform.position;
		airTime = 0;
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y >= 10.392 || this.transform.position.y <= -10.392)
			velocity.y = -velocity.y;
		if (this.transform.position.x >= 13.578 || this.transform.position.x <= -13.578)
			velocity.x = -velocity.x;
		
		this.transform.position += (velocity * Time.deltaTime);
		if (airTime > 0)
			airTime -= 1f * Time.deltaTime;
		else if (velocity.magnitude > 0.0)
			velocity = Vector3.ClampMagnitude (velocity, velocity.magnitude - (1f * Time.deltaTime));
		else
			velocity = Vector3.zero;
	}
}
