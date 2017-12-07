using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	private float fireRate;

	private float nextFire;

	private GameController gameController;
	private Mover mover;

	public GameObject shield;
	public float shieldlvl;

	void Start()
	{
		fireRate = 1;

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		GameObject AsteroidObject = GameObject.FindWithTag ("Asteroid");
		if (AsteroidObject != null)
		{
			mover = AsteroidObject.GetComponent <Mover>();
		}
		if (mover == null)
		{
			Debug.Log ("Cannot find 'Mover' script");
		}
	}

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			if (Input.GetKeyDown (KeyCode.Q)) 
			{
				if(gameController.enoughMoney())
				{
					fireRate = (fireRate/2); 
				}
				gameController.Upgrade ();
			}

			if(gameController.Money() >= 1)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource>().Play();
				gameController.AddMoney (-1);
			}
		}
	}

	public void FixedUpdate()
	{
		if (gameController.Shield() == true) 
		{
			shield.SetActive (true);
			gameController.SetShield ();
			shieldlvl++;
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		Rigidbody rb = GetComponent<Rigidbody>();

		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	public void SetShield()
	{
		shieldlvl--;
		if (shieldlvl == 0) 
		{
			shield.SetActive (false);
		}
	}
}
