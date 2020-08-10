using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.current.PlaySFX(1);

            gameObject.SetActive(false);
            ScoreManager.current.AddScore(1);
        }

    }
}
