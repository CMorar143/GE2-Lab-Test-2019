using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public Transform enemy;
	private Vector3 t;
	private Vector3 start;
	private float startTime;
	private float journeyLength;
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


	// Start is called before the first frame update
	void Start()
	{
		start = transform.position;
		transform.LookAt(enemy, Vector3.up);
		t = enemy.position - transform.position;
		//t = t - new Vector3(0,0,2);
		t = t.normalized * 2;

		startTime = Time.time;
		journeyLength = Vector3.Distance(start, enemy.position);
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
		force += Arrive(enemy.position);
		return force;
	}

	// Update is called once per frame
	void Update()
	{
		//force = CalculateForce();
		//acceleration = force / mass;
		//velocity += acceleration * Time.deltaTime;

		//transform.position += velocity * Time.deltaTime;
		//speed = velocity.magnitude;
		//if (speed > 0)
		//{
		//	Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
		//	transform.LookAt(transform.position + velocity, tempUp);
		//	//transform.forward = velocity;
		//	velocity -= (damping * velocity * Time.deltaTime);
		//}

		float distCovered = (Time.time - startTime) * speed;
		float fractionOfJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(start, enemy.position + -t, fractionOfJourney);
		Debug.Log(t);
	}
}
