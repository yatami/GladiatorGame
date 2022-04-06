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
    public Text TimerTextCPixel;

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

    private void Start()
    {
        GameController.Instance.startGameEvent.AddListener(GameStart);
    }

    public void GameStart()
    {
        GameController.Instance.SetStage(Stage.Play);
        GameController.Instance.gameOverEvent.AddListener(GameOver);

        endGameUI.SetActive(false);
        HomeScreenUI.SetActive(false);
        InGameUI.SetActive(true);
        InGameTime = 0;
        UIGameTime = 0;
    }

    private void GameOver()
    {
        GameController.Instance.SetStage(Stage.UI);
        Invoke("ActivateEndGameUI", 1f);
    }

    private void ActivateEndGameUI()
    {
        endGameText.text = "You have delayed the inevitable for " + InGameTime.ToString("0.00") + " seconds.";
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
            InGameTime += Time.deltaTime;
           // TimerText.text = InGameTime.ToString("0.00");
            TimerTextCPixel.text = InGameTime.ToString("0.00");
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
