using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleDamageWithEffect : MonoBehaviour
{
    private ParticleSystem pS;
    private PlayerStat currentPlayerStat;
    public GameObject explosionEffect;
    public GameObject parentRocket;

    private float time = 0f;
    private float endTime = .7f;
    private void Start()
    {
        pS = GetComponent<ParticleSystem>();
        GameEvent.current.OnSelect += SelectedPlayer;
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > endTime)
        {
            if (parentRocket.transform.position.x >= -2.6f && parentRocket.transform.position.x <= 2.6f)
            {
                int random = Random.Range(8, 10);
                SoundManager.current.PlaySFX(random);
            }
               
            time = 0;
        }
    }
    private void SelectedPlayer()
    {
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.current.PlaySFX(10);
            Instantiate(explosionEffect, new Vector3(GameManager.current.currentPlayer.transform.position.x, GameManager.current.currentPlayer.transform.position.y + 0.6f, -1.1f), GameManager.current.currentPlayer.transform.rotation);
            currentPlayerStat.TakeDamage(1);            
        }
    }
}
