using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
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

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animationController = GetComponent<EnemyAnimationController>();
        animationController.SetRunning(true);
    }

    private void Update()
    {
        if (AIStart)
        {
            navMeshAgent.speed = Speed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Target.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        if (!Death)
        {
            DirEnemy();
            FindDistanceandAttack();
        }
        
    }
    public void ShotEnemy()
    {
        --Health;
        if (Health == 0)
        {
            Death = true;
            animationController.PlayDeathAnim();
            Speed = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,1f);
        }
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
            animationController.ChangeFrontBack(false);
        }
        else
        {
            FrontBack = true;
            animationController.ChangeFrontBack(true);
        }
        I_DotRight = Vector3.Dot(Vector3.right, I_EnemytoTarget);
        if (I_DotRight > 0)
        {
            animationController.ChangeSides(true);
        }
        else
        {
            animationController.ChangeSides(false);
        }
    }
    private void FindDistanceandAttack()
    {
        if (Death) return;
        if (Vector3.Distance(transform.position,Target.transform.position) < HittingDistance)
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
}
