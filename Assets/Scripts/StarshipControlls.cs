using UnityEngine;
using System.Collections;

public class StarshipControlls : MonoBehaviour {

	//sensetivity for rolling and turning ship
	float rotationSensetivity = 50f;
	float rollingVelocity = 100f;
	
	float forwardspeed;

	//speed up and slow down
	float maxSpeed = 400;
	float minSpeed = 5;

	//acceleration
	float speedUpFactor = 3;

	float maxMinRotationSpeed = 100;
	float maxMinRollingSpeed = 200;

	//on hit effect
	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
		//hide cursor
		//Screen.lockCursor = false;
		Screen.showCursor = false;

		//initial speed
		forwardspeed = 40;
	}
	
	// Update is called once per frame
	void Update () {

		//forward speed
		Vector3 speed = new Vector3 (0, 0, forwardspeed);
		Vector3 directionalSpeed = transform.rotation * speed;
		transform.position += directionalSpeed * Time.deltaTime;

		//Rotation - mathf.clamp limits the rotation - mouse controlls
//		float verticalRotation = Mathf.Clamp( Input.GetAxis ("Mouse Y") * rotationSensetivity , -maxMinRotationSpeed, maxMinRotationSpeed);
//		transform.Rotate (verticalRotation * Time.deltaTime, 0, 0);

		//rolling - Math.Clamp limits speed - mouse controlls
//		float rotLeftRight = Mathf.Clamp( Input.GetAxis("Mouse X") * rollingVelocity , -maxMinRollingSpeed, maxMinRollingSpeed);
//		transform.RotateAround(transform.position, transform.forward, -(Time.deltaTime * rotLeftRight));

		//Rotation - mathf.clamp limits the rotation - keyboard controlls
		float verticalRotation = Mathf.Clamp( Input.GetAxis ("Vertical") * rotationSensetivity , -maxMinRotationSpeed, maxMinRotationSpeed);
		transform.Rotate (verticalRotation * Time.deltaTime, 0, 0);

		//rolling - Math.Clamp limits speed - keyboard controlls
		float rotLeftRight = Mathf.Clamp( Input.GetAxis("Horizontal") * rollingVelocity , -maxMinRollingSpeed, maxMinRollingSpeed);
		transform.RotateAround(transform.position, transform.forward, -(Time.deltaTime * rotLeftRight));

		//throttle flight speed
		if (forwardspeed <= maxSpeed && forwardspeed >= minSpeed)
						forwardspeed += Input.GetAxis ("Engine") * speedUpFactor;

		//ajust speed if it exceeds limit
		if (forwardspeed > maxSpeed)
						forwardspeed = maxSpeed;

		if (forwardspeed < minSpeed)
						forwardspeed = minSpeed;
	}

	//check for collision with certain tags
	void OnCollisionEnter(Collision col)
	{
		//when hitting a planet/asteroid
		if(col.gameObject.CompareTag("Planet"))
		{
			Instantiate(explosionEffect, col.gameObject.transform.position, Quaternion.identity);
			print ("crash!!!!!");
		}
	}
}
