using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverMenu;
    public AudioSource deathSound;


    private void Start()
    {
        deathSound = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        GameManager.OnPlayerDeath += EnableGameOverMenu;
        deathSound.Play();
    }

    private void OnDisable()
    {

        GameManager.OnPlayerDeath -= EnableGameOverMenu;
    }
    


    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("BasementMain");
        DungeonCrawlerController.positionsVisited.Clear();
        GameManager.health = 6f;
        GameManager.fireRate = 0.6f;
        GameManager.bulletSize = 0.5f;
        GameManager.moveSpeed = 4f;
        PlayerMovement.killedAmount = 0;
        
        Time.timeScale = 1;

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        DungeonCrawlerController.positionsVisited.Clear();
        GameManager.health = 6f;
        GameManager.fireRate = 0.6f;
        GameManager.bulletSize = 0.5f;
        GameManager.moveSpeed = 4f;
        PlayerMovement.killedAmount = 0;

        Time.timeScale = 1;
    }

}
