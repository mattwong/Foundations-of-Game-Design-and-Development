using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public delegate void gameEvent();
	public static event gameEvent OnPlayerDeath;
	public static event gameEvent SpawnEnemy;
    public Text score;
	private int playerScore = 0;
	
	public void increaseScore(){
		playerScore += 1;
		score.text = "SCORE: " + playerScore.ToString();
		Debug.Log("SCORE: " + playerScore);
		SpawnEnemy();
	}

	public void damagePlayer(){
		OnPlayerDeath();
		Debug.Log("Player dies");
	}
}
