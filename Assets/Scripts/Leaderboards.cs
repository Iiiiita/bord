using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
//using Newtonsoft.Json;




public class Leaderboards : MonoBehaviour
{
    public InputField InputFieldScores;
    public Button ButtonInputScore;
    public Text TextLeaderboard;
    private int MaxScores = 5;
    private FirebaseAuth auth;
    private FirebaseUser user = null;
    public DatabaseReference databaseReference;
    public string userID;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void UserSignedIn(bool isSignedIn, string _userID)
    {
        userID = _userID;
    }

   /* public void UserSignedOut(bool isSignedIn)
    {
        userID = "";
    }*/

    public void HandleSubmitScoreButtonClick()
    {
        Debug.Log("Score to submit: " + InputFieldScores.text);
        AddScoreToLeaders(userID, Int32.Parse(InputFieldScores.text), databaseReference.Child("scores"));
    }
    public async void AddScoreToLeaders(string userID, long score, DatabaseReference leaderBoardRef)
    {
        Task<DataSnapshot> scoresData = leaderBoardRef.RunTransaction(mutableData =>
        {
            List<object> leaders = mutableData.Value as List<object>;
            if (leaders == null)
            {
                leaders = new List<object>();
            }
            else if (mutableData.ChildrenCount >= MaxScores)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (score < minScore)
                {
                    // The new score is lower than the existing 5 scores, abort.
                    return TransactionResult.Abort();
                }
                // Remove the lowest score.
                leaders.Remove(minVal);
            }
            // Add the new high score.
            Dictionary<string, object> newScoreMap =
                             new Dictionary<string, object>();
            newScoreMap["score"] = score;
            newScoreMap["userID"] = userID;
            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        });
        try
        {
            await scoresData;
        }
        catch (AggregateException ae)
        {
            ae.InnerExceptions.ToList().ForEach(exception => {
                Debug.Log("Exception: " + exception.Message);
            });
        }
        DataSnapshot dataSnapshot = scoresData.Result;
        Debug.Log(dataSnapshot.GetRawJsonValue());
        string jsonString = dataSnapshot.GetRawJsonValue();
        ScoresData scores = JsonConvert.DeserializeObject<ScoresData>("{\"scores\":" + jsonString + "}");
        scores.scores = scores.scores.OrderByDescending(_score => _score.score).ToList();
        string leaderboardText = "";
        scores.scores.ForEach(_score => {
            leaderboardText += "Score: " + _score.score.ToString("00000000")  + " | " + "User: " +  _score.userID + "\n";
        });
        TextLeaderboard.text = leaderboardText;
    }

    public void Highscorebutton()
    {
        LoadHighscoreData();
        InputFieldScores.text = "";
    }

    private void LoadHighscoreData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("oops...");
                }
                if (task.IsCompleted)
                {
                DataSnapshot dataSnapshot = task.Result;
                Debug.Log("DATA RAW JSON: " + dataSnapshot.GetRawJsonValue());
                    if (dataSnapshot.Exists)
                    {
                    dataSnapshot = task.Result;
                    Debug.Log(dataSnapshot.GetRawJsonValue());
                    string jsonString = dataSnapshot.GetRawJsonValue();
                    ScoresData scores = JsonConvert.DeserializeObject<ScoresData>("{\"scores\":" + jsonString + "}");
                    scores.scores = scores.scores.OrderByDescending(_score => _score.score).ToList();
                    string leaderboardText = "";
                    scores.scores.ForEach(_score => {
                        leaderboardText += "Score: " + _score.score.ToString("00000000") + " | " + "User: " + _score.userID + "\n";
                    });
                    TextLeaderboard.text = leaderboardText;
                }
                    else
                    {
                    }
                }
                else
                {
                    Debug.LogError("something happened...");
                }
        });
    }
}
public class ScoresData
{
    public List<Score> scores;
}
public class Score
{
    public string userID;
    public int score;

    public Score(string _userID, int _score)
    {
        userID = _userID;
        score = _score;
    }
}