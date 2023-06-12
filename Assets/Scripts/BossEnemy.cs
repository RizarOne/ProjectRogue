using System.Collections;
using UnityEngine;

public enum BossState
{
    Idle,

    Follow,

    Die,

    Attack
};

public enum BossType
{
    Melee,

    Ranged
};

public class BossEnemy : MonoBehaviour
{
    GameObject player;

    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;

    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;
    public float damage;

    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;
    public bool notInRoom = false;
    private Vector3 randomDir;


    Animator animator;

    bool isAlive = true;


    public float _health;

    public GameObject bulletPrefab;

    public float Health
    {
        set
        {
            _health = value;
            if (value < 0)
            {
            }

            if (_health <= 0)
            {
                // RoomController.instance.StartCoroutine(RoomController.instance.RoomCouroutine()); tämä checki ajetaan animaation lopussa funktiossa
                //Destroy(gameObject); Animaatio ei mene loppuun jos on tässä
                animator.SetBool("isAlive", false);
                speed = 0;


            }
        }
        get
        {
            return _health;
        }
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("isAlive", isAlive);
        animator.SetBool("isMoving", false);

    }

    void Update()
    {
        switch (currState)
        {
            //case (EnemyState.Idle):
            // Idle();
            // break;
       

            case (EnemyState.Follow):
                Follow();
                break;

            case (EnemyState.Die):
                break;

            case (EnemyState.Attack):
                Attack();
                break;
        }
        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Idle;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 0)); // flip jos haluaa pyöriä
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    //void Wander()
 //   {
       // if (!chooseDir)
       // {
        //    StartCoroutine(ChooseDirection());
       // }
//
       // transform.position += transform.right * speed * Time.deltaTime;
       // if (IsPlayerInRange(range))
       // {
       //     currState = EnemyState.Follow;
      //  }
    //}

    void Follow()
    {
        if (gameObject != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("isAttacking", false);
        }
    }


    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):


                    GameManager.DamagePlayer(1);
                    animator.SetBool("isAttacking", true);
                    if (_health <= 0)
                    {
                        GameManager.DamagePlayer(-1);
                    }

                    StartCoroutine(CoolDown());
                    break;

                case (EnemyType.Ranged):
                    animator.SetBool("isAttacking", true);
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;


            }

        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void BossHit()
    {
        Health -= damage;
        animator.SetTrigger("hit");

    }

    public void OnObjectDestroyed()

    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCouroutine());
        Destroy(gameObject);
    }
}
