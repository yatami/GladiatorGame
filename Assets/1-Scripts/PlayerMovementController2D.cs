using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController2D : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public ParticleSystem dashParticle;

    private Rigidbody2D rb;

    private float inputX;
    private float inputY;
    private Vector2 movementDir;
    private Vector2 dashDir;
    private PlayerAnimationController animationControllerRef;
    private float dashCooldownTimer;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        dashCooldownTimer = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animationControllerRef = gameObject.GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        movementDir = new Vector2(inputX, inputY).normalized;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashCooldownTimer > dashCooldown)
            {
                dashCooldownTimer = 0;
                dashDir = movementDir;
                isDashing = true;
                StartCoroutine(StopDashingCor());
                dashParticle.Play();
            }
           
        }

        dashCooldownTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(isDashing == false)
        {
            rb.velocity = movementDir * speed;
            if (rb.velocity.magnitude > 0)
            {
                animationControllerRef.SetRunning(true);
            }
            else
            {
                animationControllerRef.SetRunning(false);
            }
        }
        else
        {
            rb.velocity = dashDir * dashSpeed;
        }
    }

    IEnumerator StopDashingCor()
    {
        yield return new WaitForSeconds(dashTime);
        dashParticle.Stop();
        isDashing = false;
        yield break;
    }

}
