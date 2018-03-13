using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

	List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

	public Vector3 force = new Vector3 (0, 0, 0);
	public Vector3 acceleration = new Vector3 (0, 0, 0);
	public Vector3 velocity = new Vector3 (0, 0, 0);

	public float mass = 1;
	public float maximumSpeed = 10;

	public Vector3 SeekForce(Vector3 target)
	{
		//calculate desired velocity vector direction
		Vector3 desired = target - transform.position;
		desired.Normalize();

		//multiply by the speed;
		desired *= maximumSpeed;

		//return desired minus the current velocity
		return desired - velocity;
	}

	public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f, float deceleration = 1.0f)
	{
		Vector3 toTarget = target - transform.position;

		//calculate scalar distance
		float distanceToTarget = toTarget.magnitude;

		//if scalar is 0, return Vector 0
		if (distanceToTarget == 0)
		{
			Vector3 zeroVector = new Vector3 (0, 0, 0);
			return zeroVector;
		}

		//calculate ramped speed
		float rampedSpeed = maximumSpeed * (distanceToTarget / (slowingDistance * deceleration));

		//choose which one is smaller
		float clamped = Mathf.Min(rampedSpeed, maximumSpeed);


		Vector3 desired = clamped * (toTarget / distanceToTarget);

		//return desired minus the current velocity
		return desired - velocity;
	}

	// Use this for initialization
	void Start () {

		SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

		foreach (SteeringBehaviour b in behaviours)
		{
			this.behaviours.Add(b);
		}


	}
	
	// Update is called once per frame
	void Update () {


		force = Calculate();
		Vector3 newAcceleration = force / mass;

		float smoothRate = Mathf.Clamp(9.0f * Time.deltaTime, 0.15f, 0.4f) / 2.0f;
		acceleration = Vector3.Lerp(acceleration, newAcceleration, smoothRate);

		velocity += acceleration * Time.deltaTime;

		velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

		Vector3 globalUp = new Vector3(0, 0.2f, 0);
		Vector3 accelUp = acceleration * 0.05f;
		Vector3 bankUp = accelUp + globalUp;
		smoothRate = Time.deltaTime * 3f;
		Vector3 tempUp = transform.up;
		tempUp = Vector3.Lerp(tempUp, bankUp, smoothRate);

		if (velocity.magnitude  > 0.0001f)
		{
			transform.LookAt(transform.position + velocity, tempUp);
			velocity *= 0.99f;
		}
		transform.position += velocity * Time.deltaTime;     
		
	}

    Vector3 Calculate()
    {
        force = Vector3.zero;

		//add all behaviours to a list
        foreach (SteeringBehaviour b in behaviours)
        {
            if (b.isActiveAndEnabled)
            {
                force += b.Calculate() * b.weight;
            }
        }


        return force;
    }

	

}

