using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    List<GameObject> Enemys;
    Stage stage;
    [SerializeField]
    private float spawnRateMin;
    [SerializeField]
    private float spawnRateMax;

    [System.Serializable] public class StartGameEvent : UnityEvent { }
    StartGameEvent startGameEvent = new StartGameEvent();
    [System.Serializable] public class ResetGameEvent : UnityEvent { }
    ResetGameEvent resetGameEvent = new ResetGameEvent();
    [System.Serializable] public class PauseGameEvent : UnityEvent { }
    PauseGameEvent pauseGameEvent = new PauseGameEvent();
    [System.Serializable] public class ResumeGameEvent : UnityEvent { }
    ResumeGameEvent resumeGameEvent = new ResumeGameEvent();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        stage = Stage.UI;
        Enemys = new List<GameObject>();
        spawnRateMin = 2;
        spawnRateMax = 4;

        StartCoroutine(SpawnEnemy());

        startGameEvent.AddListener(AIStart);
        startGameEvent.AddListener(UIController.Instance.GameStart);

        pauseGameEvent.AddListener(AIPause);
        pauseGameEvent.AddListener(UIGamePause);

        resumeGameEvent.AddListener(ResumeGame);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (stage == Stage.Play)
            {
                yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
                Debug.Log("EnemySpawn");
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (startGameEvent != null)
                pauseGameEvent.Invoke();
        }
    }

    private void GameReset()
    {
        stage = Stage.UI;
        UIController.Instance.HomeScreenUI.SetActive(true);
        UIController.Instance.InGameUI.SetActive(false);
    }
    private void UIGamePause()
    {
        stage = Stage.UI;
        UIController.Instance.PauseScreenUI.SetActive(true);
        UIController.Instance.InGameUI.SetActive(true);
    }
    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }

    public Stage GetStage()
    {
        return stage;
    }

    private void AIStart()
    {
        var Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemys.Length; i++)
        {
            Enemys[i].GetComponent<Enemy>().EnemyAI(true);
        }
    }

    private void AIPause()
    {
        var Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemys.Length; i++)
        {
            Enemys[i].GetComponent<Enemy>().EnemyAI(false);
        }
    }

    public void StartGameButton()
    {
        if (startGameEvent != null)
            startGameEvent.Invoke();
    }

    private void ResumeGame()
    {
        stage = Stage.Play;
        UIController.Instance.PauseScreenUI.SetActive(false);
        AIStart();
    }
    public void ResumeGameButon()
    {
        if (startGameEvent != null)
            resumeGameEvent.Invoke();
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Utku");
    }
}
