using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // fields
    public int damage = 24;
    public float speed;
    public int health = 100;
    Rigidbody2D rb;

    // Positions
    Transform playerPosition;
    public float angryDistance;
    public Transform startPosition;

    //Sounds
    public AudioClip slimeSound;
    public AudioSource Sound;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // health check
        if (health <= 0 || transform.position.y <= -10)
        {
            Destroy(gameObject, 0.1f);
        }

        #region conditions machine
        // distance enemy to start enemy position
        float distanceToStartPos = Vector2.Distance(transform.position, startPosition.position);
        if (distanceToStartPos >= angryDistance)
        {
            Stopping();
        }

        //float distance = Vector2.Distance(transform.position, playerPosition.position);

        // distance player to start enemy position
        float distance = Vector2.Distance(startPosition.position, playerPosition.position);
        if (angryDistance >= distance)
        {
            Hunt();
        }
        else
        {
            Stopping();
        }
        #endregion
    }

    void Hunt()
    {
        if (playerPosition.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
        }

        else if (playerPosition.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
    }
    void Stopping()
    {
        if (transform.position != startPosition.position)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                        startPosition.position, speed / 300 * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        health += damage;

        Sound.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition.position, angryDistance);
    }

    IEnumerator waitSomeSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
    }
}
