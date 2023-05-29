using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    public static GameManager instance;

    public static float health = 6;

    public static int maxHealth = 6;

    public static float moveSpeed = 4f;

    public static float fireRate = 0.4f;

    public static float bulletSize = 0.5f;

    public bool bootCollected = false;
    public bool screwCollected = false;

    public List<string> collectedNames = new List<string> ();

    public static float Health { get => health; set => health = value; }

    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public Text healthText;

    public static GameObject player;
    



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        healthText.text = "Health: " + health;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


      }


    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if(Health <= 0)
        {
           health = 0;
            Debug.Log("Died");
            Destroy(player);
            //Time.timeScale = 0; Animaatio ei toimi deathscreenissä jos tämän enabloin
            OnPlayerDeath?.Invoke();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);

        foreach (string i in collectedNames)
        {
            switch (i)
            {
                case "Boot":
                    bootCollected = true;
                    break;

                case "Screw":
                    screwCollected = true;
                    break;

            }
        }


        if (bootCollected && screwCollected)
        {
            FireRateChange(0.1f);

        }

        

    }

}
