using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class StartScreenScript : MonoBehaviour
{
  public Sprite[] characterSprites;
  public Texture2D[] characterButtons;
  public Texture2D[] mapSkinButtons;
  public SpriteRenderer selectedRender;

  bool isSoundOn = true;
  int selectedCharacter = 0;
  int askUnlockChar = 0;
  List<int> priceForChar = new List<int>();

  int askUnlockMap = 0;
  List<int> priceForMap = new List<int>();

  bool isPreStart = false;
  bool isOptionsOpen = false;
  bool isAboutOpen = false;
  bool isQuitting = false;

  public Texture2D[] numbersTexture;

  public Texture2D playTexture;
  public Texture2D optionsTexture;
  public Texture2D quitTexture;
  public Texture2D titleTexture;

  public Texture2D soundOnTexture;
  public Texture2D soundOffTexture;
  public Texture2D backTexture;
  public Texture2D scoreboardTexture;

  public Texture2D shadowTexture;
  public Texture2D humanTexture;
  public Texture2D pjTexture;
  public Texture2D whiteTexture;
  public Texture2D greenTexture;
  public Texture2D redTexture;

  public Texture2D rubyTexture0;
  public Texture2D boxTexture;
  public Texture2D yesTexture;
  public Texture2D noTexture;

  public Texture2D moneyTexture;

  public Texture2D aboutbuttonTexture;
  public Texture2D aboutCreditsTexture;

  public Texture2D scoreBGTexture;

  void Start()
  {
    isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
    selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

    priceForChar.Add(0);
    priceForChar.Add(0);
    priceForChar.Add(50);
    priceForChar.Add(100);
    priceForChar.Add(200);
    priceForChar.Add(300);

    priceForMap.Add(0);
    priceForMap.Add(0);
    priceForMap.Add(100);
    priceForMap.Add(500);

    //PlayerPrefs.DeleteAll();
    //PlayerPrefs.SetInt("MoneyRubies", 1200);
    //PlayerPrefs.SetInt("HasChar3", 0);
    //PlayerPrefs.SetInt("HasChar5", 0);
    //PlayerPrefs.SetInt("HasMap2", 0);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7))
    {
      Game.Started = true;
    }
    if (Input.GetKeyDown(KeyCode.JoystickButton6))
    {
      PlayerPrefs.SetString("RefererScreen", "StartScreen");
      Application.LoadLevel(1);
    }
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (!Game.Started)
      {
        isQuitting = !isQuitting;
      }
    }
  }



  void OnGUI()
  {
    if (!Game.Started)
    {
      if (isPreStart)
      {
        DrawPreStart();
      }
      else
      {
        DrawTitle();

        if (isQuitting)
        {
          DrawQuitDialog();
        }
        else if (isAboutOpen)
        {
          DrawCredits();
        }
        else if (askUnlockChar > 0)
        {
          DrawAskUnlockChar();
        }
        else if (askUnlockMap > 0)
        {
          DrawAskUnlockMap();
        }
        else if (isOptionsOpen)
        {
          DrawTotalRubies();
          DrawCharacterSelectionButtons();
          DrawMapSkinSelectionButtons();
          DrawCloseOptionsButton();
        }
        else
        {
          DrawStartButton();
          DrawOptionsButton();
          DrawScoreboardButton();
          DrawLanguageButton();
        }
        DrawAboutButton();
        DrawSoundButton();
        DrawQuitButton();
      }
    }
  }

  private void DrawTotalRubies()
  {
    var buttonSize = Screen.height * .2f;
    var letterWidth = buttonSize * .2f;
    var leterHeight = buttonSize * .4f;
    var marginCenter = Screen.width / 2f;
    var marginTop = buttonSize * 3f;

    var moneyRubies = PlayerPrefs.GetInt("MoneyRubies", 0);
    char[] lettersMoneyRubies = moneyRubies.ToString().ToCharArray();
    Array.Reverse(lettersMoneyRubies);


    float marginRight4 = marginCenter + buttonSize * 1.7f;
    //GUI.Box(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 4, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .2f, leterHeight + buttonSize * .1f), "");
    GUI.DrawTexture(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 5, marginTop + leterHeight + buttonSize * .15f, letterWidth * 8.5f + buttonSize * .4f, leterHeight + buttonSize * .2f), scoreBGTexture);

    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 4.5f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 5.3f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 4.9f, marginTop + leterHeight + buttonSize * .3f, leterHeight, leterHeight), rubyTexture0);
    foreach (var letter in lettersMoneyRubies)
    {
      var buttonPos = new Rect(marginRight4 + letterWidth * 3.5f, marginTop + leterHeight + buttonSize * .2f + buttonSize * .05f, letterWidth, leterHeight);

      GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);

      marginRight4 -= letterWidth;
    }
  }

  private void DrawRubiesCost(int cost = 50, Texture2D itemTexture = null)
  {
    var buttonSize = Screen.height * .2f;
    var letterWidth = buttonSize * .2f;
    var leterHeight = buttonSize * .4f;
    var marginCenter = Screen.width / 2f;
    var marginTop = buttonSize * 1.3f;

    var moneyRubies = cost;
    char[] lettersMoneyRubies = moneyRubies.ToString().ToCharArray();
    Array.Reverse(lettersMoneyRubies);

    float marginRight4 = marginCenter + buttonSize * .2f;
    //GUI.Box(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 3, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .2f, leterHeight + buttonSize * .1f), "");
    GUI.DrawTexture(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 3, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .4f, leterHeight + buttonSize * .1f), scoreBGTexture);

    if (itemTexture == null)
    {
      GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 3f, marginTop + leterHeight + buttonSize * .25f, leterHeight, leterHeight), rubyTexture0);
    }
    else
    {
      GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 3f, marginTop + leterHeight + buttonSize * .25f, leterHeight, leterHeight), itemTexture);
    }

    foreach (var letter in lettersMoneyRubies)
    {
      var buttonPos = new Rect(marginRight4 + letterWidth * 5.5f, marginTop + leterHeight + buttonSize * .2f + buttonSize * .05f, letterWidth, leterHeight);
      GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      marginRight4 -= letterWidth;
    }

    marginTop = buttonSize * 1.85f;
    moneyRubies = PlayerPrefs.GetInt("MoneyRubies", 0);
    lettersMoneyRubies = moneyRubies.ToString().ToCharArray();
    Array.Reverse(lettersMoneyRubies);

    marginRight4 = marginCenter + buttonSize * .2f;
    //GUI.Box(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 3, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .2f, leterHeight + buttonSize * .1f), "");
    GUI.DrawTexture(new Rect(marginRight4 - buttonSize * .1f - letterWidth * 3, marginTop + leterHeight + buttonSize * .2f, letterWidth * 8.5f + buttonSize * .4f, leterHeight + buttonSize * .1f), scoreBGTexture);

    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 2.6f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 3.4f, marginTop + leterHeight + buttonSize * .2f, leterHeight, leterHeight), rubyTexture0);
    GUI.DrawTexture(new Rect(marginRight4 - letterWidth * 3f, marginTop + leterHeight + buttonSize * .3f, leterHeight, leterHeight), rubyTexture0);
    foreach (var letter in lettersMoneyRubies)
    {
      var buttonPos = new Rect(marginRight4 + letterWidth * 5.5f, marginTop + leterHeight + buttonSize * .2f + buttonSize * .05f, letterWidth, leterHeight);
      GUI.DrawTexture(buttonPos, numbersTexture[(int)Char.GetNumericValue(letter)]);
      marginRight4 -= letterWidth;
    }
  }

  void DrawTitle()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1.5f, Screen.height / 2 - buttonSize * 2.5f, buttonSize * 3, buttonSize * 1.4f);
    GUI.DrawTexture(buttonPos, titleTexture);
  }

  void DrawAboutButton()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(0, Screen.height - buttonSize, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, aboutbuttonTexture);

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (isAboutOpen)
      {
        isAboutOpen = false;
      }
      else
      {
        isAboutOpen = true;
      }
    }
  }

  void DrawCredits()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 2f, buttonSize * .1f, buttonSize * 4f, buttonSize * 4.8f);
    GUI.DrawTexture(buttonPos, aboutCreditsTexture);
    var aboutText = Utils.LocalizedTexture2D("about_text");
    GUI.DrawTexture(buttonPos, aboutText);
  }

  void StartGame()
  {
    //HUDScript.GameStarted = true;
    isPreStart = true;
  }

  void DrawStartButton()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize / 2, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, playTexture);

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      StartGame();
    }
  }

  void DrawCharacterSelectionButtons()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 2.5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);

    var bgTexture = whiteTexture;

    if (selectedCharacter == 0)
    {
      bgTexture = greenTexture;
    }

    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[0]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      ChangeCurrentSkin(0);
      //selectedRender.sprite = characterSprites[0];
    }


    buttonPos = new Rect(Screen.width / 2 - buttonSize * 1.5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (selectedCharacter == 1)
    {
      bgTexture = greenTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[1]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      ChangeCurrentSkin(1);
    }
    /*
    bool hasChar2 = PlayerPrefs.GetInt("HasChar2", 0) == 1;
    buttonPos = new Rect(Screen.width / 2 + buttonSize * .5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (selectedCharacter == 2)
    {
        bgTexture = greenTexture;
    }
    if (!hasChar2)
    {
        bgTexture = redTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[2]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
        if (hasChar2)
        {
            ChangeCurrentSkin(2);
        }
        else
        {
            askUnlockChar = 2;
        }
    }*/

    bool hasChar3 = PlayerPrefs.GetInt("HasChar3", 0) == 1;
    buttonPos = new Rect(Screen.width / 2 - buttonSize * .5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (selectedCharacter == 3)
    {
      bgTexture = greenTexture;
    }
    if (!hasChar3)
    {
      bgTexture = redTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[3]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (hasChar3)
      {
        ChangeCurrentSkin(3);
      }
      else
      {
        askUnlockChar = 3;
      }
    }

    bool hasChar4 = PlayerPrefs.GetInt("HasChar4", 0) == 1;
    buttonPos = new Rect(Screen.width / 2 + buttonSize * .5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (selectedCharacter == 4)
    {
      bgTexture = greenTexture;
    }
    if (!hasChar4)
    {
      bgTexture = redTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[4]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (hasChar4)
      {
        ChangeCurrentSkin(4);
      }
      else
      {
        askUnlockChar = 4;
      }
    }

    bool hasChar5 = PlayerPrefs.GetInt("HasChar5", 0) == 1;
    buttonPos = new Rect(Screen.width / 2 + buttonSize * 1.5f, Screen.height / 2 - buttonSize * 1.2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (selectedCharacter == 5)
    {
      bgTexture = greenTexture;
    }
    if (!hasChar5)
    {
      bgTexture = redTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, characterButtons[5]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (hasChar5)
      {
        ChangeCurrentSkin(5);
      }
      else
      {
        askUnlockChar = 5;
      }
    }
  }

  private void ChangeCurrentSkin(int characterID)
  {
    selectedCharacter = characterID;
    PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    GetComponent<CameraRunnerScript>().UpdateCurrentCharacterSkin();

  }

  void DrawMapSkinSelectionButtons()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1.5f, Screen.height / 2 - buttonSize * .2f, buttonSize, buttonSize);

    var bgTexture = whiteTexture;

    if (PlayerPrefs.GetInt("MapSkin", 0) == 0)
    {
      bgTexture = greenTexture;
    }

    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, mapSkinButtons[0]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      PlayerPrefs.SetInt("MapSkin", 0);
    }


    buttonPos = new Rect(Screen.width / 2 - buttonSize * .5f, Screen.height / 2 - buttonSize * .2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (PlayerPrefs.GetInt("MapSkin", 0) == 1)
    {
      bgTexture = greenTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, mapSkinButtons[1]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      PlayerPrefs.SetInt("MapSkin", 1);
    }

    bool hasMap2 = PlayerPrefs.GetInt("HasMap2", 0) == 1;
    buttonPos = new Rect(Screen.width / 2 + buttonSize * .5f, Screen.height / 2 - buttonSize * .2f, buttonSize, buttonSize);
    bgTexture = whiteTexture;
    if (PlayerPrefs.GetInt("MapSkin", 0) == 2)
    {
      bgTexture = greenTexture;
    }
    if (!hasMap2)
    {
      bgTexture = redTexture;
    }
    GUI.DrawTexture(buttonPos, bgTexture);
    GUI.DrawTexture(buttonPos, mapSkinButtons[2]);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      if (hasMap2)
      {
        PlayerPrefs.SetInt("MapSkin", 2);
      }
      else
      {
        askUnlockMap = 2;
      }
    }
  }

  private void DrawAskUnlockChar()
  {
    var buttonSize = Screen.height * 0.2f;
    var boxPos = new Rect(Screen.width / 2 - buttonSize * 2f, Screen.height / 2 - buttonSize * .9f, buttonSize * 4, buttonSize * 2);
    GUI.DrawTexture(boxPos, boxTexture);

    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1.7f, Screen.height / 2 - buttonSize * .5f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, whiteTexture);
    GUI.DrawTexture(buttonPos, characterButtons[askUnlockChar]);

    DrawRubiesCost(priceForChar[askUnlockChar]);

    buttonPos = new Rect(Screen.width / 2 + buttonSize * 0.2f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    var currentMoney = PlayerPrefs.GetInt("MoneyRubies", 0);
    if (currentMoney >= priceForChar[askUnlockChar])
    {
      GUI.DrawTexture(buttonPos, yesTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        currentMoney -= priceForChar[askUnlockChar];
        PlayerPrefs.SetInt("MoneyRubies", currentMoney);
        PlayerPrefs.SetInt("HasChar" + askUnlockChar, 1);
        ChangeCurrentSkin(askUnlockChar);

        askUnlockChar = 0;
      }
    }

    buttonPos = new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    GUI.DrawTexture(buttonPos, noTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      askUnlockChar = 0;
    }
  }

  private void DrawAskUnlockMap()
  {
    var buttonSize = Screen.height * 0.2f;
    var boxPos = new Rect(Screen.width / 2 - buttonSize * 2f, Screen.height / 2 - buttonSize * 0.9f, buttonSize * 4, buttonSize * 2);
    GUI.DrawTexture(boxPos, boxTexture);

    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1.7f, Screen.height / 2 - buttonSize * .5f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, whiteTexture);
    GUI.DrawTexture(buttonPos, mapSkinButtons[askUnlockMap]);

    DrawRubiesCost(priceForMap[askUnlockMap]);

    buttonPos = new Rect(Screen.width / 2 + buttonSize * 0.2f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    var currentMoney = PlayerPrefs.GetInt("MoneyRubies", 0);
    if (currentMoney >= priceForMap[askUnlockMap])
    {
      GUI.DrawTexture(buttonPos, yesTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        currentMoney -= priceForMap[askUnlockMap];
        PlayerPrefs.SetInt("MoneyRubies", currentMoney);
        PlayerPrefs.SetInt("HasMap" + askUnlockMap, 1);
        PlayerPrefs.SetInt("MapSkin", askUnlockMap); ;
        askUnlockMap = 0;
      }
    }

    buttonPos = new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    GUI.DrawTexture(buttonPos, noTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      askUnlockMap = 0;
    }
  }

  void DrawSoundButton()
  {
    var buttonSize = Screen.height * 0.2f;
    //var buttonPos = new Rect(Screen.width / 2 - buttonSize / 2, Screen.height / 2 - buttonSize * .2f, buttonSize, buttonSize);
    var buttonPos = new Rect(0, 0, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, (isSoundOn ? soundOnTexture : soundOffTexture));

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      isSoundOn = !isSoundOn;
      PlayerPrefs.SetInt("Sound", (isSoundOn ? 1 : 0));
    }
  }

  void DrawOptionsButton()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize / 2, Screen.height / 2 + buttonSize * .8f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, moneyTexture);

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      isOptionsOpen = true;
    }
  }

  void DrawCloseOptionsButton()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize * .5f, Screen.height / 2 + buttonSize * .8f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, backTexture);

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      isOptionsOpen = false;
    }
  }

  void DrawScoreboardButton()
  {
    var buttonSize = Screen.height * 0.2f;
    var buttonPos = new Rect(Screen.width / 2 - buttonSize / 2, Screen.height / 2 - buttonSize * .2f, buttonSize, buttonSize);
    GUI.DrawTexture(buttonPos, scoreboardTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      PlayerPrefs.SetString("RefererScreen", "StartScreen");
      Application.LoadLevel(1);
    }
  }

  void DrawQuitButton()
  {
    //return;
    if (Application.platform != RuntimePlatform.WindowsWebPlayer && Application.platform != RuntimePlatform.OSXWebPlayer)
    {
      var buttonSize = Screen.height * 0.2f;
      var buttonPos = new Rect(Screen.width - buttonSize, 0, buttonSize, buttonSize);
      GUI.DrawTexture(buttonPos, quitTexture);
      if (GUI.Button(buttonPos, "", new GUIStyle()))
      {
        isQuitting = !isQuitting;
      }
    }
  }

  void DrawQuitDialog()
  {
    var buttonSize = Screen.height * 0.2f;
    var boxPos = new Rect(Screen.width / 2 - buttonSize * 2f, Screen.height / 2 - buttonSize * 0.9f, buttonSize * 4, buttonSize * 2);
    GUI.DrawTexture(boxPos, boxTexture);

    Texture2D quitTexture = Utils.LocalizedTexture2D("quit_message");

    var textPos = new Rect(Screen.width / 2 - buttonSize * 2f, Screen.height / 2 - buttonSize * 0.9f, buttonSize * 4, buttonSize * 2);
    GUI.DrawTexture(textPos, quitTexture);

    var buttonPos = new Rect(Screen.width / 2 - buttonSize * 1f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    GUI.DrawTexture(buttonPos, noTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      isQuitting = false;
    }

    buttonPos = new Rect(Screen.width / 2 + buttonSize * 0.2f, Screen.height / 2 + buttonSize * .65f, buttonSize * .75f, buttonSize * .75f);
    GUI.DrawTexture(buttonPos, yesTexture);
    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      Debug.Log("Application.Quit()");
      Application.Quit();
    }

  }

  public void DrawPreStart()
  {
    var buttonSize = Screen.height * 0.2f;
    Texture2D tapTexture = Utils.LocalizedTexture2D("tap_message");
    var textPos = new Rect(Screen.width / 2 - buttonSize * 3.7f, Screen.height / 2 - buttonSize * 2.1f, buttonSize * 7, buttonSize * 4);
    GUI.DrawTexture(textPos, tapTexture);

    var buttonPos = new Rect(0, 0, Screen.width, Screen.height);

    if (GUI.Button(buttonPos, "", new GUIStyle()))
    {
      isPreStart = false;
      Game.Started = true;
      CharControllerScript.ForceJump = true;
    }
  }

  void DrawLanguageButton()
  {
    return; //TODO implement Change Language Button
  }

}
