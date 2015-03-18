﻿using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      //Debug.Break();
      PlayerPrefs.SetString("RefererScreen", "GameScreen");
      Application.LoadLevel(1);
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
