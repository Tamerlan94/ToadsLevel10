using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroying : MonoBehaviour
{
    public float time;
    private void FixedUpdate()
    {
        Destroy(gameObject, time);
    }
}
