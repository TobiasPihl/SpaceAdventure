using UnityEngine;
using System.Collections;

public class LaserProperties : MonoBehaviour {

	float projectileSpeed = 2000;

	float projectileLifeSpawn = 2f;
	float projectileLifeSpawnTimer;

	//on hit effect
	public GameObject explosionEffect;
	
	// Use this for initialization
	void Start () {
		projectileLifeSpawnTimer = 0;
	}

	// Update is called once per frame
	void Update () {

		//bullet transform, rotation and speed
		Vector3 speed = new Vector3 (0, 0, projectileSpeed);
		Vector3 directionalSpeed = transform.rotation * speed;
		transform.position += directionalSpeed * Time.deltaTime;

		//timer count
		projectileLifeSpawnTimer += Time.deltaTime;

		//destroy object if it reaches its lifespawn
		if (projectileLifeSpawnTimer >= projectileLifeSpawn) {
						Destroy (gameObject);
		}
	}


	//check for collision with certain tags
	void OnCollisionEnter(Collision col)
	{
		//when hitting a planet/asteroid
		if(col.gameObject.CompareTag("Planet"))
		{
			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(col.gameObject);
		}

		//when hitting player
		if(col.gameObject.CompareTag("Player"))
		{
			print ("I'm hit!");

			Destroy(gameObject);
		}

		//when hitting an enemy
		if(col.gameObject.CompareTag("Enemy"))
		{
			print("nice shot");

			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(col.transform.parent.gameObject);
		}
	}
}
