using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.fixedDeltaTime * MoveUpPlatform.speed);
    }   
}
