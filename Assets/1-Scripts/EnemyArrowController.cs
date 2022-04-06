using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowController : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector3 direction;
    private float speed;
    private bool arrowDead;

    public BoxCollider2D trigger;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 direction, float speed)
    {
        Vector3 positionToAim = gameObject.transform.position + direction * 10f;
        transform.up = positionToAim - transform.position;

        this.direction = direction;
        this.speed = speed;

        rb.velocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        if(!arrowDead)
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            
            trigger.enabled = false;
            speed = 0;
            Destroy(this.gameObject, 0.8f);
            AudioController.Instance.PlayArrowHitSound();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
          
        }

        if (collision.gameObject.CompareTag("Arrow"))
        {
            trigger.enabled = false;
            arrowDead = true;
            Destroy(this.gameObject, 1f);

        }
    }
}
