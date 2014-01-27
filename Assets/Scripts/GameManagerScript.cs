using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
	EnemySpawnerScript enemySpawnerScript;

	void Start()
	{
		GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");

		foreach (var spawner in enemySpawners)
		{
			enemySpawnerScript = spawner.GetComponent<EnemySpawnerScript>();
			enemySpawnerScript.StartTimer();
		}
	}
	
	void Update()
	{
	}
}
