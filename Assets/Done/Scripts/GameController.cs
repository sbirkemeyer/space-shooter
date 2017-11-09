using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	public GUIText moneyText;
	public GUIText moneyInfoText;
	public int upgrade;


	private bool restart;
	private bool gameOver;
	private int score;
	private int money;

	void Start ()
	{
		score = 10;
		money = 0; 
		upgrade = 1;

		gameOver = false;
		restart = false;

		moneyInfoText.text = "";
		gameOverText.text = "";
		restartText.text = "";

		MoneyInfoText ();
		UpdateScore ();
		UpdateMoney ();

		StartCoroutine (SpawnWaves ());
	}






	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if (money > (upgrade * 100)) 
			{
				money -= (upgrade * 100);
				upgrade++;
			}
		}
		MoneyInfoText ();
	}
		
	void Update()
	{
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}






	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			AddMoney (hazardCount);
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			hazardCount++;
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restartText.text = "Press 'R' to Restart";
				restart = true;
				break;
			}
		}
	}






	public void AddScore(int newScoreValue)
	{
		score += newScoreValue ;
		UpdateScore ();
		if (score <= 0) 
		{
			GameOver();
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}






	public void AddMoney(int newMoneyValue)
	{
		money += newMoneyValue ;
		UpdateMoney ();
	}

	void UpdateMoney()
	{
		moneyText.text = "Money: " + money;
	}

	public void Upgrade()
	{
		if (enoughMoney()) 
		{
			money -= (upgrade * 100);
			upgrade++;
		}
		MoneyInfoText ();
	}

	public bool enoughMoney()
	{
		return (money > (upgrade * 100));
	}
		
	void MoneyInfoText()
	{
		moneyInfoText.text = "For an Upgrade press Q: Money needed for next upgrade: " + (upgrade * 100);
	}

	public int Money()
	{
		return money;
	}






	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}