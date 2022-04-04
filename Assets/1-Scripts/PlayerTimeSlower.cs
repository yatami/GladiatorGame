using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerTimeSlower : MonoBehaviour
{
    public float timeScaleAmount;
    public PostProcessVolume postProcess;
    GameObject playerRef;

    private ColorGrading colorGrade;

    private float timer;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        postProcess.profile.TryGetSettings(out colorGrade);
        GameController.Instance.gameOverEvent.AddListener(GameOver);
    }

    private void GameOver()
    {
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerRef.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.CompareTag("PlayerKiller"))
        {
            Time.timeScale = timeScaleAmount;
            colorGrade.saturation.value = timer * -80;
            if(timer <= 1)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            Time.timeScale = 1;
            colorGrade.saturation.value = timer * -80f;
            if(!gameOver)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
            }
            else
            {
                if (timer <= 1)
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }
}
