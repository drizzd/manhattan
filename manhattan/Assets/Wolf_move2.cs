using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class GameObjectWithDistance {
	public GameObject gameObject;
	public float distance;
	
	public GameObjectWithDistance(GameObject gameObject, float distance) {
		this.gameObject = gameObject;
		this.distance = distance;
	}
}

class Utils {
	public static GameObject findNearestNeighbor(GameObject origin, List<GameObject> neighbors) {
		GameObjectWithDistance nearestNeighbor = null;
		foreach (GameObject neighbor in neighbors) {
			float distance = Vector3.Distance (origin.transform.position, neighbor.transform.position);
			if (nearestNeighbor == null || distance < nearestNeighbor.distance) {
				nearestNeighbor = new GameObjectWithDistance(neighbor, distance);
			}
		}
		return nearestNeighbor.gameObject;
	}

	public static Vector2 toVector2(Vector3 vector3) {
		return new Vector2(vector3.x, vector3.y);
	}

	public static float reflectAtLimit(float velocity, float x, float limit) {
		if (Math.Abs (x) > limit) {
			velocity = -Math.Sign (x);
		}
		return velocity;
	}

	public static float getDistance(GameObject a, GameObject b) {
		return Vector3.Distance(a.transform.position, b.transform.position);
	}
}

public class Wolf_move2 : MonoBehaviour {

	private Vector2 velocity;
	public float speed;
	private List<GameObject> sheep;

	// Use this for initialization
	void Start () {
		sheep = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Sheep"));
	}
	
	private void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
		gameObject.GetComponent<Rigidbody>().velocity = speed * velocity ;
	}
	
	public Vector2 getVelocity() {
		return velocity;
	}

	public Vector2 computeAttractor() {
		var nearestSheep = Utils.findNearestNeighbor (gameObject, sheep);
		if (Utils.getDistance (nearestSheep, gameObject) < 0.5f) {
			return new Vector2(0, 0);
		}
		var direction = Utils.toVector2(nearestSheep.transform.position - transform.position);
		direction.Normalize ();
		return direction;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 nextVelocity;
		nextVelocity = computeAttractor();

		float height = Camera.main.orthographicSize;
		float width = (height * Screen.width) / Screen.height;
		nextVelocity.y = Utils.reflectAtLimit (nextVelocity.y, transform.position.y, height - 0.5f);
		nextVelocity.x = Utils.reflectAtLimit (nextVelocity.x, transform.position.x, width - 0.5f);

		setVelocity (nextVelocity);
	}
}
