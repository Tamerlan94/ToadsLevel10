using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewZoneMovement : MonoBehaviour
{
    private void OnEnable()
    {
        float rand = Random.Range(-2f, 2f);
        transform.DORestart();
        if (transform.position.x > 0f)
        {
            transform.DOMoveX(rand, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        }
        else
        {
            transform.DOMoveX(rand, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutExpo);
        }
    }
    private void OnDisable()
    {
        transform.DOPause();
    }
}
