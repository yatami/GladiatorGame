using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndFire : MonoBehaviour
{
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

        if(direction.x > 0)
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

        GameObject newArrow = Instantiate(arrow, arrowReleaseCenter.transform.position, Quaternion.identity);
        newArrow.GetComponent<ArrowController>().SetDirection(direction, arrowSpeed);
    }
}
