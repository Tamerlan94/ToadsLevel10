using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndZoneMovement : MonoBehaviour
{    
    bool newZoneBegin_end;
    public GameObject side;
    public GameObject zipZapParent;
    public ParticleSystem zipZapParticle;
    private GameObject target;
    bool attack;
    public float speed;    

    private void Awake()
    {
        zipZapParticle.Stop();
    }
    private void Start()
    {
        target = GameManager.current.currentPlayer;
        GameEvent.current.OnTimeZoneBegin += OnTimeZoneBegin;
        GameEvent.current.OnSelect += SelectedPlayer;              
    }
    private void SelectedPlayer()
    {
        target = GameManager.current.currentPlayer;
    }
    private void FixedUpdate()
    {
        if (!attack && newZoneBegin_end)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, 0f), new Vector2(target.transform.position.x, 0f), speed * Time.fixedDeltaTime);          
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
       
        if(transform.position.x == target.transform.position.x && !attack && newZoneBegin_end)
        {
            attack = true;

            zipZapParticle.Play();
            StartCoroutine(StopAttacking());            
            SoundManager.current.PlaySFX(11);
        }
    }
    IEnumerator StopAttacking()
    {
        yield return new WaitForSecondsRealtime(5f);
        attack = false;
        zipZapParticle.Stop();
    }
    private void OnTimeZoneBegin()
    {
        newZoneBegin_end = !newZoneBegin_end;
        if (newZoneBegin_end)
        {
            side.transform.DOLocalMoveY(-4.8f, 1f).SetEase(Ease.Linear);
            SoundManager.current.PlaySFX(12, true);
        }
        else
        {
            side.transform.DOLocalMoveY(-8f, 1f).SetEase(Ease.Linear);
            zipZapParticle.Stop();
            SoundManager.current.StopSFX();
        }
        
    }    

    
}
