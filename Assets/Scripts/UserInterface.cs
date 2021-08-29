using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image[] lives;

    public Text scoreText;

    public void DisplayScore(int score = 0)
    {
        scoreText.text = score.ToString();
    }

    public void Awake()
    {
        DisplayScore();
    }

    public void LiveLost()
    {
        int index = 0;

        while(lives[index].enabled == false && index< lives.Length - 1)
        {
            index++;
        }

        lives[index].enabled = false;
    }

    public void RenewLive()
    {
        int index = lives.Length - 1;

        while (lives[index].enabled == true && index > 0 )
        {
            index--;
        }

        lives[index].enabled = true;
    }

    public void RenewAllLives()
    {
        foreach (Image live in lives)
        {
            if (live.enabled == false)
            {
                live.enabled = true;
            }
        }
    }
}