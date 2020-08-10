using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public ScoreManager scoreManager;

    private ScoreSave scoreSave = new ScoreSave();
    private string path;


    public static SaveData current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "SaveData.json");
#else
        path = Path.Combine(Application.dataPath, "SaveData.json");
#endif
        if (File.Exists(path))
        {
            scoreSave = JsonUtility.FromJson<ScoreSave>(File.ReadAllText(path));
            Debug.Log("Score: " + scoreSave.scoreValue + " Delve: " + scoreSave.scoreDelve);
        }

        scoreManager.score = scoreSave.scoreValue;
        scoreManager.delveScore = scoreSave.scoreDelve;
    }

    public void SaveDataScore()
    {
        scoreSave.scoreValue = scoreManager.score;
        scoreSave.scoreDelve = scoreManager.delveScore;

        File.WriteAllText(path, JsonUtility.ToJson(scoreSave));
    }
    //[SerializeField] private ScoreData _scoreData = new ScoreData();
    //public void SaveIntoJson()
    //{
    //    string scoreValue = JsonUtility.ToJson(_scoreData);
    //}

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            File.WriteAllText(path, JsonUtility.ToJson(scoreSave));
    }
    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(scoreSave));
    }

}
[System.Serializable]
public class ScoreSave
{    
    public int scoreValue;
    public int scoreDelve;
}
