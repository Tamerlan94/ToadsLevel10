using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RatController : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isAlready = false;
    [SerializeField] private float time = 0f;
    [SerializeField] private float endTime = 30f;

    public GameObject hurryUp;
    private Image hurryUp_image;
    public Sprite[] hurryUp_sprite;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        GameEvent.current.OnTimeZoneBegin += OnChangeZone;

        hurryUp_image = hurryUp.GetComponentInChildren<Image>();
    }    
    private void ChangeBodyType(bool a)
    {
        if (a)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }    
    private void OnChangeZone()
    {
        Invoke("AfterSeconds", 5f);
    }
    private void AfterSeconds()
    {        
        isAlready = !isAlready;
        if (isAlready)
        {
            ChangeBodyType(true);
            transform.position = new Vector3(-2f, 8f, 0f);
        }
        else
        {
            ChangeBodyType(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LINEforRAT"))
        {
            time += Time.fixedDeltaTime;           
            hurryUp.SetActive(true);

            if (time > endTime / 2)
                hurryUp_image.sprite = hurryUp_sprite[1];
            else
                hurryUp_image.sprite = hurryUp_sprite[0];


            if (time > endTime)
            {
                GameManager.current.OnPlayerDeath();
                time = 0;               
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LINEforRAT"))
        {
            hurryUp.SetActive(false);
        }
     
    }
    public void ChangeTime()
    {
        time = 0;
        hurryUp.SetActive(false);
    }
}
