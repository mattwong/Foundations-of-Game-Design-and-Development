using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject restartButton;

    void Awake()
    {
        //Time.timeScale = 0.0f;
        restartButton.SetActive(false);
    }

    void Start()
    {
        GameManager.OnPlayerDeath += EndScreen;
    }

    // public void StartButtonClicked()
    // {
    //     foreach (Transform eachChild in transform)
    //     {
    //         if (eachChild.name != "Score Text")
    //         {
    //             Debug.Log("Child found. Name: " + eachChild.name);
    //             // disable them
    //             eachChild.gameObject.SetActive(false);
    //             Time.timeScale = 1.0f;
    //         }
    //     }
    // }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void EndScreen()
    {
        restartButton.SetActive(true);
    }
}
