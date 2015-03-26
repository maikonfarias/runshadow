using UnityEngine;
using System.Collections;

public static class Config
{

  public static string Version
  {
    get { return "1.19"; }
  }

  public static float ScoreMultiplier
  {
    get { return 2.7f; }
  }

  public static string ServerSecrectKey
  {
    get { return "YOUR-SECRET-KEY"; }
  }

  public static string ServerAddress
  {
    get { return "http://maikonfarias.com/unity/runner/score_server"; }
  }
}
