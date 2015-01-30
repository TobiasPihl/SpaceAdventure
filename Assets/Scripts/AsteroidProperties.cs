using UnityEngine;
using System.Collections;

public class AsteroidProperties : MonoBehaviour {

	float maxDistance = 3000;

	Transform playerPos;

	Vector3 vecToPlayer;
	float distanceToPlayer;

	//spawner script for reference
	AsteroidSpawner spawnScript;


	// Use this for initialization
	void Start () {
		playerPos = GameObject.FindWithTag("Player").transform;
		spawnScript = GameObject.FindGameObjectWithTag ("Respawn").gameObject.GetComponent<AsteroidSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

		//calculate distance to player
		vecToPlayer = transform.position - playerPos.position;
		distanceToPlayer = vecToPlayer.magnitude;

		//if distance to player is to far, despawn object and spawn a new asteroid
		if (distanceToPlayer > maxDistance) {
			Destroy (gameObject);

			//send call to spawnscript
			//spawnScript.SpawnAsteroid();
		}

		
	}
}
