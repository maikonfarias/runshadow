using UnityEngine;
using System.Collections;

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
}
