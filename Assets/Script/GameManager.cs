using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public bool isAccel;
    [SerializeField]private int isAccelINT;
    public bool isStart = false;
    public bool endGame = false;
    public int colorNum = 0;

    private int playerNum;
    private float time = 0;
    private float endTime = 60f;
    [Header("Player")]
    public GameObject currentPlayer;
    public GameObject[] players;
    [Header("Managers")]
    public ScoreManager scoreManager;
    public SpawnManager spawnManager;
    public ShopManager shopManager;
    public HealthManagerUI healthManager;
    public ButtonController buttonController;
    [Header("Buttons")]
    public Button selectButton;
    public Toggle accelToggle;
    [Header("Save")]
    public SaveDataPurchase sdPurchase;
    [Header("Other")]
    public Image deathImage;
    [Header("Enemy")]
    public RatController rat;
    [Header("Text")]
    public TextMeshProUGUI timerToLive;

    public static GameManager current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        isAccelINT = PlayerPrefs.GetInt("Accele");
        isAccel = Convert.ToBoolean(isAccelINT);
        accelToggle.isOn = isAccel;
        

        if (PlayerPrefs.HasKey("current"))
        {
            playerNum = PlayerPrefs.GetInt("current");
            currentPlayer = Instantiate(players[playerNum], new Vector3(0f,0f,-1f), Quaternion.identity);
        }
        else
            currentPlayer = Instantiate(players[0], new Vector3(0f, 0f, -1f), Quaternion.identity);

        FindObjectOfType<PlayerStat>().DeathEvent += OnPlayerDeath;
        selectButton.onClick.AddListener(() => { Selected(); });
        
        GameEvent.current.Select();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > endTime)
        {
            ChangeDifficulty();
            time = 0;
        }
    }
    public void OnPlayerDeath()
    {
        SoundManager.current.PlaySFX(0);

        Invoke("DeathTime", 3f);
        endGame = true;
        //FindObjectOfType<PlayerStat>().DeathEvent -= OnPlayerDeath;

        deathImage.gameObject.SetActive(true);
        deathImage.DOFade(0.2f, 0.2f).SetLoops(19, LoopType.Yoyo);

        SaveData.current.SaveDataScore();
    }
    private void ChangeDifficulty()
    {
        float random = UnityEngine.Random.Range(0.2f, 0.8f);
        spawnManager.repeatTime = random;

        colorNum++;
        if(colorNum == 3)
        {
            colorNum = 0;
        }
    }
    private void DeathTime()
    {
        SoundManager.current.StopMusic();

        deathImage.gameObject.SetActive(false);
        Time.timeScale = 0;

        buttonController.LoseGame();
        SoundManager.current.PlaySFX(14);
    }
    public void SecondLife()
    {        
        StartCoroutine("TimerForLive");
        currentPlayer.GetComponent<PlayerStat>().SecondHPSet();
        healthManager.SecondLife();
        //buttonController.ResumeGameLose();        

        rat.ChangeTime();
        rat.transform.position = new Vector3(-2f, 8f, 0f);
    }
    IEnumerator TimerForLive()
    {
        int time = 4;
        while (time > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            time--;
            timerToLive.text = time.ToString();
        }        
        UnDeath();
    }
    private void UnDeath()
    {
        timerToLive.text = "";
        Time.timeScale = 1;
        endGame = false;      
    }
    private void Selected()
    {
        if(currentPlayer != players[shopManager.num])
        {
            Destroy(currentPlayer);
            currentPlayer = Instantiate(players[shopManager.num], new Vector3(0f, 0f, -1f), Quaternion.identity);
            PlayerPrefs.SetInt("current", shopManager.num);
            FindObjectOfType<PlayerStat>().DeathEvent += OnPlayerDeath;
            GameEvent.current.Select();
        }       
       
    }

    public void ChangeAccel(bool a)
    {
        isAccel = a;
        if(isAccel)
            PlayerPrefs.SetInt("Accele", 1);
        else
            PlayerPrefs.SetInt("Accele", 0);
    }

}