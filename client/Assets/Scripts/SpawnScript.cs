﻿using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
  public GameObject[] obj;
  public float spawnMin = 1f;
  public float spawnMax = 2f;
  public bool spawnAtStart = true;
  public bool spawnDuringGame = true;


  void Start()
  {
    if (spawnAtStart)
    {
      Spawn();
    }
    else
    {
      Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
  }

  void Spawn()
  {
    if ((Game.Started && spawnDuringGame) || (!Game.Started && spawnAtStart))
    {
      Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
    }
    if (!Game.Over)
    {
      Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
  }
}
