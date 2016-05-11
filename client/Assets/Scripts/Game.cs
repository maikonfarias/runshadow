using UnityEngine;

public static class Game
{
  public static bool Started = false;
  public static bool Score = false;

  public static bool Over
  {
    get
    {
      string refererScreen = PlayerPrefs.GetString("RefererScreen", "GameScreen");
      if (Game.Score && refererScreen == "GameScreen")
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }

  public static bool Paused
  {
    get
    {
      return Time.timeScale == 0.0f;
    }
    set
    {
      Time.timeScale = value ? 0.0f : 1.0f;
    }
  }  

  public static bool Sound
  {
    get
    {
      return PlayerPrefs.GetInt("Sound", 1) == 1;
    }
    set
    {
      PlayerPrefs.SetInt("Sound", 0);
    }
  }

  public static int Character
  {
    get
    {
      return PlayerPrefs.GetInt("SelectedCharacter", 0);
    }
    set
    {
      PlayerPrefs.SetInt("SelectedCharacter", value);
    }
  }

  public static int Map
  {
    get
    {
      return PlayerPrefs.GetInt("MapSkin", 0);
    }
    set
    {
      PlayerPrefs.SetInt("MapSkin", value);
    }
  }

  public static void AddTotalRubies(int quant)
  {
    var moneyRubies = PlayerPrefs.GetInt("MoneyRubies", 0);
    PlayerPrefs.SetInt("MoneyRubies", moneyRubies + quant);
  }

  public static int TotalRubies
  {
    get
    {
      return PlayerPrefs.GetInt("MoneyRubies", 0);
    }
  }
}
