using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
	public Transform enemy;
	private Vector3 target;
	public Vector3 velocity = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 force = Vector3.zero;
	public float slowingDistance = 5;
	public float damping = 0.1f;

	[Range(0.0f, 10.0f)]
	public float banking = 0.1f;

	public float mass = 1.0f;
	public float maxSpeed = 5;
	public float speed = 0;

	private void Start()
	{
		transform.LookAt(enemy, Vector3.up);
		target = enemy.position - transform.position;
		target = -target.normalized * 10;
	}

	Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float dist = toTarget.magnitude;

		float ramped = (dist / slowingDistance) * maxSpeed;
		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = (toTarget / dist) * clamped;

		return desired - velocity;
	}

	public Vector3 CalculateForce()
	{
		Vector3 force = Vector3.zero;
		force += Arrive(enemy.position + target);
		return force;
	}

	// Update is called once per frame
	void Update()
    {
		force = CalculateForce();
		acceleration = force / mass;
		velocity += acceleration * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;
		speed = velocity.magnitude;
		if (speed > 0)
		{
			Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
			transform.LookAt(transform.position + velocity, tempUp);
			//transform.forward = velocity;
			velocity -= (damping * velocity * Time.deltaTime);
		}
	}
}
