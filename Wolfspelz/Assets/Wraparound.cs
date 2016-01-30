using UnityEngine;
using System.Collections;

public class Wraparound : MonoBehaviour
{
	public float horz;
	public float vert;
	// Use this for initialization
	void Start ()
	{
		vert = Camera.main.orthographicSize;
		horz = Camera.main.aspect * vert;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Mathf.Abs (transform.position.x) > horz*1.1f)
			transform.position = new Vector3(-transform.position.x*0.9f,transform.position.y,transform.position.z);
		if (Mathf.Abs (transform.position.y) > vert*1.1f)
			transform.position = new Vector3(transform.position.x,-transform.position.y*0.9f,transform.position.z);
	}
}
