using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
	public GameObject[] Enemies;
	public float TimerDelay;
	public int TotalEnemies;
	public int MaximumAliveEnemies;

	private bool started = false;
	private float timer = 0f;
	private int enemiesNb = 0;
	private int aliveEnemies = 0;
	private GameObject player;
	//private GameObject enemiesParent;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (started && enemiesNb < TotalEnemies)
		{
			if (timer > 0f)
				timer -= Time.deltaTime;

			if (timer <= 0f)
			{
				if (aliveEnemies < MaximumAliveEnemies)
					SpawnEnemy();

				timer += TimerDelay;
			}
		}
	}

	public void StartTimer()
	{
		started = true;
	}

	public void EnemyDied()
	{
		aliveEnemies--;
	}

	private void SpawnEnemy()
	{
		GameObject enemy = Instantiate(Enemies[0], transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity) as GameObject;
		EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

		enemy.transform.parent = transform;

		enemyScript.spawner = this;
		enemyScript.target = player;

		enemiesNb++;
		aliveEnemies++;
	}
}
