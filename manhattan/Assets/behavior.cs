using UnityEngine;
using System.Collections;

public class behavior : MonoBehaviour {
	public int speed = 1;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(1, 2, 3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + new Vector3(1, 0, 0) * speed * Time.deltaTime;
	}
}
