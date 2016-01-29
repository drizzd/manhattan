using UnityEngine;
using System;
using System.Collections.Generic;

public class behavior : MonoBehaviour {
	public float speed = 2f;
	List<GameObject> swarm;
	public float cohesion = 1.0f;
	public float alignment = 1.0f;
	public float separation = 1.0f;

	// Use this for initialization
	void Start () {
		//transform.position = new Vector3(1, 2, 3);
		swarm = new List<GameObject>(GameObject.FindGameObjectsWithTag("Sheep"));
		swarm.Remove(gameObject);

		Vector2 velocity = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
		velocity.Normalize ();
		gameObject.GetComponent<Rigidbody>().velocity = velocity;
	}

	private Vector2 toVector2(Vector3 vector3) {
		return new Vector2(vector3.x, vector3.y);
	}

	private Vector3 toVector3(Vector2 vector2) {
		return new Vector3(vector2.x, vector2.y, 0);
	}

	private Vector2 computeCohesion() {
		Vector2 center = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			center += toVector2(agent.transform.position);
		}
		center /= swarm.Count;
		Vector2 direction = center - toVector2(transform.position);
		direction.Normalize();
		return direction;
	}

	private Vector2 computeSeparation() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			direction += toVector2(agent.transform.position - transform.position);
		}
		direction = -direction;
		direction.Normalize();
		return direction;
	}

	public Vector2 getVelocity() {
		return gameObject.GetComponent<Rigidbody>().velocity;
	}

	private Vector2 computeAlignment() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			direction += agent.GetComponent<behavior>().getVelocity();
		}
		direction.Normalize();
		return direction;
	}

	// Update is called once per frame
	void Update () {
		Vector2 nextVelocity = getVelocity();

		if (transform.position.x > 5) {
			//nextVelocity.x = -Math.Sign (transform.position.x) * Math.Abs (nextVelocity.x);
			nextVelocity.x = -1;
		} else if (transform.position.x < -5) {
			nextVelocity.x = 1;
		}
		if (transform.position.y > 3) {
			nextVelocity.y = -1;
		} else if (transform.position.y < -3) {
			nextVelocity.y = 1;
		}

		nextVelocity += 0.5f * (cohesion * computeCohesion() + separation * computeSeparation() + alignment * computeAlignment());
		nextVelocity.Normalize();

		gameObject.GetComponent<Rigidbody>().velocity = nextVelocity;
		//transform.position = transform.position + toVector3(velocity) * speed * Time.deltaTime;
	}
}
