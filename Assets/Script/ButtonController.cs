using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{ 
    [Header("Panels")]
    public RectTransform startPanel;
    public RectTransform shopPanel;
    public RectTransform pausePanel;
    public RectTransform losePanel;
    public RectTransform hp_bar;
    public RectTransform watchPanel;
    public RectTransform faqPanel;
    [Header("Buttons")]
    public RectTransform adsButton;
    public RectTransform pauseButton;
    public Image joystic;
    public Image jumpButton;
    public Image handle;    
    [Header("Text")]
    public RectTransform coinHomes;
    public CanvasGroup canvasTextGame; 


    private bool isFAQ;
   

    private void Start()
    {
        startPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);

        adsButton.DOAnchorPos(new Vector2(-75f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        

        coinHomes.DOAnchorPos(new Vector2(0f, -50f), 1.5f).SetUpdate(UpdateType.Normal, true);

        joystic.DOFade(0f, 0.1f);
        jumpButton.DOFade(0f, 0.1f);
        handle.DOFade(0f, 0.1f);

        canvasTextGame.DOFade(1f, 1.5f);
    }
    public void StartGame()
    {
        startPanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);
        hp_bar.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);

        pauseButton.DOAnchorPos(new Vector2(440f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        adsButton.DOAnchorPos(new Vector2(450f,0f),1.5f).SetUpdate(UpdateType.Normal, true);        

        coinHomes.DOAnchorPos(new Vector2(0f, 115f), 1.5f).SetUpdate(UpdateType.Normal, true);

        
        jumpButton.DOFade(0.5f, 1.5f);

        if (GameManager.current.isAccel)
        {
            handle.gameObject.SetActive(false);
            joystic.gameObject.SetActive(false);
        }
        else
        {
            handle.gameObject.SetActive(true);
            joystic.gameObject.SetActive(true);
            joystic.DOFade(0.5f, 1.5f);
            handle.DOFade(0.5f, 1.5f);
        }     

        canvasTextGame.DOFade(0f, 1.5f);

        GameManager.current.isStart = true;
    }
    public void PauseGame()
    {        
        pausePanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);

        pauseButton.DOAnchorPos(new Vector2(440f, 2886f), 1.5f).SetUpdate(UpdateType.Normal, true);

        Time.timeScale = 0;
        SaveData.current.SaveDataScore();
    }
    public void ResumeGame()
    {
        pausePanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);

        pauseButton.DOAnchorPos(new Vector2(440f, 0f), .5f).SetUpdate(UpdateType.Normal, true);

        Time.timeScale = 1;
    }
    public void ShopGame()
    {
        startPanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);
        shopPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);

        adsButton.DOAnchorPos(new Vector2(450f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        

        coinHomes.DOAnchorPos(new Vector2(0f, 115f), 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void Button_backFromShop()
    {
        startPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);
        shopPanel.DOAnchorPos(new Vector2(-1080f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);

        adsButton.DOAnchorPos(new Vector2(-75f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
       

        coinHomes.DOAnchorPos(new Vector2(0f, -50f), 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void LoseGame()
    {
        losePanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void ResumeGameLose()
    {
        losePanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void MainGame()
    {
        //SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        FindObjectOfType<LevelLoader>().LoadLevel();
        Time.timeScale = 1;
    }
    public void AD_watch()
    {
        watchPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);
        losePanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void AD_questionPos()
    {
        watchPanel.DOAnchorPos(new Vector2(-1080f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void AD_questionNeg()
    {
        watchPanel.DOAnchorPos(new Vector2(-1080f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        losePanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);
    }
    public void FAQButton()
    {
        isFAQ = !isFAQ;
        if (isFAQ)
        {
            startPanel.DOAnchorPos(new Vector2(0f, -2160f), 1.5f).SetUpdate(UpdateType.Normal, true);
            faqPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);

            adsButton.DOAnchorPos(new Vector2(450f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        }
        else
        {
            startPanel.DOAnchorPos(Vector2.zero, 1.5f).SetUpdate(UpdateType.Normal, true);
            faqPanel.DOAnchorPos(new Vector2(-1080f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);

            adsButton.DOAnchorPos(new Vector2(-75f, 0f), 1.5f).SetUpdate(UpdateType.Normal, true);
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
