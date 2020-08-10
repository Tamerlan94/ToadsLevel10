using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y > 11f)
        {
            gameObject.SetActive(false);
        }
        if(transform.position.y < -11f)
        {
            gameObject.SetActive(false);
        }
    }
}
