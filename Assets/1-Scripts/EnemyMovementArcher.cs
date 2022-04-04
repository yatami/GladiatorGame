using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyMovementArcher : MonoBehaviour
{
    public ParticleSystem bloodParticle;
    public Transform Target;
    public int Health;
    public float Speed;
    EnemyAnimationController animationController;

    private Vector3 I_EnemytoTarget;
    private float I_DotUp;
    private float I_DotRight;
    private NavMeshAgent navMeshAgent;
    private bool FrontBack;
    [SerializeField]
    private bool AIStart;
    private bool Death;
    [SerializeField]
    private float HittingDistance = 1.75f;
    private BoxCollider2D boxColliderRef;
    private bool gameOver;
    private EnemyAimAndFire enemyAimController;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animationController = GetComponent<EnemyAnimationController>();
        animationController.SetRunning(true);
        boxColliderRef = gameObject.GetComponent<BoxCollider2D>();
        GameController.Instance.gameOverEvent.AddListener(GameOver);
        enemyAimController = gameObject.GetComponent<EnemyAimAndFire>();
    }

    private void GameOver()
    {
        gameOver = true;
    }

    private void Update()
    {
        if (AIStart)
        {
            navMeshAgent.speed = Speed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Target.position);
            if(navMeshAgent.velocity.magnitude < 0.2f)
            {
                animationController.SetRunning(false);
            }
            else
            {
                animationController.SetRunning(true);
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        if (!Death)
        {
            
        }
        
    }
    public void ShotEnemy(Vector3 contactPos)
    {
        --Health;
        if (Health == 0)
        {
            PlayBloodParticle(contactPos);
            FlyCharacterInTheDirection(contactPos);
            enemyAimController.GameOver();

            AIStart = false;
            Death = true;
            animationController.PlayDeathAnim();
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.Stop();
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            boxColliderRef.isTrigger = true;
            Destroy(this.gameObject,1.5f);
        }
    }

    public void PlayBloodParticle(Vector3 pos)
    {
        Vector3 dir = (gameObject.transform.position - pos).normalized;


        bloodParticle.gameObject.transform.position = pos + dir;
        bloodParticle.gameObject.transform.LookAt(Target);

        bloodParticle.Play();
    }

    public void FlyCharacterInTheDirection(Vector3 pos)
    {
        Vector3 dir = (gameObject.transform.position - Target.position).normalized;

        Vector3 targetPos = gameObject.transform.position + dir * 3;

        gameObject.transform.DOMove(targetPos, 0.5f).SetEase(Ease.OutCubic);
    }

    public void EnemyAI(bool AI)
    {
        AIStart = AI;
    }

    private void DirEnemy()
    {
        I_EnemytoTarget = Target.position - transform.position;
        I_DotUp = Vector3.Dot(Vector3.up,I_EnemytoTarget);
        if (I_DotUp > 0)
        {
            FrontBack = false;
            //animationController.ChangeFrontBack(false);
        }
        else
        {
            FrontBack = true;
           // animationController.ChangeFrontBack(true);
        }
        I_DotRight = Vector3.Dot(Vector3.right, I_EnemytoTarget);
        if (I_DotRight > 0)
        {
            //animationController.ChangeSides(true);
        }
        else
        {
//animationController.ChangeSides(false);
        }
    }
    private void FindDistanceandAttack()
    {
        if (Death) return;
        if (Vector3.Distance(transform.position,Target.transform.position) < HittingDistance && !gameOver)
        {
            Attack();
        }
        
    }
    public void Attack()
    {
        if (FrontBack)
        {
            animationController.PlayBackAttack();
        }
        else
        {
            animationController.PlayFrontAttack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
