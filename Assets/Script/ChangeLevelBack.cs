using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelBack : MonoBehaviour
{
    public Color[] colors;
    private SpriteRenderer spriteRen;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameManager.current.colorNum == 0)
        {
            ChangeColor(0);
        }
        else if (GameManager.current.colorNum == 1)
        {
            ChangeColor(1);
        }
        else if (GameManager.current.colorNum == 2)
        {
            ChangeColor(2);
        }
    }
    private void ChangeColor(int num)
    {
        spriteRen.color = colors[num];
    }

}
