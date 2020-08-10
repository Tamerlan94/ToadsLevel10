using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetect : MonoBehaviour
{
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("MovingP"))
    //    {
    //        this.transform.parent = collision.transform;

    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("MovingP"))
    //    {
    //        this.transform.parent = null;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovingP"))
        {
            this.transform.parent = collision.transform;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovingP"))
        {
            this.transform.parent = null;
        }
    }
}
