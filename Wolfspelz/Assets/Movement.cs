using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float maxSpeed;
	public float follow;
	public Transform sheep;
	public Transform wolf;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 poschange = -transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
		Vector3 pos = Vector3.ClampMagnitude(poschange,maxSpeed) + transform.position;
			
		transform.position =  Vector3.Lerp(transform.position,pos,follow);




	}
}
