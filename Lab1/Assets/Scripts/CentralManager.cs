using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
	private GameManager gameManager;
	public GameObject powerupManagerObject;
	private PowerupManager powerUpManager;
	public static CentralManager centralManagerInstance;
	
	void  Awake(){
		centralManagerInstance = this;
	}

	void  Start()
	{
		gameManager = gameManagerObject.GetComponent<GameManager>();
		powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
	}

	public void increaseScore(){
		gameManager.increaseScore();
	}

	public void damagePlayer(){
		gameManager.damagePlayer();
	}

	public void consumePowerup(KeyCode k, GameObject g){
		powerUpManager.consumePowerup(k,g);
	}

	public void addPowerup(Texture t, int i, ConsumableInterface c){
		powerUpManager.addPowerup(t, i, c);
	}
}
