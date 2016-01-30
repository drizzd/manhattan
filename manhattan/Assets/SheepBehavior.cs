using UnityEngine;
using System;
using System.Collections.Generic;

public class SheepBehavior : FlockBehavior {
	private int whatever;

	// Use this for initialization
	public override void Start () {
		base.Start();
		whatever = 1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 nextVelocity = getVelocity();

		//nextVelocity += combineDirection();
		nextVelocity.Normalize();

		setVelocity(nextVelocity);
	}
}
