using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int score;
    public static int highscore1 = 0;
    public static int highscore2 = 0;
    public static int highscore3 = 0;
    public static int highscore4 = 0;
    public static int highscore5 = 0;
    public int highscoreTemp = 0;
    private PlayerController playerController;
    private AudioSource ControllerAudio;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText1;
    public TextMeshProUGUI highscoreText2;
    public TextMeshProUGUI highscoreText3;
    public TextMeshProUGUI highscoreText4;
    public TextMeshProUGUI highscoreText5;
    public GameObject Buttons;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        ControllerAudio = GetComponent<AudioSource>();
        scoreText.text = "Score: " + score;
        UpdateScore(0);
        score = PlayerPrefs.GetInt("score", 0);
        score = 0;
        highscore1 = PlayerPrefs.GetInt("highscore1", 0);
        highscore2 = PlayerPrefs.GetInt("highscore2", 0);
        highscore3 = PlayerPrefs.GetInt("highscore3", 0);
        highscore4 = PlayerPrefs.GetInt("highscore4", 0);
        highscore5 = PlayerPrefs.GetInt("highscore5", 0);
        Buttons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isGameOver == true)
        {
            Buttons.SetActive(true);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void Highscores()
    {
        score = highscoreTemp;

        if (score > highscore1)
        {
            highscore1 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore1", highscore1);
            PlayerPrefs.SetInt("highscore1", highscore1);
            score = highscoreTemp;
        }

        if (score > highscore2)
        {
            highscore2 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore2", highscore2);
            PlayerPrefs.SetInt("highscore2", highscore2);
            score = highscoreTemp;
        }

        if (score > highscore3)
        {
            highscore3 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore3", highscore3);
            PlayerPrefs.SetInt("highscore3", highscore3);
            score = highscoreTemp;
        }

        if (score > highscore4)
        {
            highscore4 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore4", highscore4);
            PlayerPrefs.SetInt("highscore4", highscore4);
            score = highscoreTemp;
        }

        if (score > highscore5)
        {
            highscore5 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore5", highscore5);
            PlayerPrefs.SetInt("highscore5", highscore5);
            score = highscoreTemp;
        }

        highscoreText1.text = "1. " + highscore1;
        highscoreText2.text = "2. " + highscore2;
        highscoreText3.text = "3. " + highscore3;
        highscoreText4.text = "4. " + highscore4;
        highscoreText5.text = "5. " + highscore5;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(1);
    }

    public void HighscoreButton()
    {
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene(2);
    }

    public void QuitButton()
    {
        ControllerAudio.PlayOneShot(clickSound, 1.0f);
        Application.Quit();
    }
}
