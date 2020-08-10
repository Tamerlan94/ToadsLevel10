using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;
    private void Awake()
    {
        current = this;
    }
    //изменение когда персонаж на земле
    public event Action OnGroundBoolEnter;
    public void GroundEnter()
    {
        OnGroundBoolEnter?.Invoke();
    }

    public event Action OnGroundBoolExit;
    public void GroundExit()
    {
        OnGroundBoolExit?.Invoke();
    }
    
    //////////////////////
    

    //изменение когда запускается 2 зона уровня
    //EndZoneMovement.cs
    public event Action OnTimeZoneBegin;
    public void ZoneBegin()
    {
        OnTimeZoneBegin?.Invoke();
    }

    public event Action<int, int> OnTakenDamage;
    public void TakeDamage(int damage, int global_damage)
    {
        OnTakenDamage?.Invoke(damage, global_damage);
    }

    ///////////////////////////
    public event Action OnSelect;
    public void Select()
    {
        OnSelect?.Invoke();
    }

    public event Action OnBossStageBegin;
    public void BossBegin()
    {
        OnBossStageBegin?.Invoke();
    }
}
