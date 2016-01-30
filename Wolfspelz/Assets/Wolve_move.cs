using UnityEngine;
using System.Collections;



public class Wolve_move : MonoBehaviour {
	public Transform Sheep;
	public float Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards(transform.position,Sheep.position,Speed);
	}
}
