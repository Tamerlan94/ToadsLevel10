using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleDamage : MonoBehaviour
{
    //private ParticleSystem pS;
    private PlayerStat currentPlayerStat;
    private void Start()
    {
        //pS = GetComponent<ParticleSystem>();
        GameEvent.current.OnSelect += SelectedPlayer;
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }

    private void SelectedPlayer()
    {
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayerStat.TakeDamage(1);
        }
    }
}
