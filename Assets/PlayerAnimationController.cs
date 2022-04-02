using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator bodyAnimator;
    public GameObject chestFront;
    public GameObject chestBack;
    public GameObject headFront;
    public GameObject headBack;
    public GameObject handRight;
    public GameObject handLeft;
    public GameObject arrow;
    public GameObject bow;

    public static PlayerAnimationController Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

        }
        else
        {
            chestFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            chestBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            headFront.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 0, chestFront.transform.rotation.eulerAngles.x);
            headBack.transform.rotation = Quaternion.Euler(chestFront.transform.rotation.eulerAngles.x, 180, chestFront.transform.rotation.eulerAngles.z);
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

            handRight.GetComponent<SpriteRenderer>().sortingOrder = 10;
            handLeft.GetComponent<SpriteRenderer>().sortingOrder = 10;
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 10;
            bow.GetComponent<SpriteRenderer>().sortingOrder = 10;
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
        }
    }

}
