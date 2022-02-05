using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;
    public IntValue heartContainers;
    public IntValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    // Update is called once per frame
    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true); // Turning the heart on
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        int fullHeartCount = playerCurrentHealth.RuntimeValue / 4; // There are 4 quarter hearts in each heart container. 
        float fractionHeart = playerCurrentHealth.RuntimeValue % 4;

        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= fullHeartCount - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i > fullHeartCount)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // Fraction of a heart
                if (fractionHeart == 3)
                {
                    hearts[i].sprite = threeQuarterHeart;
                }
                else if (fractionHeart == 2)
                {
                    hearts[i].sprite = halfHeart;
                }
                else if (fractionHeart == 1)
                {
                    hearts[i].sprite = quarterHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }
}
