using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeLevel : MonoBehaviour
{
    public Sprite[] sprites;
    
    private SpriteRenderer spriteRen;
    // Start is called before the first frame update
    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.current.colorNum == 0)
        {
            ChangeSprite(0);
        }
        else if(GameManager.current.colorNum == 1)
        {
            ChangeSprite(1);
        }
        else if (GameManager.current.colorNum == 2)
        {
            ChangeSprite(2);
        }
    }
    
    private void ChangeSprite(int num)
    {
        spriteRen.sprite = sprites[num];
    } 
}
