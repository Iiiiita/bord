using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager_script : MonoBehaviour
{
    public Button exitButton;
    public Button startButton;
    public Button settingsButton;
    public Button highscoreButton;
    public AudioClip clickSound;
    AudioSource audioSource;
    private bool audioOn = true;
    public GameObject txt;

    public Button signInButton;
    //public Button signOutButton;
    public Text userText;

    public GameObject signInPanel;
    public InputField signEmail;
    public InputField signPassword;
    //public Button signButton;
    public Button createAccountButton;
    public Button closeSignInPanelButton;

    public GameObject createAccountPanel;
    public InputField createEmail;
    public InputField createPassword;
    //public Button createButton;
    //public Button closeCreateAccountPanelButton;

    public GameObject signedInPanel;
    //public Button closeSignedInPanelButton;

    public GameObject signedOutPanel;
    //public Button closeSignedOutPanelButton;

    public GameObject errorPanel;
    //public Button closeErrorPanelButton;

    public Authentication authentication;

    public GameObject scoreInputPanel;
    public GameObject leaderboardPanel;

    void Start()
    {
        Button btn_exit = exitButton.GetComponent<Button>();
        btn_exit.onClick.AddListener(ExitGame);

        Button btn_start = startButton.GetComponent<Button>();
        btn_start.onClick.AddListener(StartGame);

        Button btn_settings = settingsButton.GetComponent<Button>();
        btn_settings.onClick.AddListener(Settings);

        audioSource = GetComponent<AudioSource>();

        signInPanel.SetActive(false);
        createAccountPanel.SetActive(false);
        signedInPanel.SetActive(false);
        signedOutPanel.SetActive(false);
        errorPanel.SetActive(false);
        scoreInputPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        highscoreButton.gameObject.SetActive(false);

    }

    public void HighscoreButton()
    {
        scoreInputPanel.SetActive(true);
       // SceneManager.LoadScene(2);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void Settings()
    {

        switch (audioOn)
        {
            case true:
                AudioListener.volume = 0f;
                audioOn = false;
                txt.GetComponent<UnityEngine.UI.Text>().text = "Audio: Off";
                break;
            case false:
                AudioListener.volume = 1f;
                audioOn = true;
                txt.GetComponent<UnityEngine.UI.Text>().text = "Audio: On";
                break;
        }

        Debug.Log("Settings!");
        audioSource.PlayOneShot(clickSound, 1f);
    }
    void StartGame()
    {
        SceneManager.LoadScene("FlappyScene");
    }

    public void SignIn()
    {
        signInPanel.SetActive(true);
    }

    public void SignOut()
    {
        authentication.SignUserOut();
    }

    public void Sign()
    {
        authentication.SignIn();
    }

    public void createAccout()
    {
        signInPanel.SetActive(false);
        createAccountPanel.SetActive(true);
    }

    public void CloseSignInPanel()
    {
        signInPanel.SetActive(false);
    }
    public void Create()
    {
        authentication.CreateAccount();
    }

    public void CloseCreateAccountPanel()
    {
        createAccountPanel.SetActive(false);
    }

    public void CloseSignedInPanel()
    {
        signedInPanel.SetActive(false);
    }

    public void CloseSignedOutPanel()
    {
        signedOutPanel.SetActive(false);
    }

    public void closeErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    public void openLeaderboardButton()
    {
        leaderboardPanel.SetActive(true);
        scoreInputPanel.SetActive(false);
        }

    public void closeScoreInputPanel()
    {
        scoreInputPanel.SetActive(false);
    }

    public void closeLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
    }
}
