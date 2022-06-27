using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreScript : MonoBehaviour
{
    public int score = 0;
    private int highscoreTemp = 0;
    public int highscore1 = 0;
    public int highscore2 = 0;
    public int highscore3 = 0;
    public int highscore4 = 0;
    public int highscore5 = 0;
    public int check = 0;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI highscoreText1;
    public TextMeshProUGUI highscoreText2;
    public TextMeshProUGUI highscoreText3;
    public TextMeshProUGUI highscoreText4;
    public TextMeshProUGUI highscoreText5;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("osfnolgn");
        score = PlayerPrefs.GetInt("score", 0);
        check = PlayerPrefs.GetInt("highscore1", 0);

        if (score > check)
        {

            highscore1 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore1", 0);
            PlayerPrefs.SetInt("highscore1", highscore1);
            score = highscoreTemp;
        }

        check = PlayerPrefs.GetInt("highscore2", 0);

        if (score > check)
        {
            highscore2 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore2", 0);
            PlayerPrefs.SetInt("highscore2", highscore2);
            score = highscoreTemp;
        }

        check = PlayerPrefs.GetInt("highscore3", 0);

        if (score > check)
        {
            highscore3 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore3", 0);
            PlayerPrefs.SetInt("highscore3", highscore3);
            score = highscoreTemp;
        }

        check = PlayerPrefs.GetInt("highscore4", 0);

        if (score > check)
        {
            highscore4 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore4", 0);
            PlayerPrefs.SetInt("highscore4", highscore4);
            score = highscoreTemp;
        }

        check = PlayerPrefs.GetInt("highscore5", 0);

        if (score > check)
        {
            highscore5 = score;
            highscoreTemp = PlayerPrefs.GetInt("highscore5", 0);
            PlayerPrefs.SetInt("highscore5", highscore5);
            score = highscoreTemp;
        }

        highscore1 = PlayerPrefs.GetInt("highscore1", 0);
        highscore2 = PlayerPrefs.GetInt("highscore2", 0);
        highscore3 = PlayerPrefs.GetInt("highscore3", 0);
        highscore4 = PlayerPrefs.GetInt("highscore4", 0);
        highscore5 = PlayerPrefs.GetInt("highscore5", 0);

        highscoreText1.text = "1. " + highscore1;
        highscoreText2.text = "2. " + highscore2;
        highscoreText3.text = "3. " + highscore3;
        highscoreText4.text = "4. " + highscore4;
        highscoreText5.text = "5. " + highscore5;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
