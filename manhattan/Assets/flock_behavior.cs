using UnityEngine;
using System;
using System.Collections.Generic;

public class flock_behavior : MonoBehaviour {
	public float speed = 0.25f;
	List<GameObject> swarm;
	List<GameObject> detractors;
	public float cohesion = 1.0f;
	public float alignment = 1.0f;
	public float separation = 1.0f;
	public float alignmentRadius = 1f;
	public float cohesionRadius = 1f;
	public float separationRadius = 0.5f;
	public float detractorRadius = 2f;
	public float detractorWeight = 1f;
	private Vector2 velocity;

	// Use this for initialization
	void Start () {
		swarm = new List<GameObject>(GameObject.FindGameObjectsWithTag("Sheep"));
		swarm.Remove(gameObject);
		Debug.Log (String.Format ("swarm: {0}", swarm.Count));
		detractors = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Wolf"));

		//Vector2 velocity = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
		//velocity.Normalize ();
		Vector2 velocity = new Vector2(0, 0);
		setVelocity(velocity);
	}

	private void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
		gameObject.GetComponent<Rigidbody>().velocity = speed * velocity ;
	}

	public Vector2 getVelocity() {
		return velocity;
	}
	
	private Vector2 toVector2(Vector3 vector3) {
		return new Vector2(vector3.x, vector3.y);
	}

	private Vector3 toVector3(Vector2 vector2) {
		return new Vector3(vector2.x, vector2.y, 0);
	}

	private Vector2 computeCohesion() {
		Vector2 center = new Vector2(0, 0);
		int count = 0;
		foreach (GameObject agent in swarm) {
			if (getDistance(agent, gameObject) < cohesionRadius) {
				center += toVector2(agent.transform.position);
				count++;
			}
		}
		center /= count;
		Vector2 direction = center - toVector2(transform.position);
		direction.Normalize();
		return direction;
	}

	static float getDistance(GameObject a, GameObject b) {
		return (a.transform.position - b.transform.position).magnitude;
	}

	private Vector2 computeSeparation() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			if (getDistance(agent, gameObject) < separationRadius) {
				direction += toVector2(agent.transform.position - transform.position);
			}
		}
		direction = -direction;
		direction.Normalize();
		return direction;
	}

	private Vector2 computeAlignment() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			if (getDistance(agent, gameObject) < alignmentRadius) {
				direction += agent.GetComponent<flock_behavior>().getVelocity();
			}
		}
		direction.Normalize();
		return direction;
	}

	private float reflectAtLimit(float velocity, float x, float limit) {
		if (Math.Abs (x) > limit) {
			velocity = -Math.Sign (x);
		}
		return velocity;
	}

	private Vector2 computeDetractor() {
		Vector2 direction = new Vector2(0, 0);
		float minDistance = 0f;
		Vector3 minPosition = new Vector2(0, 0);
		Boolean first = true;
		foreach (GameObject detractor in detractors) {
			float distance = getDistance (detractor, gameObject);
			if (first || distance < minDistance) {
				minDistance = distance;
				minPosition = detractor.transform.position;
			}
			first = false;
		}
		if (!first && minDistance < detractorRadius) {
			direction = toVector2(transform.position - minPosition);
			direction.Normalize ();
		}
		return direction;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 nextVelocity = getVelocity();

		//float height = Camera.main.orthographicSize;
		//float width = (height * Screen.width) / Screen.height;
		//nextVelocity.y = reflectAtLimit (nextVelocity.y, transform.position.y, height);
		//nextVelocity.x = reflectAtLimit (nextVelocity.x, transform.position.x, width);

		nextVelocity += Time.deltaTime * 10f * (
			cohesion * computeCohesion() +
			separation * computeSeparation() +
			alignment * computeAlignment() +
			detractorWeight * computeDetractor());
		//Debug.Log (String.Format ("DeltaTime: {0}", Time.deltaTime));
		nextVelocity.Normalize();

		setVelocity(nextVelocity);
		//transform.position = transform.position + toVector3(velocity) * speed * Time.deltaTime;
	}
}
