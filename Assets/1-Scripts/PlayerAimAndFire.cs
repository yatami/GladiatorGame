using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerAimAndFire : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineRef;

    public float orthographicStartSize;
    public float orthographicEndSize;
    public float camShakeDuration;

    public float arrowSpeed;
    public float arrowCooldown;

    public GameObject bowRoot;
    public GameObject arrowReleaseCenter;
    public GameObject arrow;

    private float cooldownTimer;
    private PlayerAnimationController animationControllerRef;

    // Start is called before the first frame update
    void Start()
    {
        animationControllerRef = gameObject.GetComponent<PlayerAnimationController>();
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePositionWorld - bowRoot.transform.position;
        direction = new Vector3(direction.x, direction.y,0).normalized;
        Debug.Log(direction);

        bowRoot.transform.LookAt(new Vector3(mousePositionWorld.x, mousePositionWorld.y, 0));

        cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (cooldownTimer > arrowCooldown)
            {
                cooldownTimer = 0;
                ReleaseArrow(direction);
            }
        }

        if (cooldownTimer > arrowCooldown)
        {
            PlayerAnimationController.Instance.MakeArrowVisible();
        }

        if (direction.x > 0)
        {
            animationControllerRef.ChangeSides(true);
        }
        else
        {
            animationControllerRef.ChangeSides(false);
        }

        if(direction.y > 0)
        {
            animationControllerRef.ChangeFrontBack(false);
        }
        else
        {
            animationControllerRef.ChangeFrontBack(true);
        }
    }


    private void ReleaseArrow(Vector3 direction)
    {
        StartCoroutine(ReleaseArrowCor(direction));
       
        
    }

    IEnumerator ReleaseArrowCor(Vector3 direction)
    {
        float animTime = PlayerAnimationController.Instance.PlayReleaseAnimation();
        yield return new WaitForSeconds(animTime);
        GameObject newArrow = Instantiate(arrow, arrowReleaseCenter.transform.position, Quaternion.identity);
        newArrow.GetComponent<ArrowController>().SetDirection(direction, arrowSpeed);
        // Release effects
        cameraShake();

        yield break;
    }

    private void cameraShake()
    {
        DOTween.To(() => cinemachineRef.m_Lens.OrthographicSize, x => cinemachineRef.m_Lens.OrthographicSize = x, orthographicStartSize, camShakeDuration/3).OnComplete((
                () =>
                {
                    DOTween.To(() => cinemachineRef.m_Lens.OrthographicSize, x => cinemachineRef.m_Lens.OrthographicSize = x, orthographicEndSize, camShakeDuration*2/3);
                }));
    }
}
