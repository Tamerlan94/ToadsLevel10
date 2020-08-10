using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{    
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(1.42f, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}
