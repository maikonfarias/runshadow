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
  
  public static string FacebookShareLink
  {
    get { return "http://www.facebook.com/sharer/sharer.php?u=maikonfarias.com/unity/runner/"; }
  }
  
  public static string TwitterShareLink
  {
    get { return "http://twitter.com/home?status=Run%20Shadow%20Game%20http://maikonfarias.com/unity/runner/"; }
  }
  
  public static string GooglePlusShareLink
  {
    get { return "https://plus.google.com/share?url=maikonfarias.com/unity/runner/"; }
  }
}
