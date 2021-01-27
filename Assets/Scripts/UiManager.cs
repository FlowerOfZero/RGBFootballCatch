using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public int HighScore;
    public int currentScore;
    public int currentSpeed;
    public int ballsCaught;
    int scoreToAdd = 1;
    public TMP_Text ScoreText, HighScoreText, menuText, speedText;
    public GameObject startMenu;
    BallShooter BS;
    public ParticleSystem ballCaughtParticles;
    public Slider sprintSlider;
    public Movement playerMovement;

    public void Awake()
    {
        BS = FindObjectOfType<BallShooter>();
        BS.timeTillNextBall = 999999999;
        Score score = new Score();
        score.loadScore();
        HighScore = score.HighScore;
        HighScoreText.text = "High Score:" + HighScore.ToString();

        playerMovement = FindObjectOfType<Movement>();
        sprintSlider.maxValue = 2;
        sprintSlider.value = 2;
    }

    public void Update()
    {
        sprintSlider.value = playerMovement.sprintTimer;
        
    }

    public void playClicked()
    {
        scoreToAdd = 1;
        currentScore = 0;
        BS.timeTillNextBall = 3;
        startMenu.SetActive(false);
        ScoreText.text = "Score:" + currentScore.ToString();
        speedText.text = "Speed Level:" + scoreToAdd.ToString();
    }

    public void quitClicked()
    {
        Application.Quit();
    }

    public void ballDropped()
    {
        //destroy all current balls in the scene, so the player cant earn points after they drop a ball
        BallCollisionScript[] balls = FindObjectsOfType<BallCollisionScript>();
        foreach(BallCollisionScript b in balls)
        {
            Destroy(b.gameObject);
        }


        BS.timeTillNextBall = 999999999;
        startMenu.SetActive(true);
        if(currentScore > HighScore)
        {
            HighScoreText.text = "New High Score:" + currentScore;
            HighScore = currentScore;
            Score score = new Score();
            score.HighScore = currentScore;
            score.saveScore(currentScore);
        }
        else
        {
            HighScoreText.text = "High Score:" + HighScore.ToString();
        }

        menuText.text = "Game Over!";
    }

    public void updateScore()
    {
        ballsCaught++;
        if(ballsCaught /5 == 1)
        {
            scoreToAdd++;

            BS.timeTillNextBall = (BS.timeTillNextBall/100) * 90;
            speedText.text = "Speed Level:" + scoreToAdd.ToString();
            ballsCaught = 0;
        }
        currentScore += scoreToAdd;
        ScoreText.text = "Score:" + currentScore.ToString();

        ballCaughtParticles.Play();
    }
}
