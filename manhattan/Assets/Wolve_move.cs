using UnityEngine;
using System.Collections;



public class Wolve_move : MonoBehaviour {
	public float Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GameObject[] sheeps;
		sheeps = GameObject.FindGameObjectsWithTag ("Sheep");
		Vector3 dir = new Vector3(0,0,0);
		int NrS = 0;
		foreach (GameObject sheep in sheeps) {
			NrS++;
			dir += (Vector3.MoveTowards (transform.position, sheep.transform.position, 1) - transform.position) * 1 / Vector3.Distance (transform.position, sheep.transform.position);
		}
		
		
		transform.position += (dir / NrS).normalized * Speed;


	}
}
