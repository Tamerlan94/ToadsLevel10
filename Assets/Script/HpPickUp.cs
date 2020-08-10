using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player") && currentPlayer.GetComponent<PlayerStat>().life < 6)
        {
            SoundManager.current.PlaySFX(4);

            this.gameObject.SetActive(false);
            currentPlayer.GetComponent<PlayerStat>().AddHealth();
        }
    }
}
