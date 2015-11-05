using UnityEngine;

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

  public static string CharacterName(int characterId)
  {
    switch(characterId)
    {
      case 0:
        return "Shadow";
      case 1:
        return "Retro";
      case 2:
        return "PJ";
      case 3:
        return "Soccer Ball";
      case 4:
        return "Invisible";
      case 5:
        return "Toly";
      case 6:
        return "Chi";
      default:
        return "Undefined";
    }
  }

  public static string MapName(int mapId)
  {
    switch (mapId)
    {
      case 0:
        return "Field";
      case 1:
        return "Retro";
      case 2:
        return "Black and White";
      case 3:
        return "Dungeon";
      default:
        return "Undefined";
    }
  }
}