using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followLeader : MonoBehaviour {

	public GameObject Leader;

	// Use this for initialization
	void Start () {

		Leader = GameObject.Find ("Leader");
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(Leader.transform.position);
	}
}
