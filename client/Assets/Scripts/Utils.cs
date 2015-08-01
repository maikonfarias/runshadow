using UnityEngine;
using System.Collections;

public static class Utils
{
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
        case SystemLanguage.Russian:
          return "ru-ru";
        default:
          return "en-us";
      }
    }
  }

  public static Object LocalizedResource(string path)
  {
    var resource = Resources.Load("Localized/" + Utils.SystemLanguageCode + "/" + path);
    if (resource == null)
    {
      // default resource is english
      resource = Resources.Load("Localized/en-us/" + path);
    }
    return resource;
  }

  public static Texture2D LocalizedTexture2D(string path)
  {
    return LocalizedResource(path) as Texture2D;
  }

  public static string PlatformDevice
  {
    get
    {
#if UNITY_WINRT || UNITY_STANDALONE_WIN
      return "windowsphone";
#elif UNITY_ANDROID
      return "android";
#elif UNITY_IOS || UNITY_STANDALONE_OSX
      return "ios";
#else
      return "webplayer";
#endif
    }
  }
}