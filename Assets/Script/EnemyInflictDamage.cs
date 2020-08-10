using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInflictDamage : MonoBehaviour
{
    public int damage;
    private PlayerStat currentPlayerStat;
    void Start()
    {
        GameEvent.current.OnSelect += SelectedPlayer;
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }

    void InflictDamage()
    {
        currentPlayerStat.TakeDamage(damage);
    }
    private void SelectedPlayer()
    {
        currentPlayerStat = FindObjectOfType<PlayerStat>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            InflictDamage();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            InflictDamage();
    }
}
