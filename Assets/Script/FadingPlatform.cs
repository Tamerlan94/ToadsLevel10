using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int rand = Random.Range(0, 2);
            float randomX = Random.Range(-1.8f, 1.8f);

            if (rand == 0)
                transform.DOMoveX(randomX, 1f).SetEase(Ease.InOutBounce);
            else if (rand == 1)
                transform.DOMoveX(randomX, 1f).SetEase(Ease.InOutBounce);
        }
    }
   
}