using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
  string textTime = "";
  float floatTime = 0f;
  int rubies;
  float playerScore = 0;


  public Texture2D pauseTexture;
  public Texture2D playTexture;
  public Texture2D[] numbersTexture;
  public Texture2D colonTexture;
  public Texture2D timerTexture;
  public Texture2D rubyTexture0;
  public Texture2D rubyTexture1;
  public Texture2D restartTexture;
  public Texture2D soundOnTexture;
  public Texture2D soundOffTexture;
  public Texture2D quitTexture;

  public Texture2D scoreFrameTexture, timerFrameTexture, rubyFrameTexture;

  bool isSoundOn
  {
    get
    {
      return (PlayerPrefs.GetInt("Sound", 1) == 1);
    }
    set
    {
      PlayerPrefs.SetInt("Sound", (value ? 1 : 0));
    }
  }

  void Start()
  {
    //GameStarted = false;
  }


  void Update()
  {
    if (Game.Started)
    {
      playerScore += Time.deltaTime * Config.ScoreMultiplier;
      floatTime += Time.deltaTime;

      textTime = GetStringTimer(floatTime);
    }
    PlayerPrefs.SetInt("Score", (int)(playerScore * 100));
    PlayerPrefs.SetInt("Rubies", (int)(rubies));
    PlayerPrefs.SetString("Timer", textTime);
    PlayerPrefs.SetFloat("TimerFloat", floatTime);
  }

  string GetStringTimer(float fTime)
  {
    int minutes = (int)(fTime / 60f);
    int seconds = (int)(fTime % 60);
    int mil = (int)((fTime * 1000) % 1000);
    return String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, mil);
  }

  public void IncreaseScore(int amount)
  {
    playerScore += (amount / 100) * Config.ScoreMultiplier;
  }

  public void AddRubies(int quant = 1)
  {
    rubies++;
    Game.AddTotalRubies(quant);
    IncreaseScore(1000);
  }

  void OnDisable()
  {
    /*PlayerPrefs.SetInt("Score", (int)(playerScore * 100));
    PlayerPrefs.SetInt("Rubies", (int)(rubies));
    PlayerPrefs.SetString("Timer", textTime);*/
  }

  void OnGUI()
  {
    //TestFunction();

    if (Game.Started)
    {
      DrawScoreHUD();

      if (!Game.Paused)
      {
        var buttonSize = Screen.height * 0.2f;
        var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
        GUI.DrawTexture(buttonPos, pauseTexture);

        if (GUI.Button(buttonPos, "", new GUIStyle()))
        {
          Game.Paused = true;
        }
      }
      else
      {
        var buttonSize = Screen.height * 0.2f;

        GUI.Box(new Rect(-20, -20, Screen.width + 40, Screen.height + 40), "");
        var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
        GUI.DrawTexture(buttonPos, playTexture);

        if (GUI.Button(buttonPos, "", new GUIStyle()))
        {
          Game.Paused = false;
        }

        buttonPos = new Rect(buttonSize / 4, buttonSize * 1.25f, buttonSize, buttonSize);
        GUI.DrawTexture(buttonPos, restartTexture);

        if (GUI.Button(buttonPos, "", new GUIStyle()))
        {
          Game.Paused = false;
          Game.Started = true;
          SceneManager.LoadScene(0);
        }


        buttonPos = new Rect(buttonSize / 4, buttonSize * 2.25f, buttonSize, buttonSize);
        GUI.DrawTexture(buttonPos, isSoundOn ? soundOnTexture : soundOffTexture);

        if (GUI.Button(buttonPos, "", new GUIStyle()))
        {
          isSoundOn = !isSoundOn;
        }


        buttonPos = new Rect(buttonSize / 4, buttonSize * 3.25f, buttonSize, buttonSize);
        GUI.DrawTexture(buttonPos, quitTexture);

        if (GUI.Button(buttonPos, "", new GUIStyle()))
        {
          Game.Paused = false;
          Game.Started = false;
          SceneManager.LoadScene(0);
        }
      }
    }

  }

  private void TestFunction()
  {

    var buttonPos = new Rect(Screen.width - 50, 0, 50, 30);

    if (GUI.Button(buttonPos, "test"))
    {
      Debug.Log(PlayerPrefs.GetInt("MapSkin", 0));

      if (PlayerPrefs.GetInt("MapSkin", 0) == 0)
      {
        PlayerPrefs.SetInt("MapSkin", 1);
      }
      else if (PlayerPrefs.GetInt("MapSkin", 0) == 1)
      {
        PlayerPrefs.SetInt("MapSkin", 2);
      }
      else if (PlayerPrefs.GetInt("MapSkin", 0) == 2)
      {
        PlayerPrefs.SetInt("MapSkin", 0);
      }
    }

    buttonPos = new Rect(Screen.width - 50, 50, 50, 30);

    if (GUI.Button(buttonPos, "play"))
    {
      Game.Started = true;
    }

  }

  void DrawScoreHUD()
  {
    var buttonSize = Screen.height * 0.2f;
    var letterWidth = buttonSize * .2f;
    var leterHeight = buttonSize * .4f;
    var marginTop = buttonSize * .1f;

    string stringScore = ((int)(playerScore * 100)).ToString();
    char[] letters = stringScore.ToCharArray();
    Array.Reverse(letters);

    float marginRight = buttonSize * 1.3f + letterWidth * 6;
    GUI.DrawTexture(new Rect(marginRight - letterWidth * 6, marginTop, letterWidth * 6 + buttonSize * .3f, leterHeight + buttonSize * .13f), scoreFrameTexture);
    foreach (var letter in letters)
    {
      var buttonPos = new Rect(marginRight, marginTop + buttonSize * .05f, letterWidth, leterHeight);
      GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      marginRight -= letterWidth;
    }


    char[] lettersTime = textTime.ToCharArray();
    float marginRight2 = buttonSize * 3.4f;
    GUI.DrawTexture(new Rect(marginRight2 - buttonSize * .1f - letterWidth * 2, marginTop, letterWidth * 11 + buttonSize * .2f, leterHeight + buttonSize * .13f), timerFrameTexture);
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
    float marginRight3 = buttonSize * 6.3f;
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
  }

}
