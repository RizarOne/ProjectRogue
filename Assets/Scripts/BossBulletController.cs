using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public float lifeTime;

    public bool isEnemyBullet = false;

    private Vector2 lastPos;

    private Vector2 curPos;

    private Vector2 playerPos;



    void Start()
    {
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameManager.BulletSize, GameManager.BulletSize);
        }
    }

    void Update()
    {
        if (isEnemyBullet)
        {
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);

            //transform.Translate(playerPos * 1f * Time.deltaTime); Ei toiminut, olisi saanut bulletit menem‰‰n lastposia pidemm‰lle.


            if (curPos == lastPos)
            {
                Destroy(gameObject);
            }



            lastPos = curPos;
        }

    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Enemy" && !isEnemyBullet)
        {
            col.gameObject.GetComponent<EnemyController>().OnHit();
            Destroy(gameObject);
        }

        if (col.tag == "Player" && isEnemyBullet)
        {
            GameManager.DamagePlayer(1);                 /// T‰m‰ tekee damagen, keksi uusi keino!
            Destroy(gameObject);
        }

    }
}
