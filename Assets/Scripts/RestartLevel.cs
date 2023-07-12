using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
         bool restart;
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            restart = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && !restart)
            {                
                restart = true;
                SceneManager.LoadScene("BasementMain");
                DungeonCrawlerController.positionsVisited.Clear();
                GameManager.health = 6f;
                GameManager.fireRate = 0.6f;
                GameManager.bulletSize = 0.5f;
                GameManager.moveSpeed = 4f;
                PlayerMovement.killedAmount = 0;

                Time.timeScale = 1;
            }
        }
    
    
}
