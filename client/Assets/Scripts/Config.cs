using UnityEngine;
using System.Collections;

public static class Config
{

  public static string Version
  {
    get { return "1.18"; }
  }

  public static float ScoreMultiplier
  {
    get { return 2.5f; }
  }

  public static string ServerSecrectKey
  {
    get { return "YOUR-SERVER-SECRET-KEY"; }
  }
}
