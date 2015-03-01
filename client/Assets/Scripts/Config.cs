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

  public static string SystemLanguageCode
  {
    get
    {
      switch (Application.systemLanguage)
      {
        case SystemLanguage.Portuguese:
          return "pt-br";
        case SystemLanguage.Spanish:
          return "es-es";
        case SystemLanguage.French:
          return "fr-fr";
        case SystemLanguage.Chinese:
          return "zh-tw";
        case SystemLanguage.German:
          return "de-de";
        case SystemLanguage.Japanese:
          return "ja-jp";
        default:
          return "en-us";
      }
    }
  }

  public static Object LocalizedResource(string path)
  {
    var resource = Resources.Load("Localized/" + Config.SystemLanguageCode + "/" + path);
    if (resource == null)
    {
      // default resource is english
      resource = Resources.Load("Localized/en-us/" + path);
    }
    return resource;
  }
}
