using UnityEngine;
using System.Collections;

public static class Game
{
  public static bool Started = false;
  public static bool Over = false;

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
