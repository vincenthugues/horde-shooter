using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
	public enum GameState
	{
		Uninitialized,
		Initialized,
		Running,
		Ended
	}

	private GameState gameState;
	private EnemySpawnerScript enemySpawnerScript;

	void Awake()
	{
		gameState = GameState.Initialized;
	}

	void Start()
	{
		GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");

		foreach (var spawner in enemySpawners)
		{
			enemySpawnerScript = spawner.GetComponent<EnemySpawnerScript>();
			enemySpawnerScript.StartTimer();
		}

		gameState = GameState.Running;
	}
	
	void Update()
	{
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 240, 80), gameState.ToString());
	}

	private void TriggerEndZone()
	{
		gameState = GameState.Ended;
	}
}
