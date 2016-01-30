using UnityEngine;
using System.Collections;

public class WrapWolfCreate : MonoBehaviour {
	public float horz;
	public float vert;
	public Transform DummyWolf;

	// Use this for initialization
	void Start () {
		vert = Camera.main.orthographicSize;
		horz = Camera.main.aspect * vert;
		Transform clone;


		clone = Instantiate(DummyWolf,transform.position + new Vector3(-horz*2,0,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(horz*2,0,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(0,-vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(0,vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;

		clone = Instantiate(DummyWolf,transform.position + new Vector3(-horz*2,-vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(horz*2,vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(-horz*2,vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;
		clone = Instantiate(DummyWolf,transform.position + new Vector3(horz*2,-vert*2,0),Quaternion.identity) as Transform;
		clone.parent = transform;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
