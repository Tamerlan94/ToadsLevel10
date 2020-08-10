using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    public GameObject bossPlatform;
    private bool fightBegin = false;

    private void Start()
    {
        GameEvent.current.OnBossStageBegin += BossFightBegin;
        GameEvent.current.OnGroundBoolEnter += OnGroundEnter;
        GameEvent.current.OnGroundBoolExit += OnGroundExit;
    }
    private void FixedUpdate()
    {
        bossPlatform.transform.Translate(Vector2.up * Time.fixedDeltaTime * MoveUpPlatform.speed);
    }
    private void Update()
    {
        
    }
    private void BossFightBegin()
    {
        fightBegin = true;
    }
    private void OnGroundEnter()
    {        
        MoveUpPlatform.speed = 0f;
    }
    private void OnGroundExit()
    {
        MoveUpPlatform.speed = 6f;
    }

}
