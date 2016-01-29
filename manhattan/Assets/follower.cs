using UnityEngine;
using System.Collections;

public class follower : MonoBehaviour {
	public GameObject leader;
	// Use this for initialization
	void Start () {
		leader= GameObject.FindGameObjectWithTag("Leader");

	}
	
	// Update is called once per frame
	void Update () {
	}
}
