using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text GoldText;
    public Text PlayTimerText;
    public Text PauseTimerText;
    public GameObject HomeScreenUI;
    public GameObject InGameUI;
    public GameObject PauseScreenUI;
    public static UIController Instance;

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
        HomeScreenUI.SetActive(false);
        InGameUI.SetActive(true);
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
                PlayTimerText.text = InGameTime / 100 + "";
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

}
