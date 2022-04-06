using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimAndFire : MonoBehaviour
{
    public float arrowSpeed;
    public float arrowCooldownMin;
    public float arrowCooldownMax;


    public GameObject bowRoot;
    public GameObject arrowReleaseCenter;
    public GameObject arrow;

    private float cooldownTimer;
    private EnemyAnimationController animationControllerRef;
    private bool gameStarted;

    private GameObject playerRef;
    private float arrowCooldown;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.gameOverEvent.AddListener(GameOver);
        GameController.Instance.startGameEvent.AddListener(GameStarted);

        animationControllerRef = gameObject.GetComponent<EnemyAnimationController>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        arrowCooldown = UnityEngine.Random.Range(arrowCooldownMin, arrowCooldownMax);

    }

    public void GameStarted()
    {
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            Vector3 direction = playerRef.transform.position - bowRoot.transform.position;
            direction = new Vector3(direction.x, direction.y, 0).normalized;

            bowRoot.transform.LookAt(playerRef.transform.position);

            cooldownTimer += Time.deltaTime;
           
                if (cooldownTimer > arrowCooldown)
                {
                    cooldownTimer = 0;
                    arrowCooldown = UnityEngine.Random.Range(arrowCooldownMin, arrowCooldownMax);
                    ReleaseArrow(direction);
                }
         

            if (cooldownTimer > arrowCooldown)
            {
                animationControllerRef.MakeArrowVisible();
            }

            if (direction.x > 0)
            {
                animationControllerRef.ChangeSides(true);
            }
            else
            {
                animationControllerRef.ChangeSides(false);
            }

            if (direction.y > 0)
            {
                animationControllerRef.ChangeFrontBack(false);
            }
            else
            {
                animationControllerRef.ChangeFrontBack(true);
            }
        }
    }

    public void GameOver()
    {
        gameStarted = false;
    }

    private void ReleaseArrow(Vector3 direction)
    {
        StartCoroutine(ReleaseArrowCor(direction));
    }

    IEnumerator ReleaseArrowCor(Vector3 direction)
    {
        float animTime = animationControllerRef.PlayReleaseAnimation();
        yield return new WaitForSeconds(animTime);
        GameObject newArrow = Instantiate(arrow, arrowReleaseCenter.transform.position, Quaternion.identity);
        newArrow.GetComponent<EnemyArrowController>().SetDirection(direction, arrowSpeed);

        yield return null;
    }
}
