using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUp : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private PointEffector2D pointEffector;   
    [SerializeField] private GameObject currentPlayer;
    private bool isAttached;

    public DeActive parentDeActive;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        pointEffector = GetComponent<PointEffector2D>();
    }
    private void Start()
    {        
        GameEvent.current.OnSelect += SelectedPlayer;
        currentPlayer = GameManager.current.currentPlayer;
        pointEffector.enabled = false;        
    }    
    private void OnDisable()
    {        
        pointEffector.enabled = false;
        spriteRender.color = new Color(255, 255, 255, 255);
    }
    private void SelectedPlayer()
    {
        currentPlayer = GameManager.current.currentPlayer;
    }
    private void FixedUpdate()
    {
        if (isAttached)
        {            
            transform.position = currentPlayer.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRender.color = new Color(255,255,255,0);
            isAttached = true;
            parentDeActive.enabled = false;
            pointEffector.enabled = true;
            Invoke("UnAttached", 15f);
            currentPlayer.GetComponent<PlayerStat>().Magnet();

            SoundManager.current.PlaySFX(2);
        }
    }
    private void UnAttached()
    {
        isAttached = false;
        parentDeActive.enabled = true;        
        this.gameObject.SetActive(false);
    }
}
