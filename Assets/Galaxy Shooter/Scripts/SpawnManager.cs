using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	// control the spawn routines 
	private bool isNewSession = true;

	// all the GO's to spawn
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private GameObject[] _powerUpPrefabs;

	// reference to GameManager
	private GameManager _gameManager;

	void Start()
	{
		// get handle to Game Manager
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Spawns an enemy every 5 seconds
	private IEnumerator SpawnEnemyRoutine()
	{
		while (_gameManager.isGameRunning)
		{
			Instantiate(_enemyPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
	}

	// Spawn a random powerUp every 5 seconds
	private IEnumerator SpawnPowerUpRoutine()
	{
		while (_gameManager.isGameRunning)
		{
			int randomPowerUp = Random.Range(0, 3);
			Instantiate(_powerUpPrefabs[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
	}

	public void StartSpawnRoutines()
	{
		// start all spawn routines
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnPowerUpRoutine());
	}
}
