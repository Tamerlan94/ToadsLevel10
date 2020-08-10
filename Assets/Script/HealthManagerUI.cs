using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagerUI: MonoBehaviour
{
    [Header("Small HP")]
    public Image[] imagesSmallHP;
    public Sprite empty_hp;
    public Sprite full_hp;
    public int current_hp_small;
    [Header("Big HP")]
    public Image[] imagesBigHP;
    public Sprite empty_hp_big;
    public Sprite full_hp_big;
    public int current_hp_big;
    void Start()
    {
        GameEvent.current.OnTakenDamage += TakeDamageDisplay;
        current_hp_small = imagesSmallHP.Length;        
        GameEvent.current.OnSelect += ChangeCurrentBigHP;

        current_hp_big = FindObjectOfType<PlayerStat>().globalLife;
        for (int i = 0; i < current_hp_big; i++)
        {
            imagesBigHP[i].sprite = full_hp_big;
        }
    }

    public void TakeDamageDisplay(int damage, int global_damage)
    {
        if(damage == 1)
        {
            current_hp_small -= damage;
            if (current_hp_small <= 0)
            {
                current_hp_big--;
                ChangeSpriteBig(imagesBigHP[current_hp_big]);
                if (current_hp_big > 0)
                {
                    for (int i = 0; i < imagesSmallHP.Length; i++)
                    {
                        imagesSmallHP[i].sprite = full_hp;
                    }
                    current_hp_small = imagesSmallHP.Length;
                }
                else
                {
                    ChangeSprite(imagesSmallHP[current_hp_small]);
                }
            }
            else
                ChangeSprite(imagesSmallHP[current_hp_small]);
        }
        else if(damage == 2)
        {
            current_hp_small -= damage;
            if (current_hp_small <= 0)
            {
                current_hp_big--;
                ChangeSpriteBig(imagesBigHP[current_hp_big]);
                if (current_hp_big > 0)
                {
                    for (int i = 0; i < imagesSmallHP.Length; i++)
                    {
                        imagesSmallHP[i].sprite = full_hp;
                    }
                    current_hp_small = imagesSmallHP.Length;
                }
                else
                {
                    ChangeSprite(imagesSmallHP[current_hp_small], imagesSmallHP[current_hp_small + 1]);
                }
            }
            else
                ChangeSprite(imagesSmallHP[current_hp_small],imagesSmallHP[current_hp_small + 1]);
        }
        
    }

    private void ChangeSprite(Image image)
    {
        image.sprite = empty_hp;
    }
    private void ChangeSprite(Image image, Image image2)
    {        
        image.sprite = empty_hp;
        image2.sprite = empty_hp;
    }
    private void ChangeSpriteBig(Image image)
    {
        image.sprite = empty_hp_big;
    }
    private void ChangeCurrentBigHP()
    {
        for (int i = 0; i < imagesBigHP.Length; i++)
        {
            imagesBigHP[i].sprite = empty_hp_big;
        }

        current_hp_big = FindObjectOfType<PlayerStat>().globalLife;
        for(int i = 0; i < current_hp_big; i++)
        {
            imagesBigHP[i].sprite = full_hp_big;
        }
    }
    public void SecondLife()
    {            
        for (int i = 0; i < imagesBigHP.Length; i++)
        {
            imagesBigHP[i].sprite = empty_hp_big;
        }

        current_hp_big = FindObjectOfType<PlayerStat>().globalLife;
        for (int i = 0; i < current_hp_big; i++)
        {
            imagesBigHP[i].sprite = full_hp_big;
        }

        current_hp_small = FindObjectOfType<PlayerStat>().life;
        for (int i = 0; i < current_hp_small; i++)
        {
            imagesSmallHP[i].sprite = full_hp;
        }
    }
    public void AddLife()
    {
        current_hp_small++;
       // imagesSmallHP[current_hp_small].sprite = full_hp;
        for(int i = 0; i < current_hp_small; i++)
        {
            imagesSmallHP[i].sprite = full_hp;
        }
    }
}
