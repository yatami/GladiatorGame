using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float releaseAnimationTime;

    public Animator bodyAnimator;
    public GameObject chestFront;
    public GameObject chestBack;
    public GameObject headFront;
    public GameObject headBack;
    public GameObject helmetFront;
    public GameObject helmetBack;
    public GameObject handRight;
    public GameObject handLeft;
    public GameObject arrow;
    public GameObject bow;

    public GameObject drawnHandRight;
    public GameObject drawnHandLeft;
    public GameObject drawnArrow;
    public GameObject drawnBow;


    public static PlayerAnimationController Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.gameOverEvent.AddListener(GameOver);
    }

    private void GameOver()
    {
        PlayDeathAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRunning(bool isRunning)
    {
        bodyAnimator.SetBool("IsRunning", isRunning);
    }

    public void ChangeSides(bool isFacingRight)
    {
        if(isFacingRight)
        {
            chestFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.x);
            chestBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.x);
            headFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.x);
            headBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.z);
            helmetFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.z);
            helmetBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.z);

        }
        else
        {
            chestFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            chestBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            headFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            headBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.z);
            helmetFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.z);
            helmetBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.z);
        }
    }

    public void ChangeFrontBack(bool isFacingBack)
    {
        if(isFacingBack)
        {
            chestFront.SetActive(true);
            chestBack.SetActive(false);
            headFront.SetActive(true);
            headBack.SetActive(false);

            handRight.GetComponent<SpriteRenderer>().sortingOrder = 11;
            handLeft.GetComponent<SpriteRenderer>().sortingOrder = 11;
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 9;
            bow.GetComponent<SpriteRenderer>().sortingOrder = 10;

            drawnHandRight.GetComponent<SpriteRenderer>().sortingOrder = 11;
            drawnHandLeft.GetComponent<SpriteRenderer>().sortingOrder = 11;
            drawnArrow.GetComponent<SpriteRenderer>().sortingOrder = 9;
            drawnBow.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
        else
        {
            chestFront.SetActive(false);
            chestBack.SetActive(true);
            headFront.SetActive(false);
            headBack.SetActive(true);

            handRight.GetComponent<SpriteRenderer>().sortingOrder = -10;
            handLeft.GetComponent<SpriteRenderer>().sortingOrder = -10;
            arrow.GetComponent<SpriteRenderer>().sortingOrder = -10;
            bow.GetComponent<SpriteRenderer>().sortingOrder = -10;

            drawnHandRight.GetComponent<SpriteRenderer>().sortingOrder = -10;
            drawnHandLeft.GetComponent<SpriteRenderer>().sortingOrder = -10;
            drawnArrow.GetComponent<SpriteRenderer>().sortingOrder = -10;
            drawnBow.GetComponent<SpriteRenderer>().sortingOrder = -10;
        }
    }

    public float PlayReleaseAnimation()
    {
        StartCoroutine(ReleaseAnimationCor());
        return releaseAnimationTime;
    }

    IEnumerator ReleaseAnimationCor()
    {
        bow.SetActive(false);
        drawnBow.SetActive(true);
        yield return new WaitForSeconds(releaseAnimationTime);
        bow.SetActive(true);
        drawnBow.SetActive(false);
        arrow.SetActive(false);
        yield break;
    }

    public void MakeArrowVisible()
    {
        arrow.SetActive(true);
    }

    public void PlayDeathAnimation()
    {
        bodyAnimator.Play("PlayerCharacterDeath", 0);
    }



}
