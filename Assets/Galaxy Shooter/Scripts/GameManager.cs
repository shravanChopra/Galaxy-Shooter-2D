using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// control game state
	private bool _isGameRunning = false;
	public bool isGameRunning 
	{
		get { return _isGameRunning; }
	}

	// reference to Player
	[SerializeField] private GameObject _playerPrefab;

	// reference to startUp Image
	[SerializeField] private GameObject _startupImage;

	// reference to other managers
	private SpawnManager _spawnManager;
	private UIManager _uiManager;

	// Use this for initialization
	void Start () 
	{
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// player starts game by hitting the space key
		if (Input.GetKeyDown(KeyCode.Space) && !isGameRunning)
		{
			StartGame();
		}
	}

	// Hide the startup image, instantiate the player, and start all the spawn routines
	void StartGame()
	{
		_startupImage.SetActive(false);
		_isGameRunning = true;
		Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
		_spawnManager.StartSpawnRoutines();
		_uiManager.ResetUI();
	}

	// end the game, stop all spawn routines, and display title image 
	public void EndGame()
	{
		_startupImage.SetActive(true);
		_isGameRunning = false;

		// Destroy all 'Destructibles'
		foreach (GameObject destructible in GameObject.FindGameObjectsWithTag("Destructible"))
		{
			Destroy(destructible);
		}
	}


}
