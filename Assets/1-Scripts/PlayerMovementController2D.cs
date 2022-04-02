using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController2D : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private float inputX;
    private float inputY;
    private Vector2 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        movementDir = new Vector2(inputX, inputY).normalized;

    }

    private void FixedUpdate()
    {
        rb.velocity = movementDir * speed;
    }

}
