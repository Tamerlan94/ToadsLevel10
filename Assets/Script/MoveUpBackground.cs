using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpBackground : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.deltaTime * MoveUpPlatform.speedBack);
    }
}
