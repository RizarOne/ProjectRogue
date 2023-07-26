using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartCoin : MonoBehaviour
{
    public GameObject gameCompleteMenu;
    public GameObject player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {     
            Instantiate(gameCompleteMenu);
            gameCompleteMenu.SetActive(true);
            Destroy(gameObject);
            GameManager.moveSpeed = 0f;
        }

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
