using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
	public GameObject targetGameObject;
	public Vector3 target = new Vector3 (0, 0, 0);

	public void OnDrawGizmos()
	{

	}

	public void Start()
	{
		targetGameObject = new GameObject ();

		targetGameObject.gameObject.name = ("LeaderTarget");

		targetGameObject.transform.position = this.transform.position;

		Vector3 offset = new Vector3 (0, 0, 1000);

		targetGameObject.transform.position += offset;
	}

	public override Vector3 Calculate()
	{
		return boid.SeekForce(target);    
	}

	public void Update()
	{


		if (targetGameObject != null)
		{
			//targetGameObject = this.gameObject;

			Vector3 offset = new Vector3 (0, 0, 1000);

			targetGameObject.transform.position = this.transform.position + offset;

			target = targetGameObject.transform.position;
		}
	}
}