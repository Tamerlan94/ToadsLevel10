using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalPickUp : MonoBehaviour
{
    private GameObject currentPlayer;
    private void Start()
    {        
        GameEvent.current.OnSelect += SelectedPlayer;
        currentPlayer = GameManager.current.currentPlayer;

    }
    private void SelectedPlayer()
    {
        currentPlayer = GameManager.current.currentPlayer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !currentPlayer.GetComponent<PlayerStat>().unDamage)
        {
            SoundManager.current.PlaySFX(3);

            this.gameObject.SetActive(false);
            currentPlayer.GetComponent<PlayerStat>().Immortal();
        }
    }
}
