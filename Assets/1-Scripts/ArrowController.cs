using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDirection(Vector3 direction, float speed)
    {
        Vector3 positionToAim = gameObject.transform.position + direction * 10f;
        gameObject.transform.LookAt(positionToAim);

        rb.velocity = direction * speed;
        Destroy(gameObject, 5f);
    }
}
