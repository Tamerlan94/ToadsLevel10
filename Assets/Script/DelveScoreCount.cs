using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelveScoreCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-13f, -10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -0.01f)
        {
            ScoreManager.current.AddScoreDelve();
        }
        if(transform.position.y > 0)
        {
            transform.position = new Vector3(-13f, -10f, 0f);
        }
    }
}
