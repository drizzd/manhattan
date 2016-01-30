using UnityEngine;
using System.Collections;

public class Sheep_move : MonoBehaviour
{

	public float speed;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject[] wolfs;
		wolfs = GameObject.FindGameObjectsWithTag ("wolf");
		Vector3 dir = new Vector3(0,0,0);
		int NrW = 0;
		foreach (GameObject wolf in wolfs) {
			NrW++;
			dir += -(Vector3.MoveTowards (transform.position, wolf.transform.position, 1) - transform.position) * 1 / Vector3.Distance (transform.position, wolf.transform.position);
		}


		transform.position += (dir / NrW).normalized * speed;

	}
}
