using UnityEngine;
using System.Collections;

public class follower : MonoBehaviour {
	public GameObject[] swarm;
	// Use this for initialization
	void Start () {
		swarm = GameObject.FindGameObjectsWithTag("Leader");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
