using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	//projectile prefab and spawn possitions for projectiles
	public GameObject bullet;
	public GameObject bulletSpawn01;
	public GameObject bulletSpawn02;

	//Gun properties
	float fireRate = 0.5f;
	float fireTimer = 0;

	//mouseposition is used to aim crosshair
	Vector3 mousePos;
	
	//A gun object which look at crosshair, used for aiming
	public GameObject gunAim;
	
	//crosshair object and position
	public Texture2D crosshair;
	float crosshairX;
	float crosshairY;

	//Aim restrictions	
	float mouseAimLimit = 3;
	float maxX;
	float maxY; 
	float minX;
	float minY;

	//create ray and ray properties for shooting
	float range = 2000;
	RaycastHit hit;

	//on hit effect
	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {

		maxX = (mouseAimLimit - 1) * Screen.width / mouseAimLimit;
		maxY = (mouseAimLimit - 1) * Screen.height / mouseAimLimit;
		minX = Screen.width / mouseAimLimit;
		minY = Screen.height / mouseAimLimit;
	}

	// Update is called once per frame
	void Update () {

		//restricting crosshair movement with mouse using mathf.clamp
		mousePos.x = Mathf.Clamp(Input.mousePosition.x, minX, maxX);
		mousePos.y = Mathf.Clamp(Input.mousePosition.y, minY, maxY);

		//mousePos.x = Input.mousePosition.x;
		//mousePos.y = Input.mousePosition.y;

		//ajust mouse coordinates to center of the screen
		crosshairX = mousePos.x - (Screen.width / 2);
		crosshairY = mousePos.y - (Screen.height / 2);

		mousePos.z = 1000.0f; // some very large number

		//convert mouse position to world coordinates
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		//aim gun at the crosshair direction
		gunAim.transform.LookAt (mousePos);

		//if shooting and timer for firing is down
		if (Input.GetButton ("Fire1") && fireTimer >= fireRate) {

			//prefab shooting 
			//Instantiate(bullet, bulletSpawn01.transform.position, gunAim.transform.rotation); 
			//Instantiate(bullet, bulletSpawn02.transform.position, gunAim.transform.rotation); 

			//fire a ray to check for collisions
			Rayshoot ();

			//reset timer
			fireTimer = 0;
		}
		//timer counting
		fireTimer += Time.deltaTime;
	}

	//use raycast to detect collision
	public void Rayshoot(){

		//debug
		Debug.DrawRay (gunAim.transform.position, gunAim.transform.forward * range, Color.green);

		//create the ray startgin at the gun object and in aim direction
		Ray ray = new Ray (gunAim.transform.position, gunAim.transform.forward);

		if (Physics.Raycast (ray, out hit, range)) {

			//hit with asteroid: destroy and explosion
			if(hit.collider.gameObject.tag == "Planet") {

				//delete asteroid
				Destroy(hit.collider.gameObject);

				//explosion
				Instantiate(explosionEffect, hit.point, Quaternion.identity);

			}
		}
	}

	//creater the crosshair
	void OnGUI()
	{
		float xMin = (Screen.width / 2) - (crosshair.width / 2) + crosshairX;
		float yMin = (Screen.height / 2) - (crosshair.height / 2) - crosshairY;
		GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width, crosshair.height), crosshair);
	}
}
