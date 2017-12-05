using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// references to other GameObjects
	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _explosionPrefab;
	[SerializeField] private GameObject _shieldGameObject;
	[SerializeField] private GameObject[] _engines;

	// power ups 
	[SerializeField] private bool _tripleShotEnabled = false;
	[SerializeField] private bool _speedBoostEnabled = false;
	[SerializeField] private bool _shieldEnabled = false;

	// variables for firing lasers
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _fireRate = 0.25f;	
	private float _nextFireTime = 0.0f;
	
	// variables to update UI
	private UIManager _uiManager;
	public int lives = 3;

	// references to other managers
	private SpawnManager _spawnManager;
	private GameManager _gameManager;
	private AudioManager _audioManager;

	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3(0, 0, 0);

		// set handles
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

		if (_uiManager != null)
		{
			_uiManager.UpdateLives(lives);
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		ControlMovement();

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Shoot();			
		}
	}	

	private void ControlMovement ()
	{
		// enable vertical and horizontal movement through user input
		if (_speedBoostEnabled)
		{
			_speed *= 1.5f;
		}
		else 
		{
			_speed = 5.0f;
		}
		transform.Translate(Vector3.right * _speed * Input.GetAxis("Horizontal") * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * Input.GetAxis("Vertical") * Time.deltaTime);

		// set x bounds - let's wrap around!
		if (transform.position.x > 9.4f)
		{
			transform.position = new Vector3(-9.4f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.4f)
		{
			transform.position = new Vector3(9.4f, transform.position.y, 0);
		}

		// set y bounds
		if (transform.position.y > 0) 
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f) 
		{
			transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
		}		
	}
	private void Shoot ()
	{
		// play laser sound
		_audioManager.PlayAudio(AudioManager.GameAudio.Laser);

		if (Time.time > _nextFireTime)
		{
			if (_tripleShotEnabled)
			{
				// spawn the other two lasers at (+- 0.55, 0.08, 0) relative to current position
				Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.08f, 0f), Quaternion.identity);
				Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.08f, 0f), Quaternion.identity);
			}

			Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
			_nextFireTime = Time.time + _fireRate;
		}	
	}

	public void TakeDamage()
	{
		// take one hit without sustaining damage if you have the shield
		if (_shieldEnabled)
		{
			_shieldEnabled = false;
			_shieldGameObject.SetActive(false);
		}
		else
		{
			--lives;
			_uiManager.UpdateLives(lives);

			// show damage correctly according to number of hits taken
			if (lives > 0)
			{
				_engines[lives - 1].SetActive(true);
			}
			else 
			{
				// play explosion sound
				_audioManager.PlayAudio(AudioManager.GameAudio.Explosion);

				Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
				Destroy(gameObject);

				// end the game
				_gameManager.EndGame();
			}
		}
	}

	public void EnableTripleShot()
	{
		_tripleShotEnabled = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	public void EnableSpeedBoost()
	{
		_speedBoostEnabled = true;
		StartCoroutine(SpeedBoostPowerDownRoutine());
	}

	public void EnableShield()
	{
		_shieldEnabled = true;
		_shieldGameObject.SetActive(true);
	}

	// Cool-down systems for powerUps
	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		_tripleShotEnabled = false;
	}

	public IEnumerator SpeedBoostPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		_speedBoostEnabled = false;
	}
}
