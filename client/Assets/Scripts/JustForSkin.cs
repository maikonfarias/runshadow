using UnityEngine;
using System.Collections;

public class JustForSkin : MonoBehaviour
{
  public int mapSkin = 0;

	void Update ()
  {
    if (PlayerPrefs.GetInt("MapSkin", 0) != mapSkin)
    {
      GetComponent<Renderer>().enabled = false;
    }
    else
    {
      GetComponent<Renderer>().enabled = true;
    }
  }
}
