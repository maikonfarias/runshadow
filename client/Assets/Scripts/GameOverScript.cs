using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using SimpleJSON;
using Score;

public class GameOverScript : MonoBehaviour
{
  public AudioClip gameOverSound;
  int score = 0;
  int rubies = 0;
  int moneyRubies = 0;
  string textTime = "00:00:000";
  string txtPlayerName = "";
  string refererScreen = "";
  float screenUpTime = 0f;

  string listMode = "24h";
  bool isShareOpen = false;

  //bool newInterface = true;
  public Texture2D[] characterButtons;
  public Texture2D characterBackgroundTexture;
  public Texture2D[] numbersTexture;
  public Texture2D colonTexture;
  public Texture2D timerTexture;
  public Texture2D rubyTexture0;
  public Texture2D rubyTexture1;
  public Texture2D restartTexture;
  public Texture2D quitTexture;

  public Texture2D sendTexture;
  public Texture2D refreshTexture;
  public Texture2D loadingTexture;
  public Texture2D doneTexture;

  public Texture2D time24hTexture;
  public Texture2D timeAllTexture;
  public Texture2D selectedTexture;
  public Texture2D backTexture;

  public Texture2D shareTexture;
  public Texture2D facebookTexture;
  public Texture2D twitterTexture;
  public Texture2D googleplusTexture;

  public Texture2D scoreboardImageTexture;

  public Texture2D iosIconTexture;
  public Texture2D androidIconTexture;
  public Texture2D wpIconTexture;
  public Texture2D unityIconTexture;

  public Texture2D scoreFrameTexture, timerFrameTexture, rubyFrameTexture, scoreBGTexture;

  GameObject mainCamera;

  void Start()
  {
    mainCamera = GameObject.Find("Main Camera");
  }

  void Update()
  {
    if (!Game.Score)
    {
      return;
    }
    OnGameOver();
    screenUpTime += Time.deltaTime * 2;
    if (refererScreen == "GameScreen" && (Input.GetKeyDown(KeyCode.Return) /*|| Input.GetKeyDown(KeyCode.R)*/ || Input.GetKeyDown(KeyCode.JoystickButton7)))
    {
      PlayAgain();
    }
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
    {
      GoToStartScreen();
    }
  }

  void OnGUI()
  {
    if (!Game.Score)
    {
      return;
    }

    GUI.skin.textField.alignment = TextAnchor.MiddleLeft;
    GUI.skin.textField.fontSize = Screen.height / 25;
    GUI.skin.label.fontSize = Screen.height / 30;
    GUI.skin.box.fontSize = Screen.height / 30;
    //GUI.Box(new Rect(0, 0, 200, 50), refererScreen);
    if (refererScreen == "GameScreen")
    {
      DrawScoreHUD();
    }
    DrawScoreboard();

    var buttonSize = Screen.height * .2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 + buttonSize * 1.3f, buttonSize, buttonSize);
    if (refererScreen == "GameScreen")
    {
      GUI.DrawTexture(buttonPos, restartTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        if (screenUpTime > 0.5f)
        {
          PlayAgain();
        }
      }
    }

    buttonPos = new Rect(Screen.width / 2, Screen.height / 2 + buttonSize * 1.3f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, refererScreen == "GameScreen" ? quitTexture : backTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (screenUpTime > 0.5f)
      {
        GoToStartScreen();
      }
    }



    if (isShareOpen)
    {
      GUI.Box(new Rect(Screen.width - buttonSize * 1.20f, Screen.height / 2 - buttonSize * 1.05f, buttonSize * .9f, buttonSize * 2.75f), "");
      buttonPos = new Rect(Screen.width - buttonSize * 1.125f, Screen.height / 2 + buttonSize * .5f, buttonSize * .75f, buttonSize * .75f);
      GUI.DrawTexture(buttonPos, facebookTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        if (screenUpTime > 0.5f)
        {
          OpenNewUrl(Config.FacebookShareLink);
        }
      }
      buttonPos = new Rect(Screen.width - buttonSize * 1.125f, Screen.height / 2 - buttonSize * .25f, buttonSize * .75f, buttonSize * .75f);
      GUI.DrawTexture(buttonPos, twitterTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        if (screenUpTime > 0.5f)
        {
          OpenNewUrl(Config.TwitterShareLink);
        }
      }
      buttonPos = new Rect(Screen.width - buttonSize * 1.125f, Screen.height / 2 - buttonSize * 1.0f, buttonSize * .75f, buttonSize * .75f);
      GUI.DrawTexture(buttonPos, googleplusTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        if (screenUpTime > 0.5f)
        {
          OpenNewUrl(Config.GooglePlusShareLink);
        }
      }
    }

    buttonPos = new Rect(Screen.width - buttonSize * 1.25f, Screen.height / 2 + buttonSize * 1.3f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, shareTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (screenUpTime > 0.5f)
      {
        isShareOpen = !isShareOpen;
      }
    }
  }

  bool didGameOver = false;
  void OnGameOver()
  {
    if (didGameOver == false)
    {
      didGameOver = true;
      score = PlayerPrefs.GetInt("Score");
      PlayerPrefs.DeleteKey("Score");

      rubies = PlayerPrefs.GetInt("Rubies", 0);
      PlayerPrefs.DeleteKey("Rubies");

      moneyRubies = PlayerPrefs.GetInt("MoneyRubies", 0);

      textTime = PlayerPrefs.GetString("Timer", "00:00:000");
      PlayerPrefs.DeleteKey("Timer");

      txtPlayerName = PlayerPrefs.GetString("PlayerName", "Anonymous");

      refererScreen = PlayerPrefs.GetString("RefererScreen", "GameScreen");
      //PlayerPrefs.DeleteKey("RefererScreen");

      if (PlayerPrefs.GetInt("Sound", 1) == 1 && refererScreen == "GameScreen")
      {
        mainCamera.GetComponent<AudioSource>().Stop();
        mainCamera.GetComponent<AudioSource>().PlayOneShot(gameOverSound, 0.3f);
      }
      StartCoroutine(GetScores());
    }
  }

  void PlayAgain()
  {
    Game.Started = true;
    Game.Score = false;
    Application.LoadLevel(0);
  }

  void GoToStartScreen()
  {
    Game.Started = false;
    Game.Score = false;
    didGameOver = false;
    Application.LoadLevel(0);
  }

  private void DrawScoreHUD()
  {
    float playerScore = (float)score / 100;

    var buttonSize = Screen.height * .2f;
    var letterWidth = buttonSize * .2f;
    var leterHeight = buttonSize * .4f;
    var marginCenter = Screen.width / 2f;
    var marginTop = buttonSize * .1f;

    string stringScore = ((int)(playerScore * 100)).ToString();
    char[] letters = stringScore.ToCharArray();
    Array.Reverse(letters);

    //float marginRight = buttonSize * 1.3f + letterWidth * 6;
    float marginRight = marginCenter - buttonSize * 1.5f;
    GUI.DrawTexture(new Rect(marginRight - letterWidth * 6, marginTop, letterWidth * 6 + buttonSize * .3f, leterHeight + buttonSize * .13f), scoreFrameTexture);
    foreach (var letter in letters)
    {
      var buttonPos = new Rect(marginRight, marginTop + buttonSize * .05f, letterWidth, leterHeight);
      GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      marginRight -= letterWidth;
    }

    char[] lettersTime = textTime.ToCharArray();
    //Array.Reverse(lettersTime);
    //float marginRight2 = buttonSize * 3.4f;
    float marginRight2 = marginCenter - buttonSize * 0.6f;
    GUI.DrawTexture(new Rect(marginRight2 - buttonSize * .1f - letterWidth * 2, marginTop, letterWidth * 11 + buttonSize * .2f, leterHeight + buttonSize * .13f), timerFrameTexture);
    //GUI.DrawTexture(new Rect(marginRight2 - letterWidth * 2.2f, marginTop + buttonSize * .05f, leterHeight, leterHeight), timerTexture);
    foreach (var letter in lettersTime)
    {
      var buttonPos = new Rect(marginRight2, marginTop + buttonSize * .05f, letterWidth, leterHeight);
      if (letter == ':')
      {
        GUI.DrawTexture(buttonPos, colonTexture);
      }
      else
      {
        GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      }
      marginRight2 += letterWidth;
    }

    char[] lettersRubies = rubies.ToString().ToCharArray();
    Array.Reverse(lettersRubies);
    //float marginRight3 = buttonSize * 6.3f;
    float marginRight3 = marginCenter + buttonSize * 2.3f;
    GUI.DrawTexture(new Rect(marginRight3 - buttonSize * .1f - letterWidth * 4, marginTop, letterWidth * 5 + buttonSize * .2f, leterHeight + buttonSize * .13f), rubyFrameTexture);
    foreach (var letter in lettersRubies)
    {
      var buttonPos = new Rect(marginRight3, marginTop + buttonSize * .05f, letterWidth, leterHeight);
      if (letter == ':')
      {
        GUI.DrawTexture(buttonPos, colonTexture);
      }
      else
      {
        GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      }
      marginRight3 -= letterWidth;
    }

    //int moneyRubies = 314522;
    char[] lettersMoneyRubies = moneyRubies.ToString().ToCharArray();
    Array.Reverse(lettersMoneyRubies);
    //float marginRight4 = buttonSize * 5.6f;
    float marginRight4 = marginCenter + buttonSize * 2.3f;
    GUI.DrawTexture(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 4, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .2f, leterHeight + buttonSize * .1f), scoreBGTexture);

    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 3.6f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 4.4f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 4f, marginTop + leterHeight + buttonSize * .3f, leterHeight, leterHeight), rubyTexture0);
    foreach (var letter in lettersMoneyRubies)
    {
      var buttonPos = new Rect(marginRight4 + letterWidth * 3.5f, marginTop + leterHeight + buttonSize * .2f + buttonSize * .05f, letterWidth, leterHeight);
      if (letter == ':')
      {
        GUI.DrawTexture(buttonPos, colonTexture);
      }
      else
      {
        GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      }
      marginRight4 -= letterWidth;
    }
  }

  //bool sentScore = false;
  int sendScoreStatus;
  int loadingScoreStatus;

  List<PlayerScore> scoreList = new List<PlayerScore>();
  void DrawScoreboard()
  {
    var buttonSize = Screen.height * .2f;
    var buttonPos = new Rect(Screen.width / 2 + buttonSize * .5f, Screen.height / 2 - buttonSize * 1.6f, buttonSize / 2, buttonSize / 2);

    if (refererScreen == "GameScreen")
    {
      txtPlayerName = GUI.TextField(new Rect(Screen.width / 2 - buttonSize * 1.5f, Screen.height / 2 - buttonSize * 1.6f, buttonSize * 2, buttonSize / 2), txtPlayerName, 12);
      GUI.DrawTexture(buttonPos, sendScoreStatus == 2 ? doneTexture : sendTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        if (sendScoreStatus == 0) //0 ready / 1 loading / 2 sent
        {
          if (screenUpTime > 1)
          {
            PlayerPrefs.SetString("PlayerName", txtPlayerName);
            StartCoroutine(PostScores(txtPlayerName, score));
          }
        }
      }
      if (sendScoreStatus == 1)
      {
        GUI.DrawTexture(new Rect(Screen.width / 2 + buttonSize * 1f, Screen.height / 2 - buttonSize * 1.6f, buttonSize / 2, buttonSize / 2), loadingTexture);
      }
    }
    else
    {
      GUI.DrawTexture(new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 - buttonSize * 2.4f, buttonSize * 2, buttonSize * 1.3f), scoreboardImageTexture);
    }
    GUI.Box(new Rect(Screen.width / 2 - buttonSize * 2, Screen.height / 2 - buttonSize * 1.05f, buttonSize * 4, buttonSize * 2.3f), "TOP 5");
    GUI.Box(new Rect(Screen.width / 2 - buttonSize * 1.65f, Screen.height / 2 - buttonSize * 0.75f, buttonSize * 3.3f, buttonSize * 1.35f), "");

    //BEGIN LIST
    float marginRight = Screen.width / 2 - buttonSize * 1.5f;
    float marginRight2 = Screen.width / 2 + buttonSize * 0f;
    float lineWidth = buttonSize * 2f;
    float lineWidth2 = buttonSize * 1.5f;
    float lineHeight = buttonSize * 0.25f;

    float marginTop = Screen.height / 2 - buttonSize * 0.7f;
    //float lineHeight = 25f;
    int scorePosition = 1;
    foreach (var scoreRow in scoreList)
    {
#if !UNITY_IOS
      Texture2D playerDevice = null;
      if (scoreRow.Device == "ios")
      {
        playerDevice = iosIconTexture;
      }
      else if (scoreRow.Device == "android")
      {
        playerDevice = androidIconTexture;
      }
      else if (scoreRow.Device == "windowsphone")
      {
        playerDevice = wpIconTexture;
      }
      else if (scoreRow.Device == "webplayer" || scoreRow.Device == "unity")
      {
        playerDevice = unityIconTexture;
      }

      if (playerDevice != null)
      {
        GUI.DrawTexture(new Rect(marginRight2, marginTop, lineHeight, lineHeight), playerDevice);
      }
#endif
      if (!string.IsNullOrEmpty(scoreRow.ISOCountryCode) && scoreRow.ISOCountryCode != "null")
      {
        Texture2D myTexture = Resources.Load("Flags/" + scoreRow.ISOCountryCode.ToLower()) as Texture2D;
        if (myTexture != null)
        {
          GUI.DrawTexture(new Rect(marginRight2 + lineHeight, marginTop, lineHeight, lineHeight), myTexture);
        }
        else
        {
          //Debug.Log("invalid flag: " + scoreRow.ISOCountryCode);
        }
      }
      //scoreRow.CharacterPlayed = "6";
      if (scoreRow.CharacterPlayed != "0" && characterButtons.Length > int.Parse(scoreRow.CharacterPlayed) - 1)
      {
        GUI.DrawTexture(new Rect(marginRight2 + lineHeight * 2, marginTop, lineHeight, lineHeight), characterBackgroundTexture);
        GUI.DrawTexture(new Rect(marginRight2 + lineHeight * 2, marginTop, lineHeight, lineHeight), characterButtons[int.Parse(scoreRow.CharacterPlayed) - 1]);
      }

      GUI.Label(new Rect(marginRight, marginTop, lineWidth, lineHeight), scorePosition++ + ". " + scoreRow.Name);
      GUI.skin.label.alignment = TextAnchor.UpperRight;
      GUI.Label(new Rect(marginRight2, marginTop, lineWidth2, lineHeight), scoreRow.Score);
      GUI.skin.label.alignment = TextAnchor.UpperLeft;
      marginTop += lineHeight;
    }

    //END LIST

    if (listMode == "24h")
    {
      GUI.DrawTexture(new Rect(Screen.width / 2 - buttonSize * 1.01f, Screen.height / 2 + buttonSize * 0.68f, buttonSize * 0.53f, buttonSize * 0.54f), selectedTexture);
    }
    buttonPos = new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 + buttonSize * 0.7f, buttonSize / 2, buttonSize / 2);
    GUI.DrawTexture(buttonPos, time24hTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (loadingScoreStatus == 0 || loadingScoreStatus == 2)
      {
        listMode = "24h";
        StartCoroutine(GetScores());
      }
    }

    if (listMode != "24h")
    {
      GUI.DrawTexture(new Rect(Screen.width / 2 - buttonSize * .51f, Screen.height / 2 + buttonSize * 0.68f, buttonSize * 0.53f, buttonSize * 0.54f), selectedTexture);
    }
    buttonPos = new Rect(Screen.width / 2 - buttonSize * .5f, Screen.height / 2 + buttonSize * 0.7f, buttonSize / 2, buttonSize / 2);
    GUI.DrawTexture(buttonPos, timeAllTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (loadingScoreStatus == 0 || loadingScoreStatus == 2)
      {
        listMode = "";
        StartCoroutine(GetScores());
      }
    }

    buttonPos = new Rect(Screen.width / 2 + buttonSize * .5f, Screen.height / 2 + buttonSize * 0.7f, buttonSize / 2, buttonSize / 2);
    GUI.DrawTexture(buttonPos, refreshTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (loadingScoreStatus == 0 || loadingScoreStatus == 2) // 0 ready, 1 loading, 2 failed
      {
        StartCoroutine(GetScores());
      }
    }
    if (loadingScoreStatus == 1)
    {
      GUI.DrawTexture(new Rect(Screen.width / 2 + buttonSize * 1f, Screen.height / 2 + buttonSize * 0.7f, buttonSize / 2, buttonSize / 2), loadingTexture);
    }
  }

  // remember to use StartCoroutine when calling this function!
  IEnumerator PostScores(string name, int score)
  {
    sendScoreStatus = 1;
    string secretKey = Config.ServerSecrectKey;

    // Server Address
    string addScoreURL = Config.ServerAddress + "?action=add&"; // TODO put this on the config file

    string hash = Md5Sum(name + score + secretKey);

    string post_url = addScoreURL + "name=" + WWW.EscapeURL(name)
                                  + "&score=" + score
                                  + "&hash=" + hash
                                  + "&char=" + (PlayerPrefs.GetInt("SelectedCharacter", 0) + 1)
                                  + "&device=" + Utils.PlatformDevice;

    // Post the URL to the site and create a download object to get the result.
    WWW hs_post = new WWW(post_url);
    Debug.Log("requesting WWW: "+post_url);
    yield return hs_post; // Wait until the download is done

    if (hs_post.error != null)
    {
      sendScoreStatus = 0;
      print("There was an error posting the high score: " + hs_post.error);
    }
    else
    {
      sendScoreStatus = 2;
      StartCoroutine(GetScores());
    }
  }


  // Get the scores from the MySQL DB to display in a GUIText.
  // remember to use StartCoroutine when calling this function!
  IEnumerator GetScores()
  {
    loadingScoreStatus = 1;
    string highscoreURL = Config.ServerAddress + "?action=list&version=" + Config.Version + "&format=json";

    if (listMode == "24h")
    {
      highscoreURL += "&period=24";
    }

    WWW response = new WWW(highscoreURL);

    yield return response;

    if (response.error != null)
    {
      loadingScoreStatus = 2;
      print("There was an error getting the high score: " + response.error);
    }
    else
    {
      var newScoreList = new List<PlayerScore>();

      JSONNode JSONResult;
      try
      {
        JSONResult = JSON.Parse(response.text);

        foreach (JSONNode item in JSONResult.AsArray)
        {
          //string[] line = { item[0].Value, item[1].Value };
          var scoreRow = new PlayerScore();
          scoreRow.Name = item[0].Value;
          scoreRow.Score = item[1].Value;
          scoreRow.Device = item[2].Value;
          scoreRow.ISOCountryCode = item[3].Value;
          scoreRow.CharacterPlayed = item[4].Value;
          newScoreList.Add(scoreRow);
        }
      }
      catch (Exception)
      {

      }

      scoreList = newScoreList;
      loadingScoreStatus = 0;
    }
  }

  public string Md5Sum(string strToEncrypt)
  {

    System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
    byte[] bytes = ue.GetBytes(strToEncrypt);

    // encrypt bytes
    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
    byte[] hashBytes = md5.ComputeHash(bytes);

    // Convert the encrypted bytes back to a string (base 16)
    string hashString = "";

    for (int i = 0; i < hashBytes.Length; i++)
    {
      hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
    }

    return hashString.PadLeft(32, '0');
  }

  void OpenNewUrl(string url, string name = "RunShadow")
  {

#if !UNITY_EDITOR && UNITY_WEBPLAYER

        Application.ExternalEval("window.open('" + url + "','" + name + "')");

#else

    Application.OpenURL(url);

#endif

  }


}
