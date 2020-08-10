using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveDataPurchase : MonoBehaviour
{
    //Ссылка на массив персонажей для проверки и установки значения "купленности"
    public PlayerShopStatus[] playersShopStatus;
    
    [SerializeField] private Purchase purchase = new Purchase();
    private string path;   
    private void Start()
    {
        playersShopStatus =  new PlayerShopStatus[GameManager.current.players.Length];
        for (int i = 0; i < GameManager.current.players.Length; i++)
        {
            playersShopStatus[i] = GameManager.current.players[i].GetComponent<PlayerShopStatus>();
        }
        purchase.isBought = new bool[playersShopStatus.Length];

        #region Save
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "SaveDataPurchase.json");
#else
        path = Path.Combine(Application.dataPath, "SaveDataPurchase.json");
#endif
        if (File.Exists(path))
        {
            purchase = JsonUtility.FromJson<Purchase>(File.ReadAllText(path));

            //if(playersShopStatus.Length > purchase.isBought.Length)
            //{
               
            //}

            for (int i = 0; i < playersShopStatus.Length; i++)
            {
                playersShopStatus[i].isBought = purchase.isBought[i];
            }
        }
        #endregion
    }

    public void SaveDataPur()
    {
        for(int i = 0; i < playersShopStatus.Length; i++)
        {
            purchase.isBought[i] = playersShopStatus[i].isBought;
        }
        File.WriteAllText(path, JsonUtility.ToJson(purchase));
    }

    [System.Serializable]
    public class Purchase
    {       
        public bool[] isBought;
    }
}
