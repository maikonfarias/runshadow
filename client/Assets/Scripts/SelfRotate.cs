using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour
{
  void Update()
  {
    if (Time.timeScale != 0.0f)
    {
      transform.Rotate(0, 0, -23f);
    }
  }
}
