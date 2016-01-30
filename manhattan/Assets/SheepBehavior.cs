using UnityEngine;
using System;
using System.Collections.Generic;

public class SheepBehavior : FlockBehavior {
	// Use this for initialization
	public override void Start () {
		base.Init ("Sheep");
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 nextVelocity = getVelocity();

		//nextVelocity += combineDirection();
		nextVelocity.Normalize();

		setVelocity(nextVelocity);
	}
}
