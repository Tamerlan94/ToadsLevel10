using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    //private static LevelLoader _instance;

    //private void Awake()
    //{
    //    if (_instance == null)
    //    {

    //        _instance = this;
    //        DontDestroyOnLoad(this.gameObject);

    //        //Rest of your Awake code

    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}

    public void LoadLevel()
    {
        StartCoroutine("LoadLevelI");
    }
    IEnumerator LoadLevelI()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(0);
    }
}
