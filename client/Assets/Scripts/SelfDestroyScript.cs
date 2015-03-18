using UnityEngine;
using System.Collections;

public class SelfDestroyScript : MonoBehaviour
{
  public float time = 5f;

  void Start()
  {
    Destroy(gameObject, time);
  }
}
