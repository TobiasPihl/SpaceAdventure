using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	public GameObject bulletSpawn;
	public GameObject bullet;

	float fireRate = 5f;
	float fireTimer;

	float burstRate = 0.1f;
	float burstTimer;
	int burstAmount = 5;
	int burstCounter;

	Transform target;

	// Use this for initialization
	void Start () {

		burstTimer = 0;
		fireTimer = fireRate;
		burstCounter = burstAmount;

		//find player in order to aim right
		target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		//look at target
		transform.LookAt (target);

		//time intervall for each burst to enable 
		if (fireTimer <= 0) {

			//time interval for each shot in the burst
			if (burstTimer <= 0) {

				//if bullets left, continue firing
				if(burstCounter > 0) {

					Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
					burstCounter --;
					burstTimer = burstRate;
				}
				//if burst ends, refill ammo and reset fire intervall timer
				else {

					burstCounter = burstAmount;
					fireTimer = fireRate;
				}
			}

			burstTimer -= Time.deltaTime;
		}

		fireTimer -= Time.deltaTime;
	}
}
