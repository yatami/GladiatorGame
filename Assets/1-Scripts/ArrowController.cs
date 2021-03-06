using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector3 direction;
    private float speed;

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
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            speed = 0;
            AudioController.Instance.PlayArrowHitSound();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioController.Instance.PlayFleshSound();
            //Buraya animasyon eklenir
            Destroy(this.gameObject);
            if(collision.gameObject.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().ShotEnemy(collision.GetContact(0).point);
            }
            else if(collision.gameObject.GetComponent<EnemyMovementArcher>() != null)
            {
                collision.gameObject.GetComponent<EnemyMovementArcher>().ShotEnemy(collision.GetContact(0).point);
            }
        }
    }
}
