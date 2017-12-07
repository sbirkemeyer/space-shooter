using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public int moneyValue;
	private GameController gameController;
	private PlayerController player;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null)
		{
			player = playerObject.GetComponent <PlayerController>();
		}
		if (player == null)
		{
			Debug.Log ("Cannot find 'PlayerController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary") {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") 
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		gameController.AddMoney (moneyValue);
		gameController.AddShieldBonus();

		if (other.tag == "shield") 
		{
			Destroy (gameObject);
			player.SetShield ();
		}
		else
		{
			Destroy (other.gameObject);
		}
		Destroy (gameObject);

	}
}
