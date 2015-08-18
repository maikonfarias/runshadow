using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour
{
  void Start()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      PlayerPrefs.SetString("RefererScreen", "GameScreen");	  
      Game.Started = false;
      Game.Score = true;
      return;
    }

    if (other.gameObject.transform.parent)
    {
      Destroy(other.gameObject.transform.gameObject);
    }
    else
    {
      Destroy(other.gameObject);
    }
  }
}
