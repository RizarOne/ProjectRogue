using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;

    public Text killedText;
    public static int killedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;


    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing;
    bool canDash;


    [SerializeField] private AudioSource attackSound;

    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource moveSound;

    void Start()
    {
        


        rb = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isDashing)
        {
            return;
        }


        InputManagment();

        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
        animator.SetFloat("Speed", moveDir.sqrMagnitude);

  
        fireDelay = GameManager.FireRate;

        moveSpeed = GameManager.MoveSpeed;

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");
        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

                killedText.text = "Kills Collected: " + killedAmount;


        if (Input.GetKeyDown(KeyCode.Space)&& canDash)
        {
            StartCoroutine(Dash());
            dashSound.Play();
        }

    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
            );
        attackSound.Play();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Move();
    }

    void InputManagment()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        
    }

    void Move()
    {
        //rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        Vector2 direction = moveDir.normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDir.normalized.x * dashSpeed, moveDir.normalized.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        
        
    }
}