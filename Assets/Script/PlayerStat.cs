using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStat : MonoBehaviour
{
    public int globalLife;
    public int life = 6;    
    public bool unDamage = false;

    private int _globalLife;
    private int _life;

    [Header("Effect")]
    public GameObject immortalEffect;
    public GameObject magnetEffect;

    //Пример делегата
    public delegate void DeathDelegate();
    public event DeathDelegate DeathEvent;
    //

    private SpriteRenderer sRenderer;
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        _globalLife = globalLife;
        _life = life;

        magnetEffect.SetActive(false);
        immortalEffect.SetActive(false);
    }
   
    public void TakeDamage(int damage, int global_damage = 0)
    {        
        if (!unDamage && !GameManager.current.endGame)
        {
            SoundManager.current.PlaySFX(6);

            life -= damage;
            UnDamageEffect();
            unDamage = true;
            Invoke("OffUnDamage", 1.8f);
            if (life <= 0)
            {
                globalLife--;
                life = 6;
            }
            if (!GameManager.current.endGame)
                GameEvent.current.TakeDamage(damage, global_damage);
            if (globalLife <= 0 && !GameManager.current.endGame)
            {                
                GameOver();
            }
                     
        }
        globalLife -= global_damage;
    }
    private void UnDamageEffect()
    {
        sRenderer.DOFade(0.3f, 0.1f).SetLoops(16, LoopType.Yoyo).SetEase(Ease.InOutBounce);
    }
    private void OffUnDamage()
    {
        unDamage = false;
    }

    void GameOver()
    {        
        DeathEvent?.Invoke();
    }
    public void SecondHPSet()
    {
        UnDamageEffect();
        unDamage = true;
        globalLife = _globalLife;
        life = _life;        
        Invoke("OffUnDamage", 3f);
    }

    //booster
    public void Immortal()
    {
        unDamage = true;
        sRenderer.DOFade(0.3f, 0.1f).SetLoops(100, LoopType.Yoyo).SetEase(Ease.InOutBounce);
        immortalEffect.SetActive(true);
        StartCoroutine(ImmortalStop());
    }
    IEnumerator ImmortalStop()
    {
        yield return new WaitForSecondsRealtime(10);
        immortalEffect.SetActive(false);
        unDamage = false;
    }
    public void AddHealth()
    {
        if (life != 6)
        {
            life++;
            FindObjectOfType<HealthManagerUI>().AddLife();
        }
            
    }
    public void Magnet()
    {
        magnetEffect.SetActive(true);
        StartCoroutine(MagnetStop());
    }
    IEnumerator MagnetStop()
    {
        yield return new WaitForSecondsRealtime(15);
        magnetEffect.SetActive(false);
    }
}
