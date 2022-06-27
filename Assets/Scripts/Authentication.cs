using Firebase.Auth;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    public UiManager_script uiManager;
    FirebaseUser user;
    private FirebaseAuth auth;
    public Leaderboards leaderboards;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        /* auth.CreateUserWithEmailAndPasswordAsync(uiManager.createEmail.text, uiManager.createPassword.text).ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                 return;
             }
             if (task.IsFaulted)
             {
                 Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;
             }

             // Firebase user has been created.
             Firebase.Auth.FirebaseUser newUser = task.Result;
             Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                 newUser.DisplayName, newUser.UserId);
         });

         auth.SignInWithEmailAndPasswordAsync(uiManager.signEmail.text, uiManager.signPassword.text).ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                 return;
             }
             if (task.IsFaulted)
             {
                 Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;
             }

             Firebase.Auth.FirebaseUser newUser = task.Result;
             Debug.LogFormat("User signed in successfully: {0} ({1})",
                 newUser.DisplayName, newUser.UserId);
         });*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        //user = null;

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                ClearFields();
                uiManager.signedOutPanel.SetActive(true);
                uiManager.userText.text = "user";
                uiManager.signInButton.gameObject.SetActive(true);
                //leaderboards.UserSignedOut(!signedIn);
                uiManager.highscoreButton.gameObject.SetActive(false);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                Debug.Log("Email " + user.Email);
                uiManager.signedInPanel.SetActive(true);
                uiManager.signInButton.gameObject.SetActive(false);
                leaderboards.UserSignedIn(signedIn, user.UserId);
                uiManager.highscoreButton.gameObject.SetActive(true);
            }
        }
    }

    async public void SignIn()
    {
        string email = uiManager.signEmail.text;
        string password = uiManager.signPassword.text;

        Task signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        Debug.Log("Task started...");

        try
        {
            await signInTask;
            uiManager.signInPanel.SetActive(false);
            uiManager.userText.text = email;
        }
        catch (AggregateException ae)
        {
            uiManager.errorPanel.SetActive(true);
            Debug.Log("Exception...");
        }

        Debug.Log("Task completed...");

    }

    async public void CreateAccount()
    {
        string email = uiManager.createEmail.text;
        string password = uiManager.createPassword.text;

        Task createAccountTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        Debug.Log("Task started...");

        try
        {
            await createAccountTask;
            uiManager.createAccountPanel.SetActive(false);
            uiManager.userText.text = email;
        }
        catch (AggregateException ae)
        {
            uiManager.errorPanel.SetActive(true);
            Debug.Log("Exception...");
        }

        Debug.Log("Task completed...");

    }

    public void SignUserOut()
    {
        auth.SignOut();
    }

    public void ClearFields()
    {
        uiManager.signEmail.text = "";
        uiManager.signPassword.text = "";
        uiManager.createEmail.text = "";
        uiManager.createPassword.text = "";
        uiManager.userText.text = "";
    }
}
