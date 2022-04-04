using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public int playerHealth;

    private BoxCollider2D boxColliderRef;

    // Start is called before the first frame update
    void Start()
    {
        boxColliderRef = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerKiller"))
        {
            --playerHealth;
            if(playerHealth <= 0)
            {
                GameController.Instance.gameOverEvent.Invoke();
                boxColliderRef.isTrigger = true;
            }
        }
    }

  
}
