using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameObject health1;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI TimerText;
    public GameObject HomeScreenUI;
    public GameObject InGameUI;
    public GameObject CutsceneUI;
    public GameObject PauseScreenUI;
    public static UIController Instance;
    public GameObject endGameUI;
    public TextMeshProUGUI endGameText;


    private int Gold = 0;
    private float UIGameTime;
    private float InGameTime;
    private void Awake()
    {
        Instance = this;
    }

    public void GameStart()
    {
        GameController.Instance.SetStage(Stage.Play);
        GameController.Instance.gameOverEvent.AddListener(GameOver);

        endGameUI.SetActive(false);
        HomeScreenUI.SetActive(false);
        InGameUI.SetActive(true);
        InGameTime = 0;
    }

    private void GameOver()
    {
        GameController.Instance.SetStage(Stage.UI);
        Invoke("ActivateEndGameUI", 1f);
    }

    private void ActivateEndGameUI()
    {
        endGameText.text = "You have delayed the inevitable for " + TimerText.text + " seconds.";
        endGameUI.SetActive(true);
        HomeScreenUI.SetActive(false);
        InGameUI.SetActive(false);
    }

    public void GameQuit() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    private void Update()
    {
        GoldText.text = "$ " + Gold + "";
        if (GameController.Instance.GetStage() == Stage.Play)
        {
            InGameTime = Mathf.Round((Time.time - UIGameTime) * 100);
            if (InGameTime % 3f == 0)
            {
                TimerText.text = InGameTime / 100 + "";
            }
        }
        else
        {
            UIGameTime += Time.deltaTime;
        }
    }

    public void AddGold(int otherGold)
    {
        Gold += otherGold;
    }

    public int GetGold()
    {
        return Gold;
    }

    public void ReceiveDamage()
    {
        health1.SetActive(false);
    }

    public void ActivateCutscene()
    {
        CutsceneUI.SetActive(true);
    }

    public void FinishCutscene()
    {
        CutsceneUI.SetActive(false);
        GameController.Instance.startGameEvent.Invoke();
    }

}
