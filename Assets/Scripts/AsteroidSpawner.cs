using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject asteroidPrefab;

	float fieldRadius = 3000;
	float spawnRadious = 250;

	int numberOfAsteroids = 2000;

	Transform player;
	
	float spawnRate = 0.1f;
	float spawnTimer;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < numberOfAsteroids; ++i)
		{
			//plyer position
			player = GameObject.FindWithTag("Player").transform;

			GameObject newAsteroid = (GameObject)Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation);
			float size = Random.Range(1, 50);
			newAsteroid.transform.localScale = Vector3.one * size;

			spawnTimer = spawnRate;
		}
	}
	
	// Update is called once per frame
	void Update () {

	
		//attempt of infinite spawn
//		if (spawnTimer < 0) {
//			SpawnAsteroid();
//			spawnTimer = spawnRate;
//		}
//		spawnTimer -= Time.deltaTime;
	}


	public void SpawnAsteroid() {
		print ("spawned a new asteroid");

		//instantiate the new asteroid
		GameObject newAsteroid = (GameObject)Instantiate(asteroidPrefab, player.position + (Random.onUnitSphere*spawnRadious), Random.rotation);
		float size = Random.Range(1, 25);
		newAsteroid.transform.localScale = Vector3.one * size;
	}
}
