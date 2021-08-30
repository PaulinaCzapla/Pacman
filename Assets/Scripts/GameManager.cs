using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    public FruitsManager fruitsManager;

    public int ghostMultiplayer = 1;

    public int score { get; private set; }

    public int lives { get; private set; }

    public UserInterface ui;

    public Canvas canvasGameOver;

    public Text scoreGameOver;

    public Text gameOverText;

    private FadeIn fadeIn;

    private void Start()
    {
        fadeIn = canvasGameOver.gameObject.GetComponent<FadeIn>();
        canvasGameOver.gameObject.SetActive(false);

        NewGame();
    }

    private IEnumerator StartNewGame()
    {
        yield return new WaitForSeconds(3f);

        while (!canvasGameOver.gameObject.activeSelf || !Input.anyKey)
        {
            yield return null;
        }
        canvasGameOver.gameObject.SetActive(false);
        NewGame();
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        ui.RenewAllLives();
        fruitsManager.gameObject.SetActive(true);
        StopAllCoroutines();
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        fruitsManager.gameObject.SetActive(false);
        pacman.gameObject.SetActive(false);

        canvasGameOver.gameObject.SetActive(true);
        fadeIn.Restart();

        StartCoroutine(StartNewGame());
    }
    private void SetScore(int score)
    {
        this.score = score;
        ui.DisplayScore(this.score);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + (ghost.points * ghostMultiplayer));
        ghostMultiplayer++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(lives - 1);
        ui.LiveLost();

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            gameOverText.text = "GAME OVER";
            scoreGameOver.text = score.ToString();
            GameOver();
        }
    }

    public void FruitEaten(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
        SetScore(score + fruit.points);
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + pellet.points);

        if (!HasReminingPellets())
        {
            pacman.gameObject.SetActive(false);
            gameOverText.text = "YOU WON";
            scoreGameOver.text = score.ToString();
            GameOver();
        }
    }

    public void BonusPelletEaten(BonusPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].scared.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplayer), pellet.duration);
    }

    private bool HasReminingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplayer()
    {
        ghostMultiplayer = 1;
    }
}
