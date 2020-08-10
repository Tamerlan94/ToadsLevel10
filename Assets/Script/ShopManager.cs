using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{
    //Текущий номер выбора персонажа
    public int num = 0;
    public Button buy;
    public Button select;

    public Image coinImage;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI currentGold;


    public SaveDataPurchase saveData;
    public ScoreManager scoreManager;

    private void Start()
    {
        //Кнопка покупки отключена изначальна
        buy.gameObject.SetActive(false);
        currentGold.text = scoreManager.score.ToString("000000");
        cost.text = "obtained";
        coinImage.enabled = false;
    }
    //Проверка текущего окна выбора персонажа
    public void ChangeCenter(int page)
    {
        num = page;
        CheckPlayer();
    }
    //Проверка персонажа на купленность
    public void CheckPlayer()
    {   
        if (saveData.playersShopStatus[num].isBought)
        {            
            buy.gameObject.SetActive(false);
            select.interactable = true;
            cost.text = "obtained";
            coinImage.enabled = false;
        }
        else
        {
            if (num == 0)
                buy.gameObject.SetActive(false);
            else
                buy.gameObject.SetActive(true);
            //Проверка хватает ли монет

            select.interactable = false;
            coinImage.enabled = true;

            cost.gameObject.SetActive(true);
            cost.text = saveData.playersShopStatus[num].cost.ToString();
            currentGold.text = scoreManager.score.ToString("000000");
        }

        if (scoreManager.score >= saveData.playersShopStatus[num].cost)
        {
            buy.interactable = true;
        }
        else
        {
            buy.interactable = false;
        }
    }
    public void Buy()
    {
        saveData.playersShopStatus[num].isBought = true;
        buy.gameObject.SetActive(false);
        select.interactable = true;
        scoreManager.score -= saveData.playersShopStatus[num].cost;
        scoreManager.scoreText.text = scoreManager.score.ToString("000000");
        currentGold.text = scoreManager.score.ToString("000000");
        SaveData.current.SaveDataScore();
    }
    public void UpdateScore()
    {
        currentGold.text = scoreManager.score.ToString("000000");
    }
}
