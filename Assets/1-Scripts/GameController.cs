using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int archerWaveNumber;
    public int[] numberOfSwordEnemiesInWave;
    public int[] numberOfArcherEnemiesInWave;
    public float delayBetweenWaves;

    public List<Transform> spawnPoints;
    public List<GameObject> EnemyArray;

    List<GameObject> Enemys;
    private int waveNumber;
    private float waveTimer;


    Stage stage;
    [SerializeField]
    private float spawnRateMin;
    [SerializeField]
    private float spawnRateMax;

    [SerializeField]
    private float ArenaVerticalDis;
    [SerializeField]
    private float ArenaHortizonalDis;

    [System.Serializable] public class StartGameEvent : UnityEvent { }
    public StartGameEvent startGameEvent = new StartGameEvent();
    [System.Serializable] public class ResetGameEvent : UnityEvent { }
    public ResetGameEvent resetGameEvent = new ResetGameEvent();
    [System.Serializable] public class PauseGameEvent : UnityEvent { }
    public PauseGameEvent pauseGameEvent = new PauseGameEvent();
    [System.Serializable] public class ResumeGameEvent : UnityEvent { }
    public ResumeGameEvent resumeGameEvent = new ResumeGameEvent();
    [System.Serializable] public class GameOverEvent : UnityEvent { }
    public GameOverEvent gameOverEvent = new GameOverEvent();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        stage = Stage.UI;
        Enemys = new List<GameObject>();

        startGameEvent.AddListener(AIStart);
        startGameEvent.AddListener(UIController.Instance.GameStart);

        pauseGameEvent.AddListener(UIGamePause);

        resumeGameEvent.AddListener(ResumeGame);


    }

    IEnumerator SpawnEnemyWave()
    {
            if (stage == Stage.Play)
            {
#if UNITY_EDITOR
                Debug.Log("EnemySpawn");
#endif           
                int spawnPointRoot = Random.Range(0, spawnPoints.Count);
                for (int i =0;i < numberOfSwordEnemiesInWave[waveNumber ]; i++)
                {
                        var EnemyTmp = Instantiate(EnemyArray[0], spawnPoints[spawnPointRoot].position, Quaternion.identity);
                        EnemyTmp.GetComponent<Enemy>().Target = GameObject.FindGameObjectWithTag("Player").transform;
                        EnemyTmp.GetComponent<Enemy>().EnemyAI(true);
                    spawnPointRoot = (spawnPointRoot + Random.Range(1,3)) % spawnPoints.Count;
                }
                spawnPointRoot = (spawnPointRoot + Random.Range(1, 3)) % spawnPoints.Count;
                for (int i = 0; i < numberOfArcherEnemiesInWave[waveNumber]; i++)
                {
                    var EnemyTmp = Instantiate(EnemyArray[1], spawnPoints[spawnPointRoot].position, Quaternion.identity);
                    EnemyTmp.GetComponent<EnemyMovementArcher>().Target = GameObject.FindGameObjectWithTag("Player").transform;
                    EnemyTmp.GetComponent<EnemyMovementArcher>().EnemyAI(true);
                    EnemyTmp.GetComponent<EnemyAimAndFire>().GameStarted();
                    spawnPointRoot = (spawnPointRoot + Random.Range(1, 3)) % spawnPoints.Count;
                }

                waveNumber++;

                if(waveNumber >= numberOfArcherEnemiesInWave.Length)
                {
                    waveNumber = numberOfArcherEnemiesInWave.Length - 1;
                }
            }

        yield break;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameEvent != null)
            {
                pauseGameEvent.Invoke();
            }
           
        }

        waveTimer += Time.deltaTime;
        if(waveTimer > delayBetweenWaves && stage == Stage.Play)
        {
            waveTimer = 0;
            StartCoroutine(SpawnEnemyWave());
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
        var Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemys.Length; i++)
        {
            Enemys[i].GetComponent<Enemy>().EnemyAI(false);
        }
        stage = Stage.UI;
        UIController.Instance.PauseScreenUI.SetActive(true);
        UIController.Instance.InGameUI.SetActive(true);

        Invoke("StopTime", 0.1f);
    }

    private void StopTime()
    {
        //waveTimer = 0;
        Time.timeScale = 0.0f;
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
        stage = Stage.Play;
        //StartCoroutine(SpawnEnemyWave());
        Time.timeScale = 1f;
        var Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemys.Length; i++)
        {
            if(Enemys[i].GetComponent<Enemy>() != null)
            {
                Enemys[i].GetComponent<Enemy>().EnemyAI(true);
            }
            else if(Enemys[i].GetComponent<EnemyMovementArcher>() != null)
            {
                Enemys[i].GetComponent<EnemyMovementArcher>().EnemyAI(true);
            }
        }
    }

    

    public void StartGameButton()
    {
            if(!PlayerPrefs.HasKey("CutScenePlayed"))
        {
            int cutScenePlayed = PlayerPrefs.GetInt("CutScenePlayed");
            if(cutScenePlayed == 0)
            {
                PlayerPrefs.SetInt("CutScenePlayed", 1);
                PlayerPrefs.Save();
                UIController.Instance.ActivateCutscene();
            }
            else
            {
                if (startGameEvent != null)
                    startGameEvent.Invoke();
            }
        }
            else
        {
            PlayerPrefs.SetInt("CutScenePlayed", 1);
            PlayerPrefs.Save();
                UIController.Instance.ActivateCutscene();
        }



    }

    private void ResumeGame()
    {
        stage = Stage.Play;
        UIController.Instance.PauseScreenUI.SetActive(false);
        AIStart();
        Time.timeScale = 1f;
    }
    public void ResumeGameButon()
    {
        if (resumeGameEvent != null)
            resumeGameEvent.Invoke();
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("FinalScene");
        
    }
}
