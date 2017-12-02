using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Sprite[] lives;
	[SerializeField] private Image _livesImageDisplay;

	public Text scoreText;

	public int score = 0;


	private SpawnManager _spawnManager;

	void Start()
	{
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
	}

	// Display image corresponding to number of current lives
	public void UpdateLives(int currentLives)
	{
		_livesImageDisplay.sprite = lives[currentLives];	
	}

	public void UpdateScore()
	{
		score += 10;
		scoreText.text = "Score: " + score;
	}

	public void ResetUI()
	{
		score = 0;
		scoreText.text = "Score: " + score;
		_livesImageDisplay.sprite = lives[3];
	}
}
