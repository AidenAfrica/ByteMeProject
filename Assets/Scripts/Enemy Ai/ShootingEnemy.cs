using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAngel : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public float Speed;
  //  public float knockbackForce = 10f;
    public GameObject projectilePrefab;
    public Transform shootPoint; // where the projectile comes from/originate
    public float shootingInterval = 2f;
    public float detectionRadius = 10f;
    public float bulletSpeed = 5f;

    private Rigidbody2D rb;
    private Transform CurrentPoint;
    private Transform player;
    private bool facingRight = false; //my enemy is initially facing left
    private float nextShootTime = 0f;
    private bool isFollowingPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentPoint = PointB.transform;
        if (!facingRight)
        {
            Flip();
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer)
        {
            Move();

            // Check if the enemy is facing the player before shooting
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            if ((facingRight && directionToPlayer.x > 0) || (!facingRight && directionToPlayer.x < 0))
            {
                if (Time.time >= nextShootTime)
                {
                    Shoot();
                    nextShootTime = Time.time + shootingInterval;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Move()
    {
        // Follow the player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * Speed;

        // Flip the enemy to face the player
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        Vector2 direction = (player.position - shootPoint.position).normalized;
        projectileRb.velocity = direction * bulletSpeed;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        Debug.Log("Enemy flipsss"); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 2f);
        Gizmos.DrawWireSphere(PointB.transform.position, 2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (shootPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(shootPoint.position, 0.1f);
        }
    }
}
