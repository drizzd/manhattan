using UnityEngine;
using System;
using System.Collections.Generic;

public class FlockBehavior : MonoBehaviour {
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
	public virtual void Start () {
		swarm = new List<GameObject>(GameObject.FindGameObjectsWithTag("Sheep"));
		swarm.Remove(gameObject);
		Debug.Log (String.Format ("swarm: {0}", swarm.Count));
		detractors = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Wolf"));

		//Vector2 velocity = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
		//velocity.Normalize ();
		Vector2 velocity = new Vector2(0, 0);
		setVelocity(velocity);
	}

	protected void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
		gameObject.GetComponent<Rigidbody>().velocity = speed * velocity ;
	}

	public Vector2 getVelocity() {
		return velocity;
	}
	
	private Vector3 toVector3(Vector2 vector2) {
		return new Vector3(vector2.x, vector2.y, 0);
	}

	private Vector2 computeCohesion() {
		Vector2 center = new Vector2(0, 0);
		int count = 0;
		if (swarm == null) {
			return new Vector2(0, 0);
		}
		foreach (GameObject agent in swarm) {
			if (Utils.getDistance(agent, gameObject) < cohesionRadius) {
				center += Utils.toVector2(agent.transform.position);
				count++;
			}
		}
		center /= count;
		Vector2 direction = center - Utils.toVector2(transform.position);
		direction.Normalize();
		return direction;
	}

	private Vector2 computeSeparation() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			if (Utils.getDistance(agent, gameObject) < separationRadius) {
				direction += Utils.toVector2(agent.transform.position - transform.position);
			}
		}
		direction = -direction;
		direction.Normalize();
		return direction;
	}

	private Vector2 computeAlignment() {
		Vector2 direction = new Vector2(0, 0);
		foreach (GameObject agent in swarm) {
			if (Utils.getDistance(agent, gameObject) < alignmentRadius) {
				direction += agent.GetComponent<FlockBehavior>().getVelocity();
			}
		}
		direction.Normalize();
		return direction;
	}

	private Vector2 computeDetractor() {
		Vector2 direction = new Vector2(0, 0);
		float minDistance = 0f;
		Vector3 minPosition = new Vector2(0, 0);
		Boolean first = true;
		foreach (GameObject detractor in detractors) {
			float distance = Utils.getDistance (detractor, gameObject);
			if (first || distance < minDistance) {
				minDistance = distance;
				minPosition = detractor.transform.position;
			}
			first = false;
		}
		if (!first && minDistance < detractorRadius) {
			direction = Utils.toVector2(transform.position - minPosition);
			direction.Normalize ();
		}
		return direction;
	}

	protected Vector2 combineDirection() {
		return Time.deltaTime * 10f * (
			cohesion * computeCohesion() +
			separation * computeSeparation() +
			alignment * computeAlignment() +
			detractorWeight * computeDetractor());
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 nextVelocity = getVelocity();

		//float height = Camera.main.orthographicSize;
		//float width = (height * Screen.width) / Screen.height;
		//nextVelocity.y = Utils.reflectAtLimit (nextVelocity.y, transform.position.y, height);
		//nextVelocity.x = Utils.reflectAtLimit (nextVelocity.x, transform.position.x, width);

		nextVelocity += combineDirection();
		//Debug.Log (String.Format ("DeltaTime: {0}", Time.deltaTime));
		nextVelocity.Normalize();

		setVelocity(nextVelocity);
		//transform.position = transform.position + toVector3(velocity) * speed * Time.deltaTime;
	}
}
