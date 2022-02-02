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
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

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
        float tempHealth = playerCurrentHealth.initialValue / 4;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i > tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // Fraction of a heart
                if (tempHealth % 1 == 0.75)
                {
                    hearts[i].sprite = threeQuarterHeart;
                }
                else if (tempHealth % 1 == 0.5)
                {
                    hearts[i].sprite = halfHeart;
                }
                else if (tempHealth % 1 == 0.25)
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
